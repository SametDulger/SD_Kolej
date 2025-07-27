using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SDKolej.API.Models;
using SDKolej.API.Services;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace SDKolej.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IJwtService _jwtService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager, 
            IJwtService jwtService,
            ILogger<AuthController> logger)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ApiResponse<object>.ErrorResult("Geçersiz model", GetModelStateErrors()));
                }

                if (await _userManager.FindByEmailAsync(model.Email) != null)
                {
                    return BadRequest(ApiResponse<object>.ErrorResult("Bu email adresi zaten kullanılıyor"));
                }

                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    _logger.LogInformation("Yeni kullanıcı oluşturuldu: {Email}", model.Email);
                    return Ok(ApiResponse<object>.SuccessResult(null, "Kullanıcı başarıyla oluşturuldu"));
                }

                return BadRequest(ApiResponse<object>.ErrorResult("Kullanıcı oluşturulamadı", result.Errors.Select(e => e.Description).ToList()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kullanıcı kaydı sırasında hata oluştu");
                return StatusCode(500, ApiResponse<object>.ErrorResult("Kullanıcı kaydı sırasında hata oluştu"));
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ApiResponse<object>.ErrorResult("Geçersiz model", GetModelStateErrors()));
                }

                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return Unauthorized(ApiResponse<object>.ErrorResult("Geçersiz kullanıcı adı veya şifre"));
                }

                var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                if (!result.Succeeded)
                {
                    return Unauthorized(ApiResponse<object>.ErrorResult("Geçersiz kullanıcı adı veya şifre"));
                }

                var roles = await _userManager.GetRolesAsync(user);
                var accessToken = _jwtService.GenerateAccessToken(user.Id, user.Email ?? "", roles.FirstOrDefault() ?? "User");
                var refreshToken = _jwtService.GenerateRefreshToken();

                _logger.LogInformation("Kullanıcı giriş yaptı: {Email}", model.Email);

                return Ok(ApiResponse<object>.SuccessResult(new 
                { 
                    AccessToken = accessToken, 
                    RefreshToken = refreshToken,
                    ExpiresIn = 3600 // 1 saat
                }, "Giriş başarılı"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kullanıcı girişi sırasında hata oluştu");
                return StatusCode(500, ApiResponse<object>.ErrorResult("Giriş sırasında hata oluştu"));
            }
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.AccessToken) || string.IsNullOrEmpty(model.RefreshToken))
                {
                    return BadRequest(ApiResponse<object>.ErrorResult("Access token ve refresh token gereklidir"));
                }

                var principal = _jwtService.GetPrincipalFromExpiredToken(model.AccessToken);
                if (principal == null)
                {
                    return BadRequest(ApiResponse<object>.ErrorResult("Geçersiz access token"));
                }

                var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    return BadRequest(ApiResponse<object>.ErrorResult("Geçersiz token"));
                }

                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return BadRequest(ApiResponse<object>.ErrorResult("Kullanıcı bulunamadı"));
                }

                // Burada refresh token'ın veritabanında saklanması ve kontrol edilmesi gerekir
                // Şimdilik basit bir implementasyon

                var roles = await _userManager.GetRolesAsync(user);
                var newAccessToken = _jwtService.GenerateAccessToken(user.Id, user.Email ?? "", roles.FirstOrDefault() ?? "User");
                var newRefreshToken = _jwtService.GenerateRefreshToken();

                return Ok(ApiResponse<object>.SuccessResult(new 
                { 
                    AccessToken = newAccessToken, 
                    RefreshToken = newRefreshToken,
                    ExpiresIn = 3600
                }, "Token yenilendi"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Token yenileme sırasında hata oluştu");
                return StatusCode(500, ApiResponse<object>.ErrorResult("Token yenileme sırasında hata oluştu"));
            }
        }

        private List<string> GetModelStateErrors()
        {
            return ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
        }
    }

    public class RegisterModel
    {
        [Required(ErrorMessage = "Email alanı zorunludur")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre alanı zorunludur")]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre tekrarı zorunludur")]
        [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }

    public class LoginModel
    {
        [Required(ErrorMessage = "Email alanı zorunludur")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre alanı zorunludur")]
        public string Password { get; set; } = string.Empty;
    }

    public class RefreshTokenModel
    {
        [Required(ErrorMessage = "Access token zorunludur")]
        public string AccessToken { get; set; } = string.Empty;

        [Required(ErrorMessage = "Refresh token zorunludur")]
        public string RefreshToken { get; set; } = string.Empty;
    }
} 
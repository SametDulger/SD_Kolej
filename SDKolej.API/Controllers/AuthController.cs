using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SDKolej.API.Models;
using SDKolej.API.Services;

namespace SDKolej.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly JwtService _jwtService;

        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, JwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<object>.ErrorResult("Geçersiz model"));
            }

            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            {
                return BadRequest(ApiResponse<object>.ErrorResult("Email ve şifre gereklidir"));
            }

            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                return Ok(ApiResponse<object>.SuccessResult(null, "Kullanıcı başarıyla oluşturuldu"));
            }

            return BadRequest(ApiResponse<object>.ErrorResult("Kullanıcı oluşturulamadı", result.Errors.Select(e => e.Description).ToList()));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<object>.ErrorResult("Geçersiz model"));
            }

            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            {
                return BadRequest(ApiResponse<object>.ErrorResult("Email ve şifre gereklidir"));
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return Unauthorized(ApiResponse<object>.ErrorResult("Kullanıcı bulunamadı"));
                }
                
                var roles = await _userManager.GetRolesAsync(user);
                var token = _jwtService.GenerateToken(user.Id, user.Email ?? "", roles.FirstOrDefault() ?? "User");

                return Ok(ApiResponse<object>.SuccessResult(new { Token = token }, "Giriş başarılı"));
            }

            return Unauthorized(ApiResponse<object>.ErrorResult("Geçersiz kullanıcı adı veya şifre"));
        }
    }

    public class RegisterModel
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    public class LoginModel
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
} 
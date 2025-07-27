using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SDKolej.Business.DTOs;
using SDKolej.WebUI.Services;
using SDKolej.WebUI.Models;

namespace SDKolej.WebUI.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly IApiService _apiService;

        public EnrollmentController(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<IEnumerable<EnrollmentDto>>>("/api/Enrollment");
                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Kayıtlar yüklenirken hata oluştu: " + ex.Message;
                return View(new List<EnrollmentDto>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<EnrollmentDto>>($"/api/Enrollment/{id}");
                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Kayıt bulunamadı: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Create()
        {
            try
            {
                // Öğrencileri yükle
                var studentsResponse = await _apiService.GetAsync<ApiResponse<IEnumerable<StudentDto>>>("/api/Student");
                ViewBag.Students = studentsResponse.Data?.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = $"{s.FirstName} {s.LastName} - {s.SchoolNumber}"
                }).ToList() ?? new List<SelectListItem>();

                // Sınıfları yükle
                var classesResponse = await _apiService.GetAsync<ApiResponse<IEnumerable<ClassDto>>>("/api/Class");
                ViewBag.Classes = classesResponse.Data?.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = $"{c.Name} - {c.Branch?.Name}"
                }).ToList() ?? new List<SelectListItem>();

                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Veriler yüklenirken hata oluştu: " + ex.Message;
                ViewBag.Students = new List<SelectListItem>();
                ViewBag.Classes = new List<SelectListItem>();
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EnrollmentDto enrollmentDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _apiService.PostAsync<ApiResponse<object>>("/api/Enrollment", enrollmentDto);
                    TempData["Success"] = "Kayıt başarıyla oluşturuldu";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Kayıt oluşturulurken hata oluştu: " + ex.Message;
                }
            }
            
            // Validation hatası varsa dropdown'ları tekrar doldur
            try
            {
                // Öğrencileri yükle
                var studentsResponse = await _apiService.GetAsync<ApiResponse<IEnumerable<StudentDto>>>("/api/Student");
                ViewBag.Students = studentsResponse.Data?.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = $"{s.FirstName} {s.LastName} - {s.SchoolNumber}"
                }).ToList() ?? new List<SelectListItem>();

                // Sınıfları yükle
                var classesResponse = await _apiService.GetAsync<ApiResponse<IEnumerable<ClassDto>>>("/api/Class");
                ViewBag.Classes = classesResponse.Data?.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = $"{c.Name} - {c.Branch?.Name}"
                }).ToList() ?? new List<SelectListItem>();
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Veriler yüklenirken hata oluştu: " + ex.Message;
                ViewBag.Students = new List<SelectListItem>();
                ViewBag.Classes = new List<SelectListItem>();
            }
            
            return View(enrollmentDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<EnrollmentDto>>($"/api/Enrollment/{id}");
                
                // Öğrencileri yükle
                var studentsResponse = await _apiService.GetAsync<ApiResponse<IEnumerable<StudentDto>>>("/api/Student");
                ViewBag.Students = studentsResponse.Data?.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = $"{s.FirstName} {s.LastName} - {s.SchoolNumber}"
                }).ToList() ?? new List<SelectListItem>();

                // Sınıfları yükle
                var classesResponse = await _apiService.GetAsync<ApiResponse<IEnumerable<ClassDto>>>("/api/Class");
                ViewBag.Classes = classesResponse.Data?.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = $"{c.Name} - {c.Branch?.Name}"
                }).ToList() ?? new List<SelectListItem>();

                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Kayıt bulunamadı: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EnrollmentDto enrollmentDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _apiService.PutAsync<ApiResponse<object>>($"/api/Enrollment/{id}", enrollmentDto);
                    TempData["Success"] = "Kayıt başarıyla güncellendi";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Kayıt güncellenirken hata oluştu: " + ex.Message;
                }
            }
            return View(enrollmentDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<EnrollmentDto>>($"/api/Enrollment/{id}");
                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Kayıt bulunamadı: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var success = await _apiService.DeleteAsync($"/api/Enrollment/{id}");
                if (success)
                {
                    TempData["Success"] = "Kayıt başarıyla silindi";
                }
                else
                {
                    TempData["Error"] = "Kayıt silinemedi";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Kayıt silinirken hata oluştu: " + ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
    }
} 

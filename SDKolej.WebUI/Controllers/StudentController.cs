using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SDKolej.WebUI.Services;
using SDKolej.WebUI.Models;
using SDKolej.Business.DTOs;

namespace SDKolej.WebUI.Controllers
{
    public class StudentController : Controller
    {
        private readonly IApiService _apiService;

        public StudentController(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<IEnumerable<StudentDto>>>("/api/Student");
                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Öğrenciler yüklenirken hata oluştu: " + ex.Message;
                return View(new List<StudentDto>());
            }
        }

        public async Task<IActionResult> Create()
        {
            try
            {
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
                TempData["Error"] = "Sınıflar yüklenirken hata oluştu: " + ex.Message;
                ViewBag.Classes = new List<SelectListItem>();
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentDto studentDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _apiService.PostAsync<ApiResponse<object>>("/api/Student", studentDto);
                    TempData["Success"] = "Öğrenci başarıyla oluşturuldu";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Öğrenci oluşturulurken hata oluştu: " + ex.Message;
                }
            }
            
            // Validation hatası varsa dropdown'ları tekrar doldur
            try
            {
                var classesResponse = await _apiService.GetAsync<ApiResponse<IEnumerable<ClassDto>>>("/api/Class");
                ViewBag.Classes = classesResponse.Data?.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = $"{c.Name} - {c.Branch?.Name}"
                }).ToList() ?? new List<SelectListItem>();
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Sınıflar yüklenirken hata oluştu: " + ex.Message;
                ViewBag.Classes = new List<SelectListItem>();
            }
            
            return View(studentDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<StudentDto>>($"/api/Student/{id}");
                
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
                TempData["Error"] = "Öğrenci bulunamadı: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StudentDto studentDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _apiService.PutAsync<ApiResponse<object>>($"/api/Student/{id}", studentDto);
                    TempData["Success"] = "Öğrenci başarıyla güncellendi";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Öğrenci güncellenirken hata oluştu: " + ex.Message;
                }
            }
            return View(studentDto);
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<StudentDto>>($"/api/Student/{id}");
                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Öğrenci bulunamadı: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<StudentDto>>($"/api/Student/{id}");
                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Öğrenci bulunamadı: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var success = await _apiService.DeleteAsync($"/api/Student/{id}");
                if (success)
                {
                    TempData["Success"] = "Öğrenci başarıyla silindi";
                }
                else
                {
                    TempData["Error"] = "Öğrenci silinemedi";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Öğrenci silinirken hata oluştu: " + ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
    }
} 

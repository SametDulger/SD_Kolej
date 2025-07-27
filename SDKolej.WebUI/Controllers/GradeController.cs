using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SDKolej.Business.DTOs;
using SDKolej.WebUI.Services;
using SDKolej.WebUI.Models;

namespace SDKolej.WebUI.Controllers
{
    public class GradeController : Controller
    {
        private readonly IApiService _apiService;

        public GradeController(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<IEnumerable<GradeDto>>>("/api/Grade");
                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Notlar yüklenirken hata oluştu: " + ex.Message;
                return View(new List<GradeDto>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<GradeDto>>($"/api/Grade/{id}");
                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Not bulunamadı: " + ex.Message;
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

                // Dersleri yükle
                var coursesResponse = await _apiService.GetAsync<ApiResponse<IEnumerable<CourseDto>>>("/api/Course");
                ViewBag.Courses = coursesResponse.Data?.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList() ?? new List<SelectListItem>();

                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Veriler yüklenirken hata oluştu: " + ex.Message;
                ViewBag.Students = new List<SelectListItem>();
                ViewBag.Courses = new List<SelectListItem>();
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GradeDto gradeDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _apiService.PostAsync<ApiResponse<object>>("/api/Grade", gradeDto);
                    TempData["Success"] = "Not başarıyla oluşturuldu";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Not oluşturulurken hata oluştu: " + ex.Message;
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

                // Dersleri yükle
                var coursesResponse = await _apiService.GetAsync<ApiResponse<IEnumerable<CourseDto>>>("/api/Course");
                ViewBag.Courses = coursesResponse.Data?.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList() ?? new List<SelectListItem>();
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Veriler yüklenirken hata oluştu: " + ex.Message;
                ViewBag.Students = new List<SelectListItem>();
                ViewBag.Courses = new List<SelectListItem>();
            }
            
            return View(gradeDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<GradeDto>>($"/api/Grade/{id}");
                
                // Öğrencileri yükle
                var studentsResponse = await _apiService.GetAsync<ApiResponse<IEnumerable<StudentDto>>>("/api/Student");
                ViewBag.Students = studentsResponse.Data?.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = $"{s.FirstName} {s.LastName} - {s.SchoolNumber}"
                }).ToList() ?? new List<SelectListItem>();

                // Dersleri yükle
                var coursesResponse = await _apiService.GetAsync<ApiResponse<IEnumerable<CourseDto>>>("/api/Course");
                ViewBag.Courses = coursesResponse.Data?.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList() ?? new List<SelectListItem>();

                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Not bulunamadı: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GradeDto gradeDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _apiService.PutAsync<ApiResponse<object>>($"/api/Grade/{id}", gradeDto);
                    TempData["Success"] = "Not başarıyla güncellendi";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Not güncellenirken hata oluştu: " + ex.Message;
                }
            }
            return View(gradeDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<GradeDto>>($"/api/Grade/{id}");
                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Not bulunamadı: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var success = await _apiService.DeleteAsync($"/api/Grade/{id}");
                if (success)
                {
                    TempData["Success"] = "Not başarıyla silindi";
                }
                else
                {
                    TempData["Error"] = "Not silinemedi";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Not silinirken hata oluştu: " + ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
    }
} 

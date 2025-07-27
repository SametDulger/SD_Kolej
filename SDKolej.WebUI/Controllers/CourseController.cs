using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SDKolej.Business.DTOs;
using SDKolej.WebUI.Services;
using SDKolej.WebUI.Models;

namespace SDKolej.WebUI.Controllers
{
    public class CourseController : Controller
    {
        private readonly IApiService _apiService;

        public CourseController(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<IEnumerable<CourseDto>>>("/api/Course");
                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Dersler yüklenirken hata oluştu: " + ex.Message;
                return View(new List<CourseDto>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<CourseDto>>($"/api/Course/{id}");
                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Ders bulunamadı: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Create()
        {
            try
            {
                // Üst dersleri yükle
                var coursesResponse = await _apiService.GetAsync<ApiResponse<IEnumerable<CourseDto>>>("/api/Course");
                ViewBag.ParentCourses = coursesResponse.Data?.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList() ?? new List<SelectListItem>();

                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Dersler yüklenirken hata oluştu: " + ex.Message;
                ViewBag.ParentCourses = new List<SelectListItem>();
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseDto courseDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _apiService.PostAsync<ApiResponse<object>>("/api/Course", courseDto);
                    TempData["Success"] = "Ders başarıyla oluşturuldu";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Ders oluşturulurken hata oluştu: " + ex.Message;
                }
            }
            
            // Validation hatası varsa dropdown'ları tekrar doldur
            try
            {
                var parentCoursesResponse = await _apiService.GetAsync<ApiResponse<IEnumerable<CourseDto>>>("/api/Course");
                ViewBag.ParentCourses = parentCoursesResponse.Data?.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList() ?? new List<SelectListItem>();
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Üst dersler yüklenirken hata oluştu: " + ex.Message;
                ViewBag.ParentCourses = new List<SelectListItem>();
            }
            
            return View(courseDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<CourseDto>>($"/api/Course/{id}");
                
                // Üst dersleri yükle
                var coursesResponse = await _apiService.GetAsync<ApiResponse<IEnumerable<CourseDto>>>("/api/Course");
                ViewBag.ParentCourses = coursesResponse.Data?.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList() ?? new List<SelectListItem>();

                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Ders bulunamadı: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CourseDto courseDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _apiService.PutAsync<ApiResponse<object>>($"/api/Course/{id}", courseDto);
                    TempData["Success"] = "Ders başarıyla güncellendi";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Ders güncellenirken hata oluştu: " + ex.Message;
                }
            }
            return View(courseDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<CourseDto>>($"/api/Course/{id}");
                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Ders bulunamadı: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var success = await _apiService.DeleteAsync($"/api/Course/{id}");
                if (success)
                {
                    TempData["Success"] = "Ders başarıyla silindi";
                }
                else
                {
                    TempData["Error"] = "Ders silinemedi";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Ders silinirken hata oluştu: " + ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
    }
} 

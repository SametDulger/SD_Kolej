using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SDKolej.Business.DTOs;
using SDKolej.WebUI.Services;
using SDKolej.WebUI.Models;

namespace SDKolej.WebUI.Controllers
{
    public class TeacherCourseController : Controller
    {
        private readonly IApiService _apiService;

        public TeacherCourseController(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<IEnumerable<TeacherCourseDto>>>("/api/TeacherCourse");
                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Öğretmen dersleri yüklenirken hata oluştu: " + ex.Message;
                return View(new List<TeacherCourseDto>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<TeacherCourseDto>>($"/api/TeacherCourse/{id}");
                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Öğretmen dersi bulunamadı: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Create()
        {
            try
            {
                // Öğretmenleri yükle
                var teachersResponse = await _apiService.GetAsync<ApiResponse<IEnumerable<TeacherDto>>>("/api/Teacher");
                ViewBag.Teachers = teachersResponse.Data?.Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = $"{t.FirstName} {t.LastName}"
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
                ViewBag.Teachers = new List<SelectListItem>();
                ViewBag.Courses = new List<SelectListItem>();
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeacherCourseDto teacherCourseDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _apiService.PostAsync<ApiResponse<object>>("/api/TeacherCourse", teacherCourseDto);
                    TempData["Success"] = "Öğretmen dersi başarıyla oluşturuldu";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Öğretmen dersi oluşturulurken hata oluştu: " + ex.Message;
                }
            }
            
            // Validation hatası varsa dropdown'ları tekrar doldur
            try
            {
                // Öğretmenleri yükle
                var teachersResponse = await _apiService.GetAsync<ApiResponse<IEnumerable<TeacherDto>>>("/api/Teacher");
                ViewBag.Teachers = teachersResponse.Data?.Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = $"{t.FirstName} {t.LastName}"
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
                ViewBag.Teachers = new List<SelectListItem>();
                ViewBag.Courses = new List<SelectListItem>();
            }
            
            return View(teacherCourseDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<TeacherCourseDto>>($"/api/TeacherCourse/{id}");
                
                // Öğretmenleri yükle
                var teachersResponse = await _apiService.GetAsync<ApiResponse<IEnumerable<TeacherDto>>>("/api/Teacher");
                ViewBag.Teachers = teachersResponse.Data?.Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = $"{t.FirstName} {t.LastName}"
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
                TempData["Error"] = "Öğretmen dersi bulunamadı: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TeacherCourseDto teacherCourseDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _apiService.PutAsync<ApiResponse<object>>($"/api/TeacherCourse/{id}", teacherCourseDto);
                    TempData["Success"] = "Öğretmen dersi başarıyla güncellendi";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Öğretmen dersi güncellenirken hata oluştu: " + ex.Message;
                }
            }
            return View(teacherCourseDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<TeacherCourseDto>>($"/api/TeacherCourse/{id}");
                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Öğretmen dersi bulunamadı: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var success = await _apiService.DeleteAsync($"/api/TeacherCourse/{id}");
                if (success)
                {
                    TempData["Success"] = "Öğretmen dersi başarıyla silindi";
                }
                else
                {
                    TempData["Error"] = "Öğretmen dersi silinemedi";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Öğretmen dersi silinirken hata oluştu: " + ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
    }
} 

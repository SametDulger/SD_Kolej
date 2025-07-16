using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Create()
        {
            return View();
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
            return View(teacherCourseDto);
        }

        public async Task<IActionResult> Edit(int id)
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

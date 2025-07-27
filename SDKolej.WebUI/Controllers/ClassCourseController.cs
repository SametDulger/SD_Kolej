using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SDKolej.Business.DTOs;
using SDKolej.WebUI.Services;
using SDKolej.WebUI.Models;

namespace SDKolej.WebUI.Controllers
{
    public class ClassCourseController : Controller
    {
        private readonly IApiService _apiService;

        public ClassCourseController(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<IEnumerable<ClassCourseDto>>>("/api/ClassCourse");
                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Sınıf dersleri yüklenirken hata oluştu: " + ex.Message;
                return View(new List<ClassCourseDto>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<ClassCourseDto>>($"/api/ClassCourse/{id}");
                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Sınıf dersi bulunamadı: " + ex.Message;
                return RedirectToAction(nameof(Index));
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
                ViewBag.Classes = new List<SelectListItem>();
                ViewBag.Courses = new List<SelectListItem>();
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClassCourseDto classCourseDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _apiService.PostAsync<ApiResponse<object>>("/api/ClassCourse", classCourseDto);
                    TempData["Success"] = "Sınıf dersi başarıyla oluşturuldu";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Sınıf dersi oluşturulurken hata oluştu: " + ex.Message;
                }
            }
            
            // Validation hatası varsa dropdown'ları tekrar doldur
            try
            {
                // Sınıfları yükle
                var classesResponse = await _apiService.GetAsync<ApiResponse<IEnumerable<ClassDto>>>("/api/Class");
                ViewBag.Classes = classesResponse.Data?.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = $"{c.Name} - {c.Branch?.Name}"
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
                ViewBag.Classes = new List<SelectListItem>();
                ViewBag.Courses = new List<SelectListItem>();
            }
            
            return View(classCourseDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<ClassCourseDto>>($"/api/ClassCourse/{id}");
                
                // Sınıfları yükle
                var classesResponse = await _apiService.GetAsync<ApiResponse<IEnumerable<ClassDto>>>("/api/Class");
                ViewBag.Classes = classesResponse.Data?.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = $"{c.Name} - {c.Branch?.Name}"
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
                TempData["Error"] = "Sınıf dersi bulunamadı: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClassCourseDto classCourseDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _apiService.PutAsync<ApiResponse<object>>($"/api/ClassCourse/{id}", classCourseDto);
                    TempData["Success"] = "Sınıf dersi başarıyla güncellendi";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Sınıf dersi güncellenirken hata oluştu: " + ex.Message;
                }
            }
            return View(classCourseDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<ClassCourseDto>>($"/api/ClassCourse/{id}");
                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Sınıf dersi bulunamadı: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var success = await _apiService.DeleteAsync($"/api/ClassCourse/{id}");
                if (success)
                {
                    TempData["Success"] = "Sınıf dersi başarıyla silindi";
                }
                else
                {
                    TempData["Error"] = "Sınıf dersi silinemedi";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Sınıf dersi silinirken hata oluştu: " + ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
    }
} 

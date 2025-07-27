using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SDKolej.Business.DTOs;
using SDKolej.WebUI.Services;
using SDKolej.WebUI.Models;

namespace SDKolej.WebUI.Controllers
{
    public class StudentCourseController : Controller
    {
        private readonly IApiService _apiService;

        public StudentCourseController(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<IEnumerable<StudentCourseDto>>>("/api/StudentCourse");
                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Öğrenci dersleri yüklenirken hata oluştu: " + ex.Message;
                return View(new List<StudentCourseDto>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<StudentCourseDto>>($"/api/StudentCourse/{id}");
                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Öğrenci dersi bulunamadı: " + ex.Message;
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

                // Sınıfları yükle
                var classesResponse = await _apiService.GetAsync<ApiResponse<IEnumerable<ClassDto>>>("/api/Class");
                ViewBag.Classes = classesResponse.Data?.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = $"{c.Name} - {c.Branch?.Name}"
                }).ToList() ?? new List<SelectListItem>();

                // Öğretmenleri yükle
                var teachersResponse = await _apiService.GetAsync<ApiResponse<IEnumerable<TeacherDto>>>("/api/Teacher");
                ViewBag.Teachers = teachersResponse.Data?.Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = $"{t.FirstName} {t.LastName}"
                }).ToList() ?? new List<SelectListItem>();

                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Veriler yüklenirken hata oluştu: " + ex.Message;
                ViewBag.Students = new List<SelectListItem>();
                ViewBag.Courses = new List<SelectListItem>();
                ViewBag.Classes = new List<SelectListItem>();
                ViewBag.Teachers = new List<SelectListItem>();
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentCourseDto studentCourseDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _apiService.PostAsync<ApiResponse<object>>("/api/StudentCourse", studentCourseDto);
                    TempData["Success"] = "Öğrenci dersi başarıyla oluşturuldu";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Öğrenci dersi oluşturulurken hata oluştu: " + ex.Message;
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

                // Sınıfları yükle
                var classesResponse = await _apiService.GetAsync<ApiResponse<IEnumerable<ClassDto>>>("/api/Class");
                ViewBag.Classes = classesResponse.Data?.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = $"{c.Name} - {c.Branch?.Name}"
                }).ToList() ?? new List<SelectListItem>();

                // Öğretmenleri yükle
                var teachersResponse = await _apiService.GetAsync<ApiResponse<IEnumerable<TeacherDto>>>("/api/Teacher");
                ViewBag.Teachers = teachersResponse.Data?.Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = $"{t.FirstName} {t.LastName}"
                }).ToList() ?? new List<SelectListItem>();
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Veriler yüklenirken hata oluştu: " + ex.Message;
                ViewBag.Students = new List<SelectListItem>();
                ViewBag.Courses = new List<SelectListItem>();
                ViewBag.Classes = new List<SelectListItem>();
                ViewBag.Teachers = new List<SelectListItem>();
            }
            
            return View(studentCourseDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<StudentCourseDto>>($"/api/StudentCourse/{id}");
                
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

                // Sınıfları yükle
                var classesResponse = await _apiService.GetAsync<ApiResponse<IEnumerable<ClassDto>>>("/api/Class");
                ViewBag.Classes = classesResponse.Data?.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = $"{c.Name} - {c.Branch?.Name}"
                }).ToList() ?? new List<SelectListItem>();

                // Öğretmenleri yükle
                var teachersResponse = await _apiService.GetAsync<ApiResponse<IEnumerable<TeacherDto>>>("/api/Teacher");
                ViewBag.Teachers = teachersResponse.Data?.Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = $"{t.FirstName} {t.LastName}"
                }).ToList() ?? new List<SelectListItem>();

                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Öğrenci dersi bulunamadı: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StudentCourseDto studentCourseDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _apiService.PutAsync<ApiResponse<object>>($"/api/StudentCourse/{id}", studentCourseDto);
                    TempData["Success"] = "Öğrenci dersi başarıyla güncellendi";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Öğrenci dersi güncellenirken hata oluştu: " + ex.Message;
                }
            }
            return View(studentCourseDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<StudentCourseDto>>($"/api/StudentCourse/{id}");
                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Öğrenci dersi bulunamadı: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var response = await _apiService.DeleteAsync($"/api/StudentCourse/{id}");
                TempData["Success"] = "Öğrenci dersi başarıyla silindi";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Öğrenci dersi silinirken hata oluştu: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
    }
} 
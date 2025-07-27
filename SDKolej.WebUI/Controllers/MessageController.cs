using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SDKolej.Business.DTOs;
using SDKolej.WebUI.Services;
using SDKolej.WebUI.Models;

namespace SDKolej.WebUI.Controllers
{
    public class MessageController : Controller
    {
        private readonly IApiService _apiService;

        public MessageController(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<IEnumerable<MessageDto>>>("/api/Message");
                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Mesajlar yüklenirken hata oluştu: " + ex.Message;
                return View(new List<MessageDto>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<MessageDto>>($"/api/Message/{id}");
                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Mesaj bulunamadı: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Create()
        {
            try
            {
                // Öğrencileri yükle
                var studentsResponse = await _apiService.GetAsync<ApiResponse<IEnumerable<StudentDto>>>("/api/Student");
                var students = studentsResponse.Data?.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = $"Öğrenci: {s.FirstName} {s.LastName} - {s.SchoolNumber}"
                }).ToList() ?? new List<SelectListItem>();

                // Öğretmenleri yükle
                var teachersResponse = await _apiService.GetAsync<ApiResponse<IEnumerable<TeacherDto>>>("/api/Teacher");
                var teachers = teachersResponse.Data?.Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = $"Öğretmen: {t.FirstName} {t.LastName}"
                }).ToList() ?? new List<SelectListItem>();

                // Velileri yükle
                var parentsResponse = await _apiService.GetAsync<ApiResponse<IEnumerable<ParentDto>>>("/api/Parent");
                var parents = parentsResponse.Data?.Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = $"Veli: {p.FirstName} {p.LastName} - {p.Phone}"
                }).ToList() ?? new List<SelectListItem>();

                // Tüm kullanıcıları birleştir
                var allUsers = new List<SelectListItem>();
                allUsers.AddRange(students);
                allUsers.AddRange(teachers);
                allUsers.AddRange(parents);

                ViewBag.Senders = allUsers;
                ViewBag.Receivers = allUsers;

                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Kullanıcılar yüklenirken hata oluştu: " + ex.Message;
                ViewBag.Senders = new List<SelectListItem>();
                ViewBag.Receivers = new List<SelectListItem>();
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MessageDto messageDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _apiService.PostAsync<ApiResponse<object>>("/api/Message", messageDto);
                    TempData["Success"] = "Mesaj başarıyla oluşturuldu";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Mesaj oluşturulurken hata oluştu: " + ex.Message;
                }
            }
            
            // Validation hatası varsa dropdown'ları tekrar doldur
            try
            {
                // Öğrencileri yükle
                var studentsResponse = await _apiService.GetAsync<ApiResponse<IEnumerable<StudentDto>>>("/api/Student");
                var students = studentsResponse.Data?.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = $"Öğrenci: {s.FirstName} {s.LastName} - {s.SchoolNumber}"
                }).ToList() ?? new List<SelectListItem>();

                // Öğretmenleri yükle
                var teachersResponse = await _apiService.GetAsync<ApiResponse<IEnumerable<TeacherDto>>>("/api/Teacher");
                var teachers = teachersResponse.Data?.Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = $"Öğretmen: {t.FirstName} {t.LastName}"
                }).ToList() ?? new List<SelectListItem>();

                // Velileri yükle
                var parentsResponse = await _apiService.GetAsync<ApiResponse<IEnumerable<ParentDto>>>("/api/Parent");
                var parents = parentsResponse.Data?.Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = $"Veli: {p.FirstName} {p.LastName} - {p.Phone}"
                }).ToList() ?? new List<SelectListItem>();

                // Tüm kullanıcıları birleştir
                var allUsers = new List<SelectListItem>();
                allUsers.AddRange(students);
                allUsers.AddRange(teachers);
                allUsers.AddRange(parents);

                ViewBag.Senders = allUsers;
                ViewBag.Receivers = allUsers;
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Kullanıcılar yüklenirken hata oluştu: " + ex.Message;
                ViewBag.Senders = new List<SelectListItem>();
                ViewBag.Receivers = new List<SelectListItem>();
            }
            
            return View(messageDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<MessageDto>>($"/api/Message/{id}");
                
                // Öğrencileri yükle
                var studentsResponse = await _apiService.GetAsync<ApiResponse<IEnumerable<StudentDto>>>("/api/Student");
                var students = studentsResponse.Data?.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = $"Öğrenci: {s.FirstName} {s.LastName} - {s.SchoolNumber}"
                }).ToList() ?? new List<SelectListItem>();

                // Öğretmenleri yükle
                var teachersResponse = await _apiService.GetAsync<ApiResponse<IEnumerable<TeacherDto>>>("/api/Teacher");
                var teachers = teachersResponse.Data?.Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = $"Öğretmen: {t.FirstName} {t.LastName}"
                }).ToList() ?? new List<SelectListItem>();

                // Velileri yükle
                var parentsResponse = await _apiService.GetAsync<ApiResponse<IEnumerable<ParentDto>>>("/api/Parent");
                var parents = parentsResponse.Data?.Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = $"Veli: {p.FirstName} {p.LastName} - {p.Phone}"
                }).ToList() ?? new List<SelectListItem>();

                // Tüm kullanıcıları birleştir
                var allUsers = new List<SelectListItem>();
                allUsers.AddRange(students);
                allUsers.AddRange(teachers);
                allUsers.AddRange(parents);

                ViewBag.Senders = allUsers;
                ViewBag.Receivers = allUsers;

                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Mesaj bulunamadı: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MessageDto messageDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _apiService.PutAsync<ApiResponse<object>>($"/api/Message/{id}", messageDto);
                    TempData["Success"] = "Mesaj başarıyla güncellendi";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Mesaj güncellenirken hata oluştu: " + ex.Message;
                }
            }
            return View(messageDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<MessageDto>>($"/api/Message/{id}");
                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Mesaj bulunamadı: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var success = await _apiService.DeleteAsync($"/api/Message/{id}");
                if (success)
                {
                    TempData["Success"] = "Mesaj başarıyla silindi";
                }
                else
                {
                    TempData["Error"] = "Mesaj silinemedi";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Mesaj silinirken hata oluştu: " + ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
    }
} 

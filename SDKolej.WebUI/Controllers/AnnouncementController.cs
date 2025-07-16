using Microsoft.AspNetCore.Mvc;
using SDKolej.Business.DTOs;
using SDKolej.WebUI.Services;
using SDKolej.WebUI.Models;

namespace SDKolej.WebUI.Controllers
{
    public class AnnouncementController : Controller
    {
        private readonly IApiService _apiService;

        public AnnouncementController(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<IEnumerable<AnnouncementDto>>>("/api/Announcement");
                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Duyurular yüklenirken hata oluştu: " + ex.Message;
                return View(new List<AnnouncementDto>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<AnnouncementDto>>($"/api/Announcement/{id}");
                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Duyuru bulunamadı: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AnnouncementDto announcementDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _apiService.PostAsync<ApiResponse<object>>("/api/Announcement", announcementDto);
                    TempData["Success"] = "Duyuru başarıyla oluşturuldu";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Duyuru oluşturulurken hata oluştu: " + ex.Message;
                }
            }
            return View(announcementDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<AnnouncementDto>>($"/api/Announcement/{id}");
                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Duyuru bulunamadı: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AnnouncementDto announcementDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _apiService.PutAsync<ApiResponse<object>>($"/api/Announcement/{id}", announcementDto);
                    TempData["Success"] = "Duyuru başarıyla güncellendi";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Duyuru güncellenirken hata oluştu: " + ex.Message;
                }
            }
            return View(announcementDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<AnnouncementDto>>($"/api/Announcement/{id}");
                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Duyuru bulunamadı: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var success = await _apiService.DeleteAsync($"/api/Announcement/{id}");
                if (success)
                {
                    TempData["Success"] = "Duyuru başarıyla silindi";
                }
                else
                {
                    TempData["Error"] = "Duyuru silinemedi";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Duyuru silinirken hata oluştu: " + ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
    }
} 

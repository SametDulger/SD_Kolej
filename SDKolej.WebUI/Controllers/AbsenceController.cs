using Microsoft.AspNetCore.Mvc;
using SDKolej.Business.DTOs;
using SDKolej.WebUI.Services;
using SDKolej.WebUI.Models;

namespace SDKolej.WebUI.Controllers
{
    public class AbsenceController : Controller
    {
        private readonly IApiService _apiService;

        public AbsenceController(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<IEnumerable<AbsenceDto>>>("/api/Absence");
                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Devamsızlık kayıtları yüklenirken hata oluştu: " + ex.Message;
                return View(new List<AbsenceDto>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<AbsenceDto>>($"/api/Absence/{id}");
                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Devamsızlık kaydı bulunamadı: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AbsenceDto absenceDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _apiService.PostAsync<ApiResponse<object>>("/api/Absence", absenceDto);
                    TempData["Success"] = "Devamsızlık kaydı başarıyla oluşturuldu";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Devamsızlık kaydı oluşturulurken hata oluştu: " + ex.Message;
                }
            }
            return View(absenceDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<AbsenceDto>>($"/api/Absence/{id}");
                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Devamsızlık kaydı bulunamadı: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AbsenceDto absenceDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _apiService.PutAsync<ApiResponse<object>>($"/api/Absence/{id}", absenceDto);
                    TempData["Success"] = "Devamsızlık kaydı başarıyla güncellendi";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Devamsızlık kaydı güncellenirken hata oluştu: " + ex.Message;
                }
            }
            return View(absenceDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<AbsenceDto>>($"/api/Absence/{id}");
                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Devamsızlık kaydı bulunamadı: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var success = await _apiService.DeleteAsync($"/api/Absence/{id}");
                if (success)
                {
                    TempData["Success"] = "Devamsızlık kaydı başarıyla silindi";
                }
                else
                {
                    TempData["Error"] = "Devamsızlık kaydı silinemedi";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Devamsızlık kaydı silinirken hata oluştu: " + ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
    }
} 

using Microsoft.AspNetCore.Mvc;
using SDKolej.Business.DTOs;
using SDKolej.WebUI.Services;
using SDKolej.WebUI.Models;

namespace SDKolej.WebUI.Controllers
{
    public class FileUploadController : Controller
    {
        private readonly IApiService _apiService;

        public FileUploadController(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<IEnumerable<FileUploadDto>>>("/api/FileUpload");
                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Dosyalar yüklenirken hata oluştu: " + ex.Message;
                return View(new List<FileUploadDto>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<FileUploadDto>>($"/api/FileUpload/{id}");
                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Dosya bulunamadı: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FileUploadDto fileUploadDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _apiService.PostAsync<ApiResponse<object>>("/api/FileUpload", fileUploadDto);
                    TempData["Success"] = "Dosya başarıyla yüklendi";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Dosya yüklenirken hata oluştu: " + ex.Message;
                }
            }
            return View(fileUploadDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<FileUploadDto>>($"/api/FileUpload/{id}");
                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Dosya bulunamadı: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FileUploadDto fileUploadDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _apiService.PutAsync<ApiResponse<object>>($"/api/FileUpload/{id}", fileUploadDto);
                    TempData["Success"] = "Dosya başarıyla güncellendi";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Dosya güncellenirken hata oluştu: " + ex.Message;
                }
            }
            return View(fileUploadDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<FileUploadDto>>($"/api/FileUpload/{id}");
                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Dosya bulunamadı: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var success = await _apiService.DeleteAsync($"/api/FileUpload/{id}");
                if (success)
                {
                    TempData["Success"] = "Dosya başarıyla silindi";
                }
                else
                {
                    TempData["Error"] = "Dosya silinemedi";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Dosya silinirken hata oluştu: " + ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
    }
} 

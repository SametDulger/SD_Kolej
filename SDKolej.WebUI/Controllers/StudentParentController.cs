using Microsoft.AspNetCore.Mvc;
using SDKolej.Business.DTOs;
using SDKolej.WebUI.Services;
using SDKolej.WebUI.Models;

namespace SDKolej.WebUI.Controllers
{
    public class StudentParentController : Controller
    {
        private readonly IApiService _apiService;

        public StudentParentController(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<IEnumerable<StudentParentDto>>>("/api/StudentParent");
                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Öğrenci veli ilişkileri yüklenirken hata oluştu: " + ex.Message;
                return View(new List<StudentParentDto>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<StudentParentDto>>($"/api/StudentParent/{id}");
                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Öğrenci veli ilişkisi bulunamadı: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentParentDto studentParentDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _apiService.PostAsync<ApiResponse<object>>("/api/StudentParent", studentParentDto);
                    TempData["Success"] = "Öğrenci veli ilişkisi başarıyla oluşturuldu";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Öğrenci veli ilişkisi oluşturulurken hata oluştu: " + ex.Message;
                }
            }
            return View(studentParentDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<StudentParentDto>>($"/api/StudentParent/{id}");
                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Öğrenci veli ilişkisi bulunamadı: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StudentParentDto studentParentDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _apiService.PutAsync<ApiResponse<object>>($"/api/StudentParent/{id}", studentParentDto);
                    TempData["Success"] = "Öğrenci veli ilişkisi başarıyla güncellendi";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Öğrenci veli ilişkisi güncellenirken hata oluştu: " + ex.Message;
                }
            }
            return View(studentParentDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<StudentParentDto>>($"/api/StudentParent/{id}");
                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Öğrenci veli ilişkisi bulunamadı: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var success = await _apiService.DeleteAsync($"/api/StudentParent/{id}");
                if (success)
                {
                    TempData["Success"] = "Öğrenci veli ilişkisi başarıyla silindi";
                }
                else
                {
                    TempData["Error"] = "Öğrenci veli ilişkisi silinemedi";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Öğrenci veli ilişkisi silinirken hata oluştu: " + ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
    }
} 

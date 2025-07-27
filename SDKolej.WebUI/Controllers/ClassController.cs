using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SDKolej.Business.DTOs;
using SDKolej.WebUI.Services;
using SDKolej.WebUI.Models;

namespace SDKolej.WebUI.Controllers
{
    public class ClassController : Controller
    {
        private readonly IApiService _apiService;

        public ClassController(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<IEnumerable<ClassDto>>>("/api/Class");
                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Sınıflar yüklenirken hata oluştu: " + ex.Message;
                return View(new List<ClassDto>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<ClassDto>>($"/api/Class/{id}");
                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Sınıf bulunamadı: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Create()
        {
            try
            {
                // Şubeleri yükle
                var branchesResponse = await _apiService.GetAsync<ApiResponse<IEnumerable<BranchDto>>>("/api/Branch");
                ViewBag.Branches = branchesResponse.Data?.Select(b => new SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = b.Name
                }).ToList() ?? new List<SelectListItem>();

                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Şubeler yüklenirken hata oluştu: " + ex.Message;
                ViewBag.Branches = new List<SelectListItem>();
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClassDto classDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _apiService.PostAsync<ApiResponse<object>>("/api/Class", classDto);
                    TempData["Success"] = "Sınıf başarıyla oluşturuldu";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Sınıf oluşturulurken hata oluştu: " + ex.Message;
                }
            }
            
            // Validation hatası varsa dropdown'ları tekrar doldur
            try
            {
                var branchesResponse = await _apiService.GetAsync<ApiResponse<IEnumerable<BranchDto>>>("/api/Branch");
                ViewBag.Branches = branchesResponse.Data?.Select(b => new SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = b.Name
                }).ToList() ?? new List<SelectListItem>();
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Şubeler yüklenirken hata oluştu: " + ex.Message;
                ViewBag.Branches = new List<SelectListItem>();
            }
            
            return View(classDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<ClassDto>>($"/api/Class/{id}");
                
                // Şubeleri yükle
                var branchesResponse = await _apiService.GetAsync<ApiResponse<IEnumerable<BranchDto>>>("/api/Branch");
                ViewBag.Branches = branchesResponse.Data?.Select(b => new SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = b.Name
                }).ToList() ?? new List<SelectListItem>();

                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Sınıf bulunamadı: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClassDto classDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _apiService.PutAsync<ApiResponse<object>>($"/api/Class/{id}", classDto);
                    TempData["Success"] = "Sınıf başarıyla güncellendi";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Sınıf güncellenirken hata oluştu: " + ex.Message;
                }
            }
            return View(classDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<ClassDto>>($"/api/Class/{id}");
                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Sınıf bulunamadı: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var success = await _apiService.DeleteAsync($"/api/Class/{id}");
                if (success)
                {
                    TempData["Success"] = "Sınıf başarıyla silindi";
                }
                else
                {
                    TempData["Error"] = "Sınıf silinemedi";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Sınıf silinirken hata oluştu: " + ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
    }
} 

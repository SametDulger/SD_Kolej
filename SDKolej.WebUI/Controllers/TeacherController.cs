using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SDKolej.Business.DTOs;
using SDKolej.WebUI.Services;
using SDKolej.WebUI.Models;

namespace SDKolej.WebUI.Controllers
{
    public class TeacherController : Controller
    {
        private readonly IApiService _apiService;

        public TeacherController(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<IEnumerable<TeacherDto>>>("/api/Teacher");
                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Öğretmenler yüklenirken hata oluştu: " + ex.Message;
                return View(new List<TeacherDto>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<TeacherDto>>($"/api/Teacher/{id}");
                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Öğretmen bulunamadı: " + ex.Message;
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
        public async Task<IActionResult> Create(TeacherDto teacherDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _apiService.PostAsync<ApiResponse<object>>("/api/Teacher", teacherDto);
                    TempData["Success"] = "Öğretmen başarıyla oluşturuldu";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Öğretmen oluşturulurken hata oluştu: " + ex.Message;
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
            
            return View(teacherDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<TeacherDto>>($"/api/Teacher/{id}");
                
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
                TempData["Error"] = "Öğretmen bulunamadı: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TeacherDto teacherDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _apiService.PutAsync<ApiResponse<object>>($"/api/Teacher/{id}", teacherDto);
                    TempData["Success"] = "Öğretmen başarıyla güncellendi";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Öğretmen güncellenirken hata oluştu: " + ex.Message;
                }
            }
            return View(teacherDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _apiService.GetAsync<ApiResponse<TeacherDto>>($"/api/Teacher/{id}");
                return View(response.Data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Öğretmen bulunamadı: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var success = await _apiService.DeleteAsync($"/api/Teacher/{id}");
                if (success)
                {
                    TempData["Success"] = "Öğretmen başarıyla silindi";
                }
                else
                {
                    TempData["Error"] = "Öğretmen silinemedi";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Öğretmen silinirken hata oluştu: " + ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
    }
} 

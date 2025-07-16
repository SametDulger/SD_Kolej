using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SDKolej.WebUI.Models;
using SDKolej.WebUI.Services;

namespace SDKolej.WebUI.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IApiService _apiService;

    public HomeController(ILogger<HomeController> logger, IApiService apiService)
    {
        _logger = logger;
        _apiService = apiService;
    }

    public async Task<IActionResult> Index()
    {
        var dashboardData = new DashboardViewModel
        {
            StudentCount = await GetCountAsync("Student"),
            TeacherCount = await GetCountAsync("Teacher"),
            ParentCount = await GetCountAsync("Parent"),
            ClassCount = await GetCountAsync("Class"),
            CourseCount = await GetCountAsync("Course"),
            EnrollmentCount = await GetCountAsync("Enrollment"),
            GradeCount = await GetCountAsync("Grade"),
            AbsenceCount = await GetCountAsync("Absence"),
            AnnouncementCount = await GetCountAsync("Announcement"),
            MessageCount = await GetCountAsync("Message"),
            DocumentCount = await GetCountAsync("Document"),
            FileUploadCount = await GetCountAsync("FileUpload"),
            BranchCount = await GetCountAsync("Branch")
        };

        return View(dashboardData);
    }

    private async Task<int> GetCountAsync(string entity)
    {
        try
        {
            var response = await _apiService.GetAsync<ApiResponse<List<object>>>($"/api/{entity}");
            if (response?.Success == true && response.Data != null)
            {
                return response.Data.Count;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting count for {Entity}", entity);
        }
        return 0;
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

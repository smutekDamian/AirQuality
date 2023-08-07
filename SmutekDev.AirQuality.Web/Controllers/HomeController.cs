using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using SmutekDev.AirQuality.Core.Models;
using SmutekDev.AirQuality.Core.Services;
using SmutekDev.AirQuality.Web.Configuration;
using SmutekDev.AirQuality.Web.Models.ViewModels;

namespace SmutekDev.AirQuality.Web.Controllers;

public class HomeController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly IAirQualityService _airQualityService;

    public HomeController(IConfiguration configuration, IAirQualityService airQualityService)
    {
        _configuration = configuration;
        _airQualityService = airQualityService;
    }

    public IActionResult Index()
    {
        var viewModel = new HomeViewModel
        {
            GooglePlacesApiKey = _configuration[ConfigConstants.GooglePlacesApiKey]
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AirQuality([FromForm] GetAirQualityParams parameters)
    {
        var airQuality = await _airQualityService.GetAirQualityForLocalization(parameters);

        var viewModel = new AirQualityViewModel
        {
            AirQuality = airQuality
        };

        return PartialView(viewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
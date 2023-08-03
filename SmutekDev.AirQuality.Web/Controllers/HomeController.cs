using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using SmutekDev.AirQuality.Web.Configuration;
using SmutekDev.AirQuality.Web.Models.ViewModels;

namespace SmutekDev.AirQuality.Web.Controllers;

public class HomeController : Controller
{
    private readonly IConfiguration _configuration;

    public HomeController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IActionResult Index()
    {
        var viewModel = new HomeViewModel
        {
            GooglePlacesApiKey = _configuration[ConfigConstants.GooglePlacesApiKey]
        };

        return View(viewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
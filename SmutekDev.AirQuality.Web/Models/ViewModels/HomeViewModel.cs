using SmutekDev.AirQuality.Core.Models;

namespace SmutekDev.AirQuality.Web.Models.ViewModels;

public class HomeViewModel
{
    public string GooglePlacesApiKey { get; set; }
    public Distance Distance { get; set; }
    public SortOrder SortOrder { get; set; }
}
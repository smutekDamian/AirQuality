using SmutekDev.AirQuality.Core.Models;

namespace SmutekDev.AirQuality.Web.Models.ViewModels;

public class AirQualityViewModel
{
    public string Localization => AirQuality?.LocalizationName;
    public LocalizationAirQuality AirQuality { get; set; }
}
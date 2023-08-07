using SmutekDev.AirQuality.Core.Models;

namespace SmutekDev.AirQuality.Web.Models.ViewModels;

public class AirQualityViewModel
{
    public LocalizationAirQuality AirQuality { get; set; }
    public string Localization => AirQuality?.LocalizationName;
    public bool IsDataAvailable => AirQuality?.MeasurementStations?.Any() ?? false;
}
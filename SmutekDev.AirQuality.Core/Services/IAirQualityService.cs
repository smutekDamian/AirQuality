using SmutekDev.AirQuality.Core.Models;

namespace SmutekDev.AirQuality.Core.Services;

public interface IAirQualityService
{
    LocalizationAirQuality GetAirQualityForLocalization(string localizationName, string lat, string lng);
}
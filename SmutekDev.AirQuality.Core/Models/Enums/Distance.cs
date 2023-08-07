using System.ComponentModel.DataAnnotations;

namespace SmutekDev.AirQuality.Core.Models.Enums;

public enum Distance
{
    [Display(Name = "1 km")]
    OneKm = 1,
    [Display(Name = "10 km")]
    TenKm = 10,
    [Display(Name = "15 km")]
    FifteenKm = 15,
    [Display(Name = "25 km")]
    TwentyFiveKm = 25
}
﻿using SmutekDev.AirQuality.Core.Models.Enums;

namespace SmutekDev.AirQuality.Core.Models;

public class GetAirQualityParams
{
    public string Localization { get; set; }
    public string Lat { get; set; }
    public string Lng { get; set; }
    public Distance Distance { get; set; }
    public SortOrder SortOrder { get; set; }
    public int PageSize { get; set; } = 100;
}
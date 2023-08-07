using System.ComponentModel.DataAnnotations;

namespace SmutekDev.AirQuality.Core.Models;

public enum SortOrder
{
    [Display(Name = "First Updated")]
    FirstUpdated,
    [Display(Name = "Last Updated")]
    LastUpdated,
    [Display(Name = "Distance ASC")]
    DistanceAsc,
    [Display(Name = "Distance DESC")]
    DistanceDesc
}
using SmutekDev.AirQuality.Core.Models.Enums;

namespace SmutekDev.AirQuality.Integration.OpenAQ.Models;

internal class FilteringParameters
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 100;
    public int Skip { get; set; } = 0;
    public Distance Distance { get; set; } = Distance.OneKm;
    public SortOrder SortOrder { get; set; } = SortOrder.FirstUpdated;
}
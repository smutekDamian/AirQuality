using Microsoft.Extensions.DependencyInjection;
namespace SmutekDev.AirQuality.Integration.OpenAQ;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOpenAQ(this IServiceCollection services)
    {
        return services;
    }
}
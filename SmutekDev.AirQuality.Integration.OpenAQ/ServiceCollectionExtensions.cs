using Microsoft.Extensions.DependencyInjection;
using SmutekDev.AirQuality.Integration.OpenAQ.Services;

namespace SmutekDev.AirQuality.Integration.OpenAQ;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOpenAQ(this IServiceCollection services)
    {
        services.AddHttpClient<OpenAQClient>(x =>
        {
            x.BaseAddress = new Uri("https://api.openaq.org/v2/");
        });
        return services;
    }
}
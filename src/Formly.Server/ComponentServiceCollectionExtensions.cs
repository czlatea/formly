using Formly.Server.Services;
using Formly.Shared.Services;

namespace Microsoft.Extensions.DependencyInjection
{
  public static class ComponentServiceCollectionExtensions
  {
    public static void AddFormly(this IServiceCollection services)
    {
      services.AddSingleton<ITemplateProcessor, TemplateProcessor>();

      services.AddScoped<ITemplateService, TemplateService>();
      services.AddScoped<IWeatherForecastService, WeatherForecastService>();
    }
  }
}
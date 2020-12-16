using Formly.Server;
using Formly.Server.Services;
using Formly.Shared;
using Formly.Shared.Services;

namespace Microsoft.Extensions.DependencyInjection
{
  public static class ComponentServiceCollectionExtensions
  {
    public static void AddFormly(this IServiceCollection services)
    {
      services.AddSingleton<ITemplateMetaDataRetriever, TemplateMetaDataRetriever>();

      services.AddScoped<ITemplateService, TemplateService>();
      services.AddScoped<IWeatherForecastService, WeatherForecastService>();
    }
  }
}
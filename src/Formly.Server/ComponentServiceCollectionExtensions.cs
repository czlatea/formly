using Formly.Server;
using Formly.Shared;

namespace Microsoft.Extensions.DependencyInjection
{
  public static class ComponentServiceCollectionExtensions
  {
    public static void AddFormly(this IServiceCollection services)
    {
      services.AddSingleton<ITemplateMetaDataRetriever, TemplateMetaDataRetriever>();

      services.AddScoped<ITemplateRepository, TemplateRepository>();
      services.AddScoped<IWeatherForecastService, WeatherForecastService>();
    }
  }
}
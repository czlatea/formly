using Formly.Server.Services;
using Formly.Shared.Services;
using Microsoft.Extensions.Caching.Memory;

namespace Microsoft.Extensions.DependencyInjection
{
  public static class ComponentServiceCollectionExtensions
  {
    public static void AddFormly(this IServiceCollection services)
    {
      services.AddSingleton<ITemplateProcessor, TemplateProcessor>();

      services.AddScoped<TemplateService>();
      services.AddScoped<ITemplateService>(p => new TemplateServiceCachingDecorator(p.GetService<IMemoryCache>(), p.GetService<TemplateService>()));
    }
  }
}
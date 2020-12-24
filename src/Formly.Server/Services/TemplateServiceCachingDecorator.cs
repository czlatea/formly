using System.Collections.Generic;
using Formly.Shared;
using Formly.Shared.Services;
using Framework;
using Microsoft.Extensions.Caching.Memory;

namespace Formly.Server.Services
{
  internal class TemplateServiceCachingDecorator : CachingDecoratorBase<ITemplateService>, ITemplateService
  {
    public TemplateServiceCachingDecorator(IMemoryCache cache, ITemplateService inner)
      : base(cache, inner)
    {
    }

    public string GetTemplateContent(long templateId)
    {
      return GetOrCreate($"Template_{templateId}", () => Inner.GetTemplateContent(templateId));
    }

    public IList<TemplateMetaDataItem> GetTemplateMetaData(long templateId)
    {
      return GetOrCreate($"TemplateMetaData_{templateId}", () => Inner.GetTemplateMetaData(templateId));
    }

    public string Transform(long templateId, IDictionary<string, string> placeholderValues)
    {
      return Inner.Transform(templateId, placeholderValues);
    }
  }
}
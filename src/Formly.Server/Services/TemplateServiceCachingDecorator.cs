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

    public string GetTemplateContent(long id)
    {
      return GetOrCreate($"{nameof(GetTemplateContent)}_{id}", () => Inner.GetTemplateContent(id));
    }

    public IList<TemplateMetaDataItem> GetTemplateMetaData(long id)
    {
      return GetOrCreate($"{nameof(GetTemplateMetaData)}_{id}", () => Inner.GetTemplateMetaData(id));
    }

    public IList<TemplateMetaDataItem> GetTemplateMetaDataByExternalId(string externalId)
    {
      return GetOrCreate($"{nameof(GetTemplateMetaDataByExternalId)}_{externalId}", () => Inner.GetTemplateMetaDataByExternalId(externalId));
    }

    public string Transform(long id, IDictionary<string, string> placeholderValues)
    {
      return Inner.Transform(id, placeholderValues);
    }

    public TemplateDetails GetTemplateDetails(long id)
    {
      return GetOrCreate($"{nameof(GetTemplateDetails)}_{id}", () => Inner.GetTemplateDetails(id));
    }

    public TemplateDetails GetTemplateDetailsByExternalId(string externalId)
    {
      return GetOrCreate($"{nameof(GetTemplateDetailsByExternalId)}_{externalId}", () => Inner.GetTemplateDetailsByExternalId(externalId));
    }

    public IList<TemplateDetails> GetAllTemplates()
    {
      return GetOrCreate($"{nameof(GetAllTemplates)}", () => Inner.GetAllTemplates());
    }

    public string GetTemplateContentByExternalId(string externalId)
    {
      return GetOrCreate($"{nameof(GetTemplateContentByExternalId)}_{externalId}", () => Inner.GetTemplateContentByExternalId(externalId));
    }
  }
}
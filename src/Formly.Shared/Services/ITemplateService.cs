using System.Collections.Generic;

namespace Formly.Shared.Services
{
  public interface ITemplateService
  {
    string GetTemplateContent(long id);
    string GetTemplateContentByExternalId(string externalId);

    IList<TemplateMetaDataItem> GetTemplateMetaData(long id);
    IList<TemplateMetaDataItem> GetTemplateMetaDataByExternalId(string externalId);

    TemplateDetails GetTemplateDetails(long id);
    TemplateDetails GetTemplateDetailsByExternalId(string externalId);

    string Transform(long id, IDictionary<string, string> placeholderValues);

    IList<TemplateDetails> GetAllTemplates();
  }
}
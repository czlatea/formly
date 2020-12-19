using System.Collections.Generic;

namespace Formly.Shared.Services
{
  public interface ITemplateService
  {
    string GetTemplateContent(long templateId);
    IList<TemplateMetaDataItem> GetTemplateMetaData(long templateId);
    string Transform(long templateId, IDictionary<string, string> placeholderValues);
  }
}
using System.Collections.Generic;

namespace Formly.Shared.Services
{
  public interface ITemplateProcessor
  {
    IList<TemplateMetaDataItem> GetMetaDataItems(string templateContent);
    string Transform(string templateContent, IDictionary<string, string> placeholderValues);
  }
}

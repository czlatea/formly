using System.Collections.Generic;

namespace Formly.Shared.Services
{
  public interface ITemplateProcessor
  {
    IList<TemplateMetaDataItem> GetMetaDataItems(string templateContent);
    string TransformToText(string templateContent, IDictionary<string, string> placeholderValues);
    string TransformToPdf(string templateContent, IDictionary<string, string> placeholderValues);
  }
}

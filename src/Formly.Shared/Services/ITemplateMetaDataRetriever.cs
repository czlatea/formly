using System.Collections.Generic;

namespace Formly.Shared.Services
{
  public interface ITemplateMetaDataRetriever
  {
    IList<TemplateMetaDataItem> GetMetaDataItems(string templateContent);
  }
}

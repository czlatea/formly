using System;
using System.Collections.Generic;
using Formly.Shared;
using Formly.Shared.Services;

namespace Formly.Server.Services
{
  internal class TemplateService : ITemplateService
  {
    private readonly ITemplateMetaDataRetriever mTemplateMetaDataRetriever;

    public TemplateService(ITemplateMetaDataRetriever templateMetaDataRetriever)
    {
      mTemplateMetaDataRetriever = templateMetaDataRetriever;
    }
    
    public string GetTemplateContent(long templateId)
    {
      return 
        "Please fill in the required fields"+Environment.NewLine+ "**First Name**:{{FirstName}}" + Environment.NewLine + "**Last Name **:{{FirstName}}";
    }

    public IList<TemplateMetaDataItem> GetTemplateMetaData(long templateId)
    {
      string templateContent = GetTemplateContent(templateId);

      return mTemplateMetaDataRetriever.GetMetaDataItems(templateContent);
    }
  }
}

﻿using System;
using System.Collections.Generic;
using Formly.Shared;
using Formly.Shared.Services;
using MarkdownDeep;

namespace Formly.Server.Services
{
  internal class TemplateService : ITemplateService
  {
    private readonly ITemplateProcessor mTemplateProcessor;

    public TemplateService(ITemplateProcessor templateProcessor)
    {
      mTemplateProcessor = templateProcessor;
    }

    public string GetTemplateContent(long templateId)
    {
      return
        "Please fill in the required fields" + Environment.NewLine + "**First Name**:{{FirstName}}" + Environment.NewLine + "**Last Name **:{{FirstName}}";
    }

    public IList<TemplateMetaDataItem> GetTemplateMetaData(long templateId)
    {
      string templateContent = GetTemplateContent(templateId);

      return mTemplateProcessor.GetMetaDataItems(templateContent);
    }

    public string Transform(long templateId, IDictionary<string, string> placeholderValues)
    {
      string templateContent = GetTemplateContent(templateId);

      return mTemplateProcessor.Transform(templateContent, placeholderValues);
    }
  }
}
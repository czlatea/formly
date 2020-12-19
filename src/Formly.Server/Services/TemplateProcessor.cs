﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Formly.Shared;
using Formly.Shared.Services;
using Framework;
using Framework.Extensions;
using iTextSharp.text.pdf;
using MarkdownDeep;

namespace Formly.Server.Services
{
  public class TemplateProcessor : ITemplateProcessor
  {
    private static readonly Regex mPlaceholderMatchRegex = new Regex(".*?{{(?<Name>.*?)}}.*?", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Singleline);

    public IList<TemplateMetaDataItem> GetMetaDataItems(string templateContent)
    {
      Check.NotNullOrEmpty(templateContent, nameof(templateContent));

      var matches = mPlaceholderMatchRegex.Matches(templateContent);

      return matches.Select(Convert).Where(x => x != null).DistinctBy(x => x.Name).ToArray();
    }

    public string TransformToText(string templateContent, IDictionary<string, string> placeholderValues)
    {
      foreach (var placeholderValue in placeholderValues)
      {
        templateContent = templateContent.Replace("{{" + placeholderValue.Key + "}}", placeholderValue.Value);
      }

      string transformedTemplate = new Markdown().Transform(templateContent);

      return transformedTemplate;
    }

    public string TransformToPdf(string templateContent, IDictionary<string, string> placeholderValues)
    {
      string htmlContent = TransformToText(templateContent, placeholderValues);

      DirectoryInfo directoryInfo = Directory.CreateDirectory("Path\\Temp");
      string fileName = $"report_{DateTime.Now:ddMMyyyHHmmss}.pdf";
      string filePath = Path.Combine(directoryInfo.FullName, fileName);
      
      var document = new iTextSharp.text.Document();
      using (var fileStream = new FileStream(filePath, FileMode.Create))
      {
        PdfWriter.GetInstance(document, fileStream);

        document.Open();
        var htmlWorker = new iTextSharp.text.html.simpleparser.HtmlWorker(document);
        htmlWorker.Parse(new StringReader(htmlContent));
        document.Close();
      }

      return filePath;
    }

    private static TemplateMetaDataItem Convert(Match match)
    {
      var group = match.Groups["Name"];

      if (group.Success)
      {
        return new TemplateMetaDataItem
        {
          Name = group.Value
        };
      }

      return null;
    }
  }
}
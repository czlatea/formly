using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Formly.Shared;
using Formly.Shared.Services;
using Framework;
using Framework.Extensions;
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

      return matches.Select(Convert).Where(x => x != null).DistinctBy(x=>x.Name).ToArray();
    }

    public string Transform(string templateContent, IDictionary<string, string> placeholderValues)
    {
      foreach (var placeholderValue in placeholderValues)
      {
        templateContent = templateContent.Replace("{{" + placeholderValue.Key + "}}", placeholderValue.Value);
      }

      string transformedTemplate = new Markdown().Transform(templateContent);

      return transformedTemplate;
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
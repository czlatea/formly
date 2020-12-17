using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Formly.Shared;
using Formly.Shared.Services;
using Framework;

namespace Formly.Server.Services
{
  public class TemplateMetaDataRetriever : ITemplateMetaDataRetriever
  {
    private static Regex _regex = new Regex(".*?{{(?<Name>.*?)}}.*?", RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase | RegexOptions.Singleline);

    public IList<TemplateMetaDataItem> GetMetaDataItems(string templateContent)
    {
      Check.NotNullOrEmpty(templateContent, nameof(templateContent));
      
      var matches = _regex.Matches(templateContent);

      return matches.Select(Convert).Where(x => x != null).ToArray();
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
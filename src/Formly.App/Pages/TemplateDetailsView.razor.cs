using System.Collections.Generic;
using System.Linq;
using Formly.Shared;
using Formly.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;

namespace Formly.App.Pages
{
  public partial class TemplateDetailsView
  {
    [Inject]
    public ITemplateService TemplateService { get; private set; }

    [Parameter]
    public string Id { get; set; }

    public string DownloadUrl => $"/api/Template{GetDownloadParameters()}";

    protected override void OnInitialized()
    {
      MetaData = TemplateService.GetTemplateMetaDataByExternalId(Id);
      TemplateDetails = TemplateService.GetTemplateDetailsByExternalId(Id);
    }

    public IList<TemplateMetaDataItem> MetaData { get; private set; }

    public TemplateDetails TemplateDetails { get; private set; }

    private string GetDownloadParameters()
    {
      var parameters = MetaData.Where(x => !string.IsNullOrWhiteSpace(x.Value)).ToDictionary(x => x.Name, x => x.Value);
      parameters.Add("id", TemplateDetails.Id.ToString());

      return QueryString.Create(parameters).Value;
    }
  }
}
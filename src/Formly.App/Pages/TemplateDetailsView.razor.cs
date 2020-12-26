using System.Collections.Generic;
using Formly.Shared;
using Formly.Shared.Services;
using Microsoft.AspNetCore.Components;

namespace Formly.App.Pages
{
  public partial class TemplateDetailsView
  {
    [Inject]
    public ITemplateService TemplateService { get; set; }

    [Inject]
    public ITemplateProcessor TemplateProcessor { get; set; }

    [Parameter]
    public long TemplateId { get; set; }
    
    
    protected override void OnInitialized()
    {
      MetaData = TemplateService.GetTemplateMetaData(TemplateId);
      TemplateDetails = TemplateService.GetTemplateDetails(TemplateId);
    }

    public IList<TemplateMetaDataItem> MetaData { get; private set; }
    
    public TemplateDetails TemplateDetails { get; private set; }
  }
}
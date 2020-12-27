using System.Collections.Generic;
using Formly.Shared;
using Formly.Shared.Services;
using Microsoft.AspNetCore.Components;

namespace Formly.App.Pages
{
  public partial class TemplatesList
  {
    [Inject]
    public ITemplateService TemplateService { get; set; }

    protected override void OnInitialized()
    {
      Templates = TemplateService.GetAllTemplates();
    }

    public IList<TemplateDetails> Templates { get; private set; }
  }
}
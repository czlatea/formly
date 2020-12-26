using System.Collections.Generic;
using Formly.Shared.Services;
using Framework;
using Microsoft.AspNetCore.Mvc;

namespace Formly.App.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TemplateController : ControllerBase
  {
    private readonly ITemplateService mTemplateService;
    private readonly ITemplateProcessor mTemplateProcessor;

    public TemplateController(ITemplateService templateService, ITemplateProcessor templateProcessor)
    {
      Check.Assigned(templateService, nameof(templateService));
      Check.Assigned(templateProcessor, nameof(templateProcessor));

      mTemplateService = templateService;
      mTemplateProcessor = templateProcessor;
    }

    [HttpGet]
    public IActionResult Get(long id)
    {
      string templateContent = mTemplateService.GetTemplateContent(id);

      IDictionary<string, string> placeholderValues = new Dictionary<string, string>()
      {
        ["FirstName"]="Bob",
        ["LastName"]="Emma"
      };
      
      string filePath = mTemplateProcessor.TransformToPdf(templateContent, placeholderValues);
      var fileContent = System.IO.File.ReadAllBytes(filePath);
      
      return File(fileContent, "application/octet-stream", System.IO.Path.GetFileName(filePath));
    }
  }
}

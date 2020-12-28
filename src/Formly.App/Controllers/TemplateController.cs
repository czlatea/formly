using System.Collections.Generic;
using System.Linq;
using Formly.Shared.Services;
using Framework;
using Microsoft.AspNetCore.Mvc;

namespace Formly.App.Controllers
{
  [Route("api/[controller]/[action]")]
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
    public IActionResult Display(long id)
    {
      IDictionary<string, string> placeholderValues = HttpContext.Request.Query.ToDictionary(kv => kv.Key, kv => kv.Value.ToString());
      string templateContent = mTemplateService.GetTemplateContent(id);

      string filePath = mTemplateProcessor.TransformToPdf(templateContent, placeholderValues);
      var fileContent = System.IO.File.ReadAllBytes(filePath);

      return File(fileContent, "application/pdf");
    }

    [HttpGet]
    public IActionResult Download(long id)
    {
      IDictionary<string, string> placeholderValues = HttpContext.Request.Query.ToDictionary(kv => kv.Key, kv => kv.Value.ToString());
      string templateContent = mTemplateService.GetTemplateContent(id);

      string filePath = mTemplateProcessor.TransformToPdf(templateContent, placeholderValues);
      var fileContent = System.IO.File.ReadAllBytes(filePath);

      return File(fileContent, "application/pdf", System.IO.Path.GetFileName(filePath));
    }
  }
}

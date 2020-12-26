using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace Formly.App.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TemplateController : ControllerBase
  {
    [HttpGet]
    public IActionResult Get(string fileName)
    {
      var content=Encoding.UTF8.GetBytes("this is a generated file");
      return File(content, "application/octet-stream", fileName);
    }
  }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace Formly.DataEntities
{
  [Table("Template")]
  public class TemplateDataEntity
  {
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Content { get; set; }
    public bool IsActive { get; set; }
  }
}

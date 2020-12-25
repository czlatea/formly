using Formly.DataEntities;
using Microsoft.EntityFrameworkCore;

namespace Formly.Server
{
  public class FormlyDbContext : DbContext
  {
    public FormlyDbContext(DbContextOptions<FormlyDbContext> options)
      : base(options)
    {
    }

    public DbSet<TemplateDataEntity> Templates { get; set; }
  }
}
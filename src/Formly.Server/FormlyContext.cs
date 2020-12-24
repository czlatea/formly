using Microsoft.EntityFrameworkCore;

namespace Formly.Server
{
  public class FormlyDbContext : DbContext
  {
    public FormlyDbContext(DbContextOptions<FormlyDbContext> options)
      : base(options)
    {
    }
  }
}
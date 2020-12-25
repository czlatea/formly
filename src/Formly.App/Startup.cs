using System.Reflection;
using DbUp;
using DbUp.Engine;
using DbUp.Helpers;
using Formly.DataAccess.Migrations;
using Formly.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Formly.App
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddRazorPages();
      services.AddServerSideBlazor();
      services.AddDbContext<FormlyDbContext>(options => options.UseSqlServer(GetConnectionString()));
      services.AddMemoryCache();

      services.AddFormly();

      RunDbUpdate();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseStaticFiles();

      app.UseRouting();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapBlazorHub();
        endpoints.MapFallbackToPage("/_Host");
      });
    }

    private void RunDbUpdate()
    {
      EnsureDatabase.For.SqlDatabase(GetConnectionString());
      Assembly dbMigrationAssembly = GetDbMigrationAssembly();
      UpgradeEngine upgradeEngine = GetUpgradeEngine(dbMigrationAssembly);

      upgradeEngine.PerformUpgrade();
    }

    private UpgradeEngine GetUpgradeEngine(Assembly dbMigrationAssembly)
    {
      var upgradeEngine = DeployChanges.To
        .SqlDatabase(GetConnectionString())
        .JournalTo(new NullJournal())
        .WithScriptsAndCodeEmbeddedInAssembly(dbMigrationAssembly)
        .LogToConsole()
        .Build();

      return upgradeEngine;
    }

    private static Assembly GetDbMigrationAssembly()
    {
      return typeof(IDatabaseMigration).Assembly;
    }

    private string GetConnectionString()
    {
      return Configuration.GetConnectionString("DefaultConnection");
    }
  }
}

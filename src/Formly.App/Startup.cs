using System.Reflection;
using DbUp;
using DbUp.Engine;
using Formly.DataAccess.Migrations;
using Formly.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Formly.App
{
  public class Startup
  {
    private readonly IConfiguration mConfiguration;

    public Startup(IConfiguration configuration)
    {
      mConfiguration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
      string connectionString = GetConnectionString();

      services.AddControllers();
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Formly API", Version = "v1" });
      });

      services.AddRazorPages();
      services.AddServerSideBlazor();
      services.AddDbContext<FormlyDbContext>(options => options.UseSqlServer(connectionString));
      services.AddMemoryCache();
      
      services.AddFormly();

      RunDbUpdate(connectionString);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Formly API v1"));
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

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
        endpoints.MapBlazorHub();
        endpoints.MapFallbackToPage("/_Host");
      });
    }

    private void RunDbUpdate(string connectionString)
    {
      DropDatabase.For.SqlDatabase(connectionString);
      EnsureDatabase.For.SqlDatabase(connectionString);

      Assembly dbMigrationAssembly = GetDbMigrationAssembly();
      UpgradeEngine upgradeEngine = GetUpgradeEngine(dbMigrationAssembly);

      upgradeEngine.PerformUpgrade();
    }

    private UpgradeEngine GetUpgradeEngine(Assembly dbMigrationAssembly)
    {
      var upgradeEngine = DeployChanges.To
        .SqlDatabase(GetConnectionString())
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
      return mConfiguration.GetConnectionString("DefaultConnection");
    }
  }
}

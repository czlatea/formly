using System;
using System.Linq;
using System.Threading.Tasks;
using Formly.Shared;
using Formly.Shared.Services;

namespace Formly.Server.Services
{
  internal class WeatherForecastService : IWeatherForecastService
  {
    private readonly FormlyDbContext mDbContext;
    private static readonly string[] Summaries = { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };

    public WeatherForecastService(FormlyDbContext dbContext)
    {
      mDbContext = dbContext;
    }
    
    public Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
    {
      var templates=mDbContext.Templates.ToList();
      Console.Out.WriteLine(templates.Count);
      
      var rng = new Random();
      return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
      {
        Date = startDate.AddDays(index),
        TemperatureC = rng.Next(-20, 55),
        Summary = Summaries[rng.Next(Summaries.Length)]
      }).ToArray());
    }
  }
}

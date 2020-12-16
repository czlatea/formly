using System;
using System.Threading.Tasks;

namespace Formly.Shared.Services
{
  public interface IWeatherForecastService
  {
    Task<WeatherForecast[]> GetForecastAsync(DateTime startDate);
  }
}
using System;
using System.Threading.Tasks;

namespace Formly.Shared
{
  public interface IWeatherForecastService
  {
    Task<WeatherForecast[]> GetForecastAsync(DateTime startDate);
  }
}
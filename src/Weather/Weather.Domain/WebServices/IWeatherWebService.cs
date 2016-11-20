using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Domain.WebServices
{
    public interface IWeatherWebService
    {
        // TODO: Ändra namn till WeatherWebservice.
        IEnumerable<Location> GetLocationsFromTextSearch(string query);
        //Location FindLocation(string query);
        IEnumerable<Forecast> GetLocationForecasts(Location location);
    }
}

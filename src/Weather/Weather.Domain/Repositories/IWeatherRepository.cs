using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Domain.Repositories
{
    public interface IWeatherRepository : IDisposable
    {
        // Locations
        Location GetLocationById(int id);
        void AddLocation(Location location);
        void UpdateLocation(Location location);
        void DeleteLocation(int id);
        IEnumerable<Location> GetLocationsBySearch(string query);
        bool locationExists(int id);
        void RemoveUnusedLocations();

        // Forecasts
        void AddForecast(Forecast forecast);
        void UpdateForecast(Forecast forecast);
        void RemoveForecast(int id);

        void Save();

    }
}

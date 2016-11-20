using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Domain
{
    public interface IWeatherService : IDisposable
    {
        Location GetLocation(int id);
        IEnumerable<Location> GetLocations(string query);
        void RefreshForecast(Location location);
    }
}

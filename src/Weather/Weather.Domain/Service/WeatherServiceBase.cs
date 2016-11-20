using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Domain
{
    public abstract class WeatherServiceBase : IWeatherService
    {
        public abstract Location GetLocation(int id);
        public abstract IEnumerable<Location> GetLocations(string query);
        public abstract void RefreshForecast(Location location);

        #region IDisposable Members

        protected virtual void Dispose(bool disposing)
        {
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true /* disposing */);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}

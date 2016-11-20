using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Domain.Repositories
{
    public abstract class WeatherRepositoryBase : IWeatherRepository
    {
        #region Locations

        protected abstract IQueryable<Location> QueryLocations();

        public Location GetLocationById(int id)
        {
            return QueryLocations().SingleOrDefault(l => l.LocationID == id);
        }

        public bool locationExists(int id)
        {
            return QueryLocations().Where(l => l.LocationID == id).ToList().Any();
        }

        public void RemoveUnusedLocations()
        {
            var locations = QueryLocations().ToList();

            foreach (var location in locations)
            {
                if (location.NextUpdate == null)
                {
                    DeleteLocation(location.LocationID);
                }
            }

        }

        public abstract void AddLocation(Location location);
        public abstract void UpdateLocation(Location location);
        public abstract void DeleteLocation(int id);

        public IEnumerable<Location> GetLocationsBySearch(string query)
        {
            return QueryLocations().Where(l => l.Name == query).ToList();
        }
        #endregion

        #region Forecasts 
        public abstract IQueryable<Forecast> QueryForecast();

        public abstract void AddForecast(Forecast forecast);
        public abstract void UpdateForecast(Forecast forecast);
        public abstract void RemoveForecast(int id);



        #endregion

        public abstract void Save();

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

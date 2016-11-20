using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Domain.Repositories
{
    public class WeatherRepository : WeatherRepositoryBase
    {
        private readonly WeatherEntities _context = new WeatherEntities();

        #region Location
        protected override IQueryable<Location> QueryLocations()
        {
            return _context.Locations.AsQueryable();
        }

        public override void AddLocation(Location location)
        {
            _context.Locations.Add(location);
        }

        public override void UpdateLocation(Location location)
        {
            _context.Entry(location).State = EntityState.Modified;
        }

        public override void DeleteLocation(int id)
        {
            Location location = _context.Locations.Find(id);
            _context.Locations.Remove(location);
        }
        #endregion

        #region Forecast
        public override IQueryable<Forecast> QueryForecast()
        {
            return _context.Forecasts.AsQueryable();
        }

        public override void AddForecast(Forecast forecast)
        {
            _context.Forecasts.Add(forecast);
        }

        public override void UpdateForecast(Forecast forecast)
        {
            _context.Entry(forecast).State = EntityState.Modified;
        }

        public override void RemoveForecast(int id)
        {
            Forecast forecast = _context.Forecasts.Find(id);
            _context.Forecasts.Remove(forecast);
        }
        #endregion

        #region Context
        public override void Save()
        {
            _context.SaveChanges();
        }
        #endregion

        #region IDisposable

        private bool _disposed = false;

        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;

            base.Dispose(disposing);
        }
        #endregion
    }
}

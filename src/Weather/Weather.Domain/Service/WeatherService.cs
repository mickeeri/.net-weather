using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Domain.Repositories;
using Weather.Domain.WebServices;

namespace Weather.Domain
{
    public class WeatherService : WeatherServiceBase
    {
        private IWeatherRepository _repository;
        private IWeatherWebService _webservice;

        public WeatherService(IWeatherRepository repository, IWeatherWebService webservice)
        {
            _repository = repository;
            _webservice = webservice;
        }

        public override Location GetLocation(int id)
        {
            return _repository.GetLocationById(id);
        }

        public override IEnumerable<Location> GetLocations(string query)
        {
            // Trying to get locations from database.
            IEnumerable<Location> locations = _repository.GetLocationsBySearch(query);

            // If there are no locations...
            if (locations.Any() == false)
            {
                // ... use web service to find locations. 
                locations = _webservice.GetLocationsFromTextSearch(query);

                // Save locations in repository.
                foreach (var location in locations)
                {
                    // TODO: Spara bara locations som användaren har hämtat väderdata för. Eller radera övriga.                    

                    // Check if entry with same GeoName-id already exists. 
                    if (!_repository.locationExists(location.LocationID))
                    {
                        _repository.AddLocation(location);
                        _repository.Save();
                    }
                }
            }
            return locations;
        }

        public override void RefreshForecast(Location location)
        {
            // If there are no weather data for location, or if it is time to update data.
            if (!location.Forecasts.Any() || location.NextUpdate < DateTime.Now)
            {
                // ... delete old data (if there is any)
                foreach (var forecast in location.Forecasts.ToList())
                {
                    _repository.RemoveForecast(forecast.ForecastID);
                }

                // then get forecasts from web service and insert them. 
                foreach (var forecast in _webservice.GetLocationForecasts(location))
                {
                    _repository.AddForecast(forecast);
                }

                // Cache data for 10 minutes according to yr api guidelines. 
                location.NextUpdate = DateTime.Now.AddMinutes(10);

                _repository.Save();
            }           
        }

        protected override void Dispose(bool disposing)
        {
            _repository.Dispose();
            base.Dispose(disposing);
        }
    }
}

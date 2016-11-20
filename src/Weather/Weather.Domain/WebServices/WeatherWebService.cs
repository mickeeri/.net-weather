using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Weather.Domain.WebServices
{
    public class WeatherWebService : IWeatherWebService
    {
        public IEnumerable<Location> GetLocationsFromTextSearch(string query)
        {
            try
            {
                string json;
                string geoNamesUserName = "me222wm";
                // URI som bara returnerar orter. Inte t.ex. flygplatser etc. 
                var uri = $"http://api.geonames.org/search?q={query}&maxRows=8&username={geoNamesUserName}&style=full&isNameRequired=true&featureClass=P&type=json";
                var request = (HttpWebRequest)WebRequest.Create(uri);

                using (var response = request.GetResponse())
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    json = reader.ReadToEnd();
                }

                JObject o = JObject.Parse(json);
                IEnumerable<Location> results = o["geonames"].Select(t => new Location(t)).ToList();

                return results;
            }
            catch (Exception)
            {
                throw new Exception("Fel när data skulle hämtas från GeoNames.");
            }     
        }

        public IEnumerable<Forecast> GetLocationForecasts(Location location)
        {

            try
            {
                var uri = $"http://www.yr.no/place/{location.Country}/{location.Province}/{location.Name}/forecast.xml";

                var document = XDocument.Load(uri);

                var weatherData = document.Descendants("weatherdata").FirstOrDefault();

                var apiLocation = weatherData.Element("location");

                //string credit = weatherData.Element("credit").Element("link").Attribute("text").Value;

                var tabular = weatherData.Element("forecast").Element("tabular");

                //var forecasts = tabular.Descendants("time");

                var forecasts = (from forecast in tabular.Descendants("time")
                                 select new Forecast
                                 {
                                     LocationID = location.LocationID,
                                     Location = location,
                                     ValidFrom = DateTime.Parse(forecast.Attribute("from").Value, CultureInfo.InvariantCulture),
                                     ValidTo = DateTime.Parse(forecast.Attribute("to").Value, CultureInfo.InvariantCulture),
                                     Period = int.Parse(forecast.Attribute("period").Value),
                                     Temperature = int.Parse(forecast.Element("temperature").Attribute("value").Value),
                                     SymbolSrc = String.Format("{0}.svg", forecast.Element("symbol").Attribute("var").Value),
                                     SymbolAlt = forecast.Element("symbol").Attribute("name").Value
                                 }).ToList();

                return forecasts;
            }
            catch (Exception)
            {
                throw new Exception("Fel uppstod när väderdata skulle hämtas från yr.no.");
            }     
        }
    }
}

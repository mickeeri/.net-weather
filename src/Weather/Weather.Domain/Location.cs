using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Domain
{
    public partial class Location
    {
        public Location(JToken token)
            : this()
        {
            LocationID = token.Value<int>("geonameId");
            Name = token.Value<string>("name");
            Country = token.Value<string>("countryName");
            Province = token.Value<string>("adminName1"); // Landskap
            Municipality = token.Value<string>("adminName2"); // Kommun
            Latitude = token.Value<double>("lat");
            Longitude = token.Value<double>("lng");
                     
            //LocationID = token.Value<string>("id");
            //Name = token.Value<string>("name");
            //FormattedAddress = token.Value<string>("formatted_address");

            ////// TODO: förbättra.
            ////Latitude = double.Parse(token["geometry"]["location"]["lat"].ToString());
            ////Longitude = double.Parse(token["geometry"]["location"]["lng"].ToString());                  
        }
    }
}

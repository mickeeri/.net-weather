using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Weather.Domain;

namespace Weather.Application.ViewModels
{
    public class LocationViewModel
    {
        [Required]
        [DisplayName("Ort")]
        [StringLength(50)]
        public string LocationSearchQuery { get; set; }

        public bool LocationExists { get; set; }

        public IEnumerable<Location> Locations { get; set; }

        public Location Location { get; set; }

        public string Name => Location?.Name ?? "[Unknown]";

        public IEnumerable<Forecast> Forecasts
        {
            get { return Location != null ? Location.Forecasts : null; }
        }

        public bool HasForecasts
        {
            get { return Forecasts != null && Forecasts.Any(); }
        }
    }
}
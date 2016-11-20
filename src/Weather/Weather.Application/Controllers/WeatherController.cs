using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Weather.Application.ViewModels;
using Weather.Domain.WebServices;
using Weather.Domain;

namespace Weather.Application.Controllers
{
    public class WeatherController : Controller
    {
        private IWeatherService _service;

        public WeatherController(IWeatherService service)
        {
            _service = service;
        }

        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
            base.Dispose(disposing);
        }

        // GET: Weather
        public ActionResult Index()
        {
            var location = TempData["location"];

            if (location != null)
            {
                return View(location);
            }

            return View(); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "LocationSearchQuery")]LocationViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.Locations = _service.GetLocations(model.LocationSearchQuery);

                    if (!model.Locations.Any())
                    {
                        model.LocationExists = false;
                    }
                    else if (model.Locations.Count() > 1)
                    {
                        model.LocationExists = true;
                    }
                    else
                    {
                        // Only one hit. Get weatherdata. 
                        model.Location = model.Locations.FirstOrDefault();
                        _service.RefreshForecast(model.Location);
                        model.LocationExists = true;
                    }
                }
            }
            catch (Exception ex)
            {                
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }

                TempData["error"] = ex.Message;
            }            

            return View(model);
        }

        // After user has choosen one location from search results. 
        public ActionResult ChooseLocation(int id)
        {
            try
            {
                LocationViewModel model = new LocationViewModel();
                model.Location = _service.GetLocation(id);
                _service.RefreshForecast(model.Location);
                model.LocationExists = true;
                TempData["location"] = model;
                
            }
            catch (Exception ex)
            {               
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }

                TempData["error"] = ex.Message;
            }

            return RedirectToAction("Index");
        }
    }
}
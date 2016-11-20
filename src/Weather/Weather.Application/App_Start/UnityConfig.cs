using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using Weather.Domain.Repositories;
using Weather.Domain.WebServices;
using Weather.Domain;

namespace Weather.Application
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
           
            container.RegisterType<IWeatherRepository, WeatherRepository>();
            container.RegisterType<IWeatherWebService, WeatherWebService>();
            container.RegisterType<IWeatherService, WeatherService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
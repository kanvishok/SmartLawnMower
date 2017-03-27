using RestBus.RabbitMQ;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using RestBus.RabbitMQ.Subscription;
using RestBus.WebApi;

namespace LawnMower.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        RestBusHost restbusHost = null;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configure(DependencyConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //*** Start RestBus subscriber/host **//

            var amqpUrl = "amqp://localhost:5672"; //AMQP URL for RabbitMQ installation
            var serviceName = "LawnMover"; //Uniquely identifies this service

            var msgMapper = new BasicMessageMapper(amqpUrl, serviceName);
            var subscriber = new RestBusSubscriber(msgMapper);
            restbusHost = new RestBusHost(subscriber, GlobalConfiguration.Configuration);
            restbusHost.Start();
        }
        protected void Application_End()
        {
            restbusHost?.Dispose();
        }
    }
}

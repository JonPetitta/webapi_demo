using BasicAuthForWebAPI;
using Microsoft.Practices.Unity;
using System.Web.Http;
using webapi.IOCResolver;
using webapi.Repository;
using webapi.MembershipProvider;

namespace webapi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //IOCResolver
            var container = new UnityContainer();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            config.DependencyResolver = new UnityResolver(container);

            // Web API configuration and services
            GlobalConfiguration.Configuration.MessageHandlers.Add(
              new BasicAuthenticationMessageHandler(
                  new MySqlMembershipProvider(
                          container.Resolve<IUnitOfWork>()))
              { IssueChallengeResponse = true });

            config.Filters.Add(new AuthorizeAttribute());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}

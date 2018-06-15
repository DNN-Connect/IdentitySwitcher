namespace DNN.Modules.IdentitySwitcher.Components
{
    using DotNetNuke.Web.Api;

    public class RouteMapper : IServiceRouteMapper
    {
        public void RegisterRoutes(IMapRoute mapRouteManager)
        {
            mapRouteManager.MapHttpRoute("Identity Switcher", "default", "{controller}/{action}",
                                         new[] {"DNN.Modules.IdentitySwitcher.Components"});
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DNN.Modules.IdentitySwitcher.Components
{
    using System.Web.Http.Controllers;
    using DotNetNuke.Framework.Reflections;
    using DotNetNuke.Web.Api;
    public class RouteMapper: IServiceRouteMapper
    {
        public void RegisterRoutes(IMapRoute mapRouteManager)
        {
            mapRouteManager.MapHttpRoute("Identity Switcher", "default", "{controller}/{action}", new[] { "DNN.Modules.IdentitySwitcher.Components" });
        }

    }
}
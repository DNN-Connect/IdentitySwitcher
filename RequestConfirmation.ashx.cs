using DotNetNuke.Services.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DNN.Modules.IdentitySwitcher
{
    /// <summary>
    /// Summary description for RequestConfirmation
    /// </summary>
    public class RequestConfirmation : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string SharedResourceFile = "~/DesktopModules/IdentitySwitcher/App_LocalResources/SharedResources.resx";

            string requestId = "";
            bool validRequest = false;

            if (context.Request.QueryString["id"] != null)
            {
                requestId = context.Request.QueryString["id"];

                validRequest = IdentitySwitcher.Controllers.IdentitySwitcherController.ApproveRequest(requestId);
            }


            if (validRequest)
            {
                context.Response.ContentType = "text/html";
                context.Response.Write(Localization.GetString("RequestApproved", SharedResourceFile));
            }
            else
            {
                context.Response.ContentType = "text/html";
                context.Response.Write(Localization.GetString("RequestInvalid", SharedResourceFile));
            }
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}

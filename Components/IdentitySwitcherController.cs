using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DNN.Modules.IdentitySwitcher.Components
{
    using System.Web.Http;
    using DotNetNuke.Common.Utilities;
    using DotNetNuke.Entities.Users;
    using DotNetNuke.Security;
    using DotNetNuke.Web.Api;

    public class IdentitySwitcherController: DnnApiController
    {
        [DnnAuthorize]
        [HttpGet]
        public IHttpActionResult SwitchUser()
        {
            var User = 2;
            var IdentityName = "host";
            var SelectedValue = "2";
            var UserHostAddress = "127.0.0.1";

            var MyUserInfo = UserController.GetUserById(this.PortalSettings.PortalId, int.Parse(SelectedValue));

            if (User != null)
            {
                DataCache.ClearUserCache(this.PortalSettings.PortalId, IdentityName);
            }

            // sign current user out
            var objPortalSecurity = new PortalSecurity();
            objPortalSecurity.SignOut();

            // sign new user in
            UserController.UserLogin(this.PortalSettings.PortalId, MyUserInfo, this.PortalSettings.PortalName,
                                     UserHostAddress, false);

            return this.Ok();
        }
    }
}
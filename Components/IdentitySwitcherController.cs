using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DNN.Modules.IdentitySwitcher.Components
{
    using System.Web.Http;
    using DotNetNuke.Common.Utilities;
    using DotNetNuke.Entities.Profile;
    using DotNetNuke.Entities.Users;
    using DotNetNuke.Security;
    using DotNetNuke.Web.Api;

    public class IdentitySwitcherController: DnnApiController
    {
        //private SortBy SortResultsBy
        //{
        //    get
        //        {
        //            var bRetValue = SortBy.DisplayName;
        //            if (this.Settings.Contains("sortBy"))
        //            {
        //                bRetValue = (SortBy)Enum.Parse(typeof(SortBy),
        //                                               Convert.ToString(this.Settings["sortBy"].ToString()));
        //            }
        //            return bRetValue;
        //        }
        //}


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

        [DnnAuthorize]
        [HttpGet]
        public IHttpActionResult GetSearchItems()
        {
            var result = new List<string>();

            var profileProperties = ProfileController.GetPropertyDefinitionsByPortal(this.PortalSettings.PortalId, false);

            foreach (ProfilePropertyDefinition definition in profileProperties)
            {
                result.Add(definition.PropertyName);
            }
            result.AddRange(new List<string> { "RoleName", "Email", "Username" });

            return this.Ok(result);
        }

        [DnnAuthorize]
        [HttpGet]
        public IHttpActionResult GetUsers(string searchText, string selectedSearchItem)
        {
            if (searchText == "")
            {
                this.LoadAllUsers();
            }
            else
            {
                //this.Filter(searchText, selectedSearchItem);
            }

            //return this.Ok(result);
            return null;
        }

        private void LoadAllUsers()
        {
            //var users = UserController.GetUsers(this.PortalId).OfType<UserInfo>().ToList();
            //this.BindUsers(users);

            //this.LoadDefaultUsers();
        }

        //private void BindUsers(IEnumerable<UserInfo> users)
        //{
        //    //this.cboUsers.Items.Clear();

        //    switch (this.SortResultsBy)
        //    {
        //        case SortBy.DisplayName:
        //            users = users.OrderBy(arg => arg.DisplayName.ToLower());
        //            break;
        //        case SortBy.UserName:
        //            users = users.OrderBy(arg => arg.Username.ToLower());
        //            break;
        //    }

        //    var display = "";
        //    foreach (var user in users)
        //    {
        //        if (this.SortResultsBy == SortBy.DisplayName)
        //        {
        //            display = string.Format("{0} - {1}", user.DisplayName, user.Username);
        //        }
        //        else
        //        {
        //            display = string.Format("{0} - {1}", user.Username, user.DisplayName);
        //        }
        //        this.cboUsers.Items.Add(new ListItem(display, user.UserID.ToString()));
        //    }
        //}
    }
}
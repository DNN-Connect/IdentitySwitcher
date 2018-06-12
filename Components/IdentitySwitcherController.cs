using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DNN.Modules.IdentitySwitcher.Components
{
    using System.Web.Http;
    using DotNetNuke.Common.Utilities;
    using DotNetNuke.Entities.Modules;
    using DotNetNuke.Entities.Profile;
    using DotNetNuke.Entities.Users;
    using DotNetNuke.Security;
    using DotNetNuke.Security.Roles;
    using DotNetNuke.Web.Api;

    public class IdentitySwitcherController: DnnApiController
    {
        private List<UserInfo> Users {get; set; }

        private int ModuleID { get; set; }

        //private bool IncludeHostUser
        //{
        //    get
        //        {
        //            var bRetValue = false;
        //            if (this.Settings.Contains("includeHost"))
        //            {
        //                bool.TryParse(Convert.ToString(this.Settings["includeHost"].ToString()), out bRetValue);
        //            }
        //            return bRetValue;
        //        }
        //}

        //private SortBy SortResultsBy
        //{
        //    get
        //    {
        //        var bRetValue = SortBy.DisplayName;

        //        var moduleInfo = new ModuleController().GetModule(this.ModuleID);

        //        var repository = new IdentitySwitcherModuleSettingsRepository();
        //        var settings = repository.GetSettings(moduleInfo);

        //        if (settings.SortBy != null)
        //        {
        //            bRetValue = (SortBy)Enum.Parse(typeof(SortBy),
        //                                           Convert.ToString(settings.SortBy));
        //        }

        //        //if (this.Settings.Contains("sortBy"))
        //        //{
        //        //    bRetValue = (SortBy)Enum.Parse(typeof(SortBy),
        //        //                                   Convert.ToString(this.Settings["sortBy"].ToString()));
        //        //}
        //        return bRetValue;
        //    }
        //}


        [DnnAuthorize]
        [HttpGet]
        public IHttpActionResult SwitchUser(int selectedUserId, string selectedUserUserName)
        {
            //var User = 2;
            //var IdentityName = "host";
            //var SelectedValue = "2";
            //var UserHostAddress = "127.0.0.1";

            var MyUserInfo = UserController.GetUserById(this.PortalSettings.PortalId, selectedUserId);

            if (selectedUserId != null)
            {
                DataCache.ClearUserCache(this.PortalSettings.PortalId, selectedUserUserName);
            }

            // sign current user out
            var objPortalSecurity = new PortalSecurity();
            objPortalSecurity.SignOut();

            //HttpContext.Current.Request.UserHostAddress

            // sign new user in
            UserController.UserLogin(this.PortalSettings.PortalId, MyUserInfo, this.PortalSettings.PortalName,
                                     HttpContext.Current.Request.UserHostAddress, false);

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
        public IHttpActionResult GetUsers(int moduleId, string searchText = null, string selectedSearchItem = null)
        {
            this.ModuleID = moduleId;

            if (searchText == null)
            {
                this.LoadAllUsers();
            }
            else
            {
                this.Filter(searchText, selectedSearchItem);
            }

            var result = this.Users.Select(userInfo => new UserDto
                                                           {
                                                               Id = userInfo.UserID,
                                                               UserName = userInfo.Username,
                                                               UserAndDisplayName = userInfo.DisplayName != null ? $"{userInfo.DisplayName} - {userInfo.Username}"
                                                                                        : userInfo.Username,
            })
                             .ToList();
  
            return this.Ok(result);
        }

        private void LoadAllUsers()
        {
            this.Users = UserController.GetUsers(this.PortalSettings.PortalId).OfType<UserInfo>().ToList();
            this.SortUsers();

            this.LoadDefaultUsers();
        }

        private void LoadDefaultUsers()
        {
            var moduleInfo = new ModuleController().GetModule(this.ModuleID);
            var repository = new IdentitySwitcherModuleSettingsRepository();
            var settings = repository.GetSettings(moduleInfo);

            if (settings.IncludeHost != null && (bool)settings.IncludeHost)
            {
                var arHostUsers = UserController.GetUsers(Null.NullInteger);

                foreach (UserInfo hostUser in arHostUsers)
                {
                    this.Users.Insert(0, new UserInfo {Username = hostUser.Username, UserID = hostUser.UserID, DisplayName = null});
                }
            }

            this.Users.Insert(0, new UserInfo {Username = "Anonymous", DisplayName = null} );
        }

        private void SortUsers()
        {
            var moduleInfo = new ModuleController().GetModule(this.ModuleID);
            var repository = new IdentitySwitcherModuleSettingsRepository();
            var settings = repository.GetSettings(moduleInfo);

            switch (settings.SortBy)
            {
                case SortBy.DisplayName:
                    this.Users.OrderBy(arg => arg.DisplayName.ToLower());
                    break;
                case SortBy.UserName:
                    this.Users.OrderBy(arg => arg.Username.ToLower());
                    break;
            }

            //var display = "";
            //foreach (var user in users)
            //{
            //    if (this.SortResultsBy == SortBy.DisplayName)
            //    {
            //        display = string.Format("{0} - {1}", user.DisplayName, user.Username);
            //    }
            //    else
            //    {
            //        display = string.Format("{0} - {1}", user.Username, user.DisplayName);
            //    }
            //    //this.cboUsers.Items.Add(new ListItem(display, user.UserID.ToString()));
            //}
        }

        private void Filter(string searchText, string selectedSearchItem)
        {
            //var users = default(List<UserInfo>);
            var total = 0;

            switch (selectedSearchItem)
            {
                case "Email":
                    this.Users = UserController.GetUsersByEmail(this.PortalSettings.PortalId, searchText + "%", -1, -1, ref total)
                                          .OfType<UserInfo>().ToList();
                    break;
                case "Username":
                    this.Users = UserController.GetUsersByUserName(this.PortalSettings.PortalId, searchText + "%", -1, -1, ref total)
                                          .OfType<UserInfo>().ToList();
                    break;
                case "RoleName":
                    this.Users = RoleController.Instance.GetUsersByRole(this.PortalSettings.PortalId, searchText).ToList();
                    break;

                default:
                    this.Users = UserController
                        .GetUsersByProfileProperty(this.PortalSettings.PortalId, selectedSearchItem, searchText + "%", 0, 1000, ref total)
                        .OfType<UserInfo>().ToList();
                    break;
            }
            this.SortUsers();

            this.LoadDefaultUsers();
        }
    }
}
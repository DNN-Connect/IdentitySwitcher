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
    using DotNetNuke.Security.Roles;
    using DotNetNuke.Web.Api;

    public class IdentitySwitcherController: DnnApiController
    {
        private IEnumerable<UserInfo> Users {get; set; }

        private SortBy SortResultsBy
        {
            get
            {
                var bRetValue = SortBy.DisplayName;

                var moduleId = this.ActiveModule.ModuleID;

                var repository = new IdentitySwitcherModuleSettingsRepository();
                var settings = repository.GetSettings(this.ActiveModule);

                if (settings.SortBy != null)
                {
                    bRetValue = (SortBy)Enum.Parse(typeof(SortBy),
                                                   Convert.ToString(settings.SortBy));
                }

                //if (this.Settings.Contains("sortBy"))
                //{
                //    bRetValue = (SortBy)Enum.Parse(typeof(SortBy),
                //                                   Convert.ToString(this.Settings["sortBy"].ToString()));
                //}
                return bRetValue;
            }
        }


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
            if (searchText == null)
            {
                this.LoadAllUsers();
            }
            else
            {
                this.Filter(searchText, selectedSearchItem);
            }

            var result = new List<UserDto>();

            foreach (UserInfo userInfo in this.Users)
            {
                var dto = new UserDto
                              {
                                  Name = userInfo.Username,
                                  Id = userInfo.UserID
                              };
                result.Add(dto);
            }


            //return this.Ok(result);
            return this.Ok(result);
        }

        private void LoadAllUsers()
        {
            this.Users = UserController.GetUsers(this.PortalSettings.PortalId).OfType<UserInfo>().ToList();
            this.SortUsers();
        }

        private void SortUsers()
        {
            switch (this.SortResultsBy)
            {
                case SortBy.DisplayName:
                    this.Users = this.Users.OrderBy(arg => arg.DisplayName.ToLower());
                    break;
                case SortBy.UserName:
                    this.Users = this.Users.OrderBy(arg => arg.Username.ToLower());
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

            //this.LoadDefaultUsers();
        }
    }
}
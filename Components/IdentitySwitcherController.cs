#region Copyright

// 
// DotNetNuke® - http://www.dotnetnuke.com
// Copyright (c) 2002-2018
// by DotNetNuke Corporation
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
// the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
// to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions 
// of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.
//

#endregion

namespace DNN.Modules.IdentitySwitcher.Components
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Http;
    using DNN.Modules.IdentitySwitcher.Components.Model;
    using DotNetNuke.Common;
    using DotNetNuke.Common.Utilities;
    using DotNetNuke.Entities.Modules;
    using DotNetNuke.Entities.Profile;
    using DotNetNuke.Entities.Users;
    using DotNetNuke.Security;
    using DotNetNuke.Security.Roles;
    using DotNetNuke.Web.Api;

    /// <summary>
    /// </summary>
    /// <seealso cref="DotNetNuke.Web.Api.DnnApiController" />
    public class IdentitySwitcherController : DnnApiController
    {
        /// <summary>
        ///     Switches the user.
        /// </summary>
        /// <param name="selectedUserId">The selected user identifier.</param>
        /// <param name="selectedUserName">Name of the selected user user.</param>
        /// <returns></returns>
        [DnnAuthorize]
        [HttpPost]
        public IHttpActionResult SwitchUser(int selectedUserId, string selectedUserName)
        {
            if (selectedUserId == -1)
            {
                HttpContext.Current.Response.Redirect(Globals.NavigateURL("LogOff"));
            }
            else
            {
                var UserInfo = UserController.GetUserById(this.PortalSettings.PortalId, selectedUserId);


                DataCache.ClearUserCache(this.PortalSettings.PortalId, selectedUserName);


                // sign current user out
                var objPortalSecurity = new PortalSecurity();
                objPortalSecurity.SignOut();

                // sign new user in
                UserController.UserLogin(this.PortalSettings.PortalId, UserInfo, this.PortalSettings.PortalName,
                                         HttpContext.Current.Request.UserHostAddress, false);
            }
            return this.Ok();
        }

        /// <summary>
        ///     Gets the search items.
        /// </summary>
        /// <returns></returns>
        [DnnAuthorize]
        [HttpGet]
        public IHttpActionResult GetSearchItems()
        {
            var result = new List<string>();

            var profileProperties =
                ProfileController.GetPropertyDefinitionsByPortal(this.PortalSettings.PortalId, false);

            foreach (ProfilePropertyDefinition definition in profileProperties)
            {
                result.Add(definition.PropertyName);
            }
            result.AddRange(new List<string> {"RoleName", "Email", "Username"});

            return this.Ok(result);
        }

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <param name="searchText">The search text.</param>
        /// <param name="selectedSearchItem">The selected search item.</param>
        /// <returns></returns>
        [DnnAuthorize]
        [HttpGet]
        public IHttpActionResult GetUsers(string searchText = null, string selectedSearchItem = null)
        {
            var users = searchText == null ? this.GetAllUsers() : this.GetFilteredUsers(searchText, selectedSearchItem);
            users = this.SortUsers(users);
            this.AddDefaultUsers(users);

            var result = users.Select(userInfo => new UserDto
                                                           {
                                                               Id = userInfo.UserID,
                                                               UserName = userInfo.Username,
                                                               UserAndDisplayName = userInfo.DisplayName != null
                                                                                        ? $"{userInfo.DisplayName} - {userInfo.Username}"
                                                                                        : userInfo.Username
                                                           })
                             .ToList();

            return this.Ok(result);
        }

        /// <summary>
        ///     Loads all users.
        /// </summary>
        private List<UserInfo> GetAllUsers()
        {
            var users = UserController.GetUsers(this.PortalSettings.PortalId).OfType<UserInfo>().ToList();

            return users;
        }

        /// <summary>
        ///     Loads the default users.
        /// </summary>
        private void AddDefaultUsers(List<UserInfo> users)
        {
            var repository = new IdentitySwitcherModuleSettingsRepository();
            var settings = repository.GetSettings(this.ActiveModule);
           
            if (settings.IncludeHost ?? false)
            {
                var hostUsers = UserController.GetUsers(false, true, Null.NullInteger);

                foreach (UserInfo hostUser in hostUsers)
                {
                    users.Insert(
                        0,
                        new UserInfo {Username = hostUser.Username, UserID = hostUser.UserID, DisplayName = null});
                }
            }

            users.Insert(0, new UserInfo {Username = "Anonymous", DisplayName = null});
        }

        /// <summary>
        ///     Sorts the users.
        /// </summary>
        private List<UserInfo> SortUsers(List<UserInfo> users)
        {
            var repository = new IdentitySwitcherModuleSettingsRepository();
            var settings = repository.GetSettings(this.ActiveModule);

            switch (settings.SortBy)
            {
                case SortBy.DisplayName:
                    users = users.OrderBy(arg => arg.DisplayName.ToLower()).ToList();
                    break;
                case SortBy.UserName:
                    users = users.OrderBy(arg => arg.Username.ToLower()).ToList();
                    break;
            }

            return users;
        }

        /// <summary>
        /// Gets the filtered users.
        /// </summary>
        /// <param name="searchText">The search text.</param>
        /// <param name="selectedSearchItem">The selected search item.</param>
        /// <returns></returns>
        private List<UserInfo> GetFilteredUsers(string searchText, string selectedSearchItem)
        {
            var total = 0;

            var users = default(List<UserInfo>);

            switch (selectedSearchItem)
            {
                case "Email":
                   users = UserController
                        .GetUsersByEmail(this.PortalSettings.PortalId, searchText + "%", -1, -1, ref total)
                        .OfType<UserInfo>().ToList();
                    break;
                case "Username":
                    users = UserController
                        .GetUsersByUserName(this.PortalSettings.PortalId, searchText + "%", -1, -1, ref total)
                        .OfType<UserInfo>().ToList();
                    break;
                case "RoleName":
                    users = RoleController
                        .Instance.GetUsersByRole(this.PortalSettings.PortalId, searchText).ToList();
                    break;

                default:
                    users = UserController
                        .GetUsersByProfileProperty(this.PortalSettings.PortalId, selectedSearchItem, searchText + "%",
                                                   0, 1000, ref total)
                        .OfType<UserInfo>().ToList();
                    break;
            }

            return users;
        }
    }
}
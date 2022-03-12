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

namespace DNN.Modules.IdentitySwitcher.Controllers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Http;
    using DNN.Modules.IdentitySwitcher.Model;
    using DNN.Modules.IdentitySwitcher.ModuleSettings;
    using DotNetNuke.Common;
    using DotNetNuke.Common.Utilities;
    using DotNetNuke.Data;
    using DotNetNuke.Entities.Modules;
    using DotNetNuke.Entities.Portals;
    using DotNetNuke.Entities.Profile;
    using DotNetNuke.Entities.Users;
    using DotNetNuke.Security;
    using DotNetNuke.Security.Permissions;
    using DotNetNuke.Security.Roles;
    using DotNetNuke.Services.Exceptions;
    using DotNetNuke.Services.Localization;
    using DotNetNuke.Services.Mail;
    using DotNetNuke.Services.Tokens;
    using DotNetNuke.Web.Api;

    /// <summary>
    /// </summary>
    /// <seealso cref="DotNetNuke.Web.Api.DnnApiController" />
    public class IdentitySwitcherController : DnnApiController
    {
        #region Implementation of IdentitySwitcherController
        /// <summary>
        ///     Gets the search items.
        /// </summary>
        /// <returns></returns>
        [SupportedModules("IdentitySwitcher")]
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult GetSearchItems()
        {
            var result = default(IHttpActionResult);

            if (!CheckSecurity(ActiveModule, UserInfo))
            {
                Exceptions.LogException(new Exception(String.Format("IdentitySwitcher module access without proper security context. " +
                    "ModuleId: {0} - TabId: {1} - PortalId: {2} - UserId: {3} - IP: {4}",
                    ActiveModule.ModuleID,
                    ActiveModule.TabID,
                    ActiveModule.PortalID,
                    UserInfo.UserID,
                    HttpContext.Current.Request.UserHostAddress
                    )));
                return result;
            }

            // Obtain the properties of each user profile and return these for the user to search by.
            try
            {
                var resultData = new List<string>();

                var profileProperties =
                    ProfileController.GetPropertyDefinitionsByPortal(PortalSettings.PortalId, false);

                resultData.AddRange(new List<string> { "RoleName", "Email", "Username", "FirstName", "LastName", "DisplayName" });

                foreach (ProfilePropertyDefinition definition in profileProperties)
                {
                    if (!resultData.Contains(definition.PropertyName))
                        resultData.Add(definition.PropertyName);
                }

                result = Ok(resultData);
            }
            catch (Exception exception)
            {
                Exceptions.LogException(exception);

                result = InternalServerError(exception);
            }

            return result;
        }

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <param name="searchText">The search text.</param>
        /// <param name="selectedSearchItem">The selected search item.</param>
        /// <param name="onlyDefault">if set to <c>true</c> [only default].</param>
        /// <returns></returns>
        [SupportedModules("IdentitySwitcher")]
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult GetUsers(string searchText = null, string selectedSearchItem = null, bool onlyDefault = false)
        {
            var result = default(IHttpActionResult);

            if (!CheckSecurity(ActiveModule, UserInfo))
            {
                Exceptions.LogException(new Exception(String.Format("IdentitySwitcher module access without proper security context. " +
                    "ModuleId: {0} - TabId: {1} - PortalId: {2} - UserId: {3} - IP: {4}",
                    ActiveModule.ModuleID,
                    ActiveModule.TabID,
                    ActiveModule.PortalID,
                    UserInfo.UserID,
                    HttpContext.Current.Request.UserHostAddress
                    )));
                return result;
            }

            var repository = new IdentitySwitcherModuleSettingsRepository();
            var settings = repository.GetSettings(ActiveModule);

            try
            {
                List<UserInfo> usersList = new List<UserInfo>();

                // Get only the default users or..
                if (!onlyDefault)
                {
                    // ..get all users if no searchtext is provided or filtered users if a searchtext is provided.
                    usersList = searchText == null
                        ? GetAllUsers()
                        : GetFilteredUsers(searchText, selectedSearchItem);
                }

                List<UserInfo> usersInfo = new List<UserInfo>();
                if (settings.IncludeAdmin.GetValueOrDefault())
                {
                    usersInfo.AddRange(usersList);
                }
                else
                {
                    foreach (UserInfo user in usersList)
                    {
                        if (!user.IsInRole(PortalSettings.AdministratorRoleName))
                            usersInfo.Add(user);
                    }
                }
                usersInfo = SortUsers(usersInfo);
                AddDefaultUsers(usersInfo);

                var selectedUserId = UserInfo.UserID;

                var resultData = new UserCollectionDto
                {
                    Users = usersInfo.Select(userInfo => new UserDto
                    {
                        Id = userInfo.UserID,
                        UserName = userInfo.Username,
                        UserAndDisplayName = userInfo.DisplayName != null
                                ? $"{userInfo.DisplayName} - {userInfo.Username}"
                                : userInfo.Username
                    }).ToList(),
                    SelectedUserId = selectedUserId
                };

                result = Ok(resultData);
            }
            catch (Exception exception)
            {
                Exceptions.LogException(exception);

                result = InternalServerError(exception);
            }

            return result;
        }

        /// <summary>
        ///     Switches the user.
        /// </summary>
        /// <param name="selectedUserId">The selected user identifier.</param>
        /// <param name="selectedUserName">Name of the selected user user.</param>
        /// <returns></returns>
        [SupportedModules("IdentitySwitcher")]
        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult SwitchUser(int selectedUserId, string selectedUserName)
        {
            var result = default(IHttpActionResult);

            if (!CheckSecurity(ActiveModule, UserInfo))
            {
                Exceptions.LogException(new Exception(String.Format("IdentitySwitcher module access without proper security context. " +
                    "ModuleId: {0} - TabId: {1} - PortalId: {2} - UserId: {3} - IP: {4}",
                    ActiveModule.ModuleID,
                    ActiveModule.TabID,
                    ActiveModule.PortalID,
                    UserInfo.UserID,
                    HttpContext.Current.Request.UserHostAddress
                    )));
                return result;
            }

            try
            {
                // Log request
                RequestLog requestLog = RequestLog(selectedUserId);

                // Request approval if required
                var repository = new IdentitySwitcherModuleSettingsRepository();
                var settings = repository.GetSettings(ActiveModule);
                if (settings.RequestAuthorization.GetValueOrDefault() && selectedUserId != -1)
                {
                    SendRequestAuthorization(requestLog);
                }
                else
                {
                    if (selectedUserId == -1)
                    {
                        HttpContext.Current.Response.Redirect(Globals.NavigateURL("LogOff"));
                    }
                    else
                    {
                        UserInfo selectedUser = UserController.GetUserById(PortalSettings.PortalId, selectedUserId);
                        ExecuteSwitchUser(selectedUser);
                    }
                }
                result = Ok(new
                {
                    requestAuthorization = settings.RequestAuthorization.GetValueOrDefault(),
                    requestId = requestLog.Id
                });
            }
            catch (Exception exception)
            {
                Exceptions.LogException(exception);

                result = InternalServerError(exception);
            }

            return result;
        }

        /// <summary>
        /// Checks status of the request.
        /// </summary>
        /// <param name="id">Id of the request.</param>
        /// <returns></returns>
        [SupportedModules("IdentitySwitcher")]
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult CheckStatus(int id)
        {
            var result = default(IHttpActionResult);

            if (!CheckSecurity(ActiveModule, UserInfo))
            {
                Exceptions.LogException(new Exception(String.Format("IdentitySwitcher module access without proper security context. " +
                    "ModuleId: {0} - TabId: {1} - PortalId: {2} - UserId: {3} - IP: {4}",
                    ActiveModule.ModuleID,
                    ActiveModule.TabID,
                    ActiveModule.PortalID,
                    UserInfo.UserID,
                    HttpContext.Current.Request.UserHostAddress
                    )));
                return result;
            }

            try
            {
                bool approved = false;

                using (IDataContext ctx = DataContext.Instance())
                {
                    var rep = ctx.GetRepository<RequestLog>();
                    RequestLog log = rep.Find("WHERE Id = @0 ", id).FirstOrDefault();

                    // We allow the identity switch if the request is valid as:
                    // - user is the same that initiated the request
                    // - it is on the same IP
                    // - request is approved
                    // - approval date is not older than 1 hour
                    if (log != null &&
                        log.RequestByUserId == UserInfo.UserID &&
                        log.RequestIP == HttpContext.Current.Request.UserHostAddress &&
                        log.ApprovalDate.HasValue &&
                        DateTime.Now < log.ApprovalDate.Value.AddHours(1))
                    {
                        UserInfo selectedUser = UserController.GetUserById(PortalSettings.PortalId, log.SwitchToUserId);
                        ExecuteSwitchUser(selectedUser);

                        approved = true;
                    }
                }
                result = Ok(approved);
            }
            catch (Exception exception)
            {
                Exceptions.LogException(exception);

                result = InternalServerError(exception);
            }

            return result;
        }

        #endregion

        #region Private methods
        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns></returns>
        private List<UserInfo> GetAllUsers()
        {
            var users = UserController.GetUsers(PortalSettings.PortalId).OfType<UserInfo>().ToList();

            return users;
        }

        /// <summary>
        ///     Loads the default users.
        /// </summary>
        private void AddDefaultUsers(List<UserInfo> users)
        {
            var repository = new IdentitySwitcherModuleSettingsRepository();
            var settings = repository.GetSettings(ActiveModule);

            // If includehost setting is set to true, add host users to the list.
            if (settings.IncludeHost.GetValueOrDefault())
            {
                var hostUsers = UserController.GetUsers(false, true, Null.NullInteger);

                foreach (UserInfo hostUser in hostUsers)
                {
                    users.Insert(
                        0,
                        new UserInfo { Username = hostUser.Username, UserID = hostUser.UserID, DisplayName = null });
                }
            }

            users.Insert(0, new UserInfo { Username = "Anonymous", DisplayName = null });
        }

        /// <summary>
        ///     Sorts the users.
        /// </summary>
        private List<UserInfo> SortUsers(List<UserInfo> users)
        {
            var repository = new IdentitySwitcherModuleSettingsRepository();
            var settings = repository.GetSettings(ActiveModule);

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

            // Sort based on the selected search item.
            switch (selectedSearchItem)
            {
                case "Email":
                    users = UserController
                         .GetUsersByEmail(PortalSettings.PortalId, searchText + "%", -1, -1, ref total)
                         .OfType<UserInfo>().ToList();
                    break;
                case "Username":
                    users = UserController
                        .GetUsersByUserName(PortalSettings.PortalId, searchText + "%", -1, -1, ref total)
                        .OfType<UserInfo>().ToList();
                    break;
                case "RoleName":
                    users = RoleController
                        .Instance.GetUsersByRole(PortalSettings.PortalId, searchText).ToList();
                    break;

                default:
                    users = UserController
                        .GetUsersByProfileProperty(PortalSettings.PortalId, selectedSearchItem, searchText + "%",
                                                   0, 1000, ref total)
                        .OfType<UserInfo>().ToList();
                    break;
            }

            return users;
        }

        /// <summary>
        /// Adds a log of the impersonation action
        /// </summary>
        /// <param name="userIdToImpersonate"></param>
        private RequestLog RequestLog(int userIdToImpersonate)
        {
            RequestLog log = new RequestLog();

            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<RequestLog>();
                log.RequestId = Guid.NewGuid().ToString();
                log.RequestByUserId = UserInfo.UserID;
                log.RequestDate = DateTime.Now;
                log.RequestIP = HttpContext.Current.Request.UserHostAddress;
                log.SwitchToUserId = userIdToImpersonate;

                rep.Insert(log);
            }

            return log;
        }

        /// <summary>
        /// Sends a request authorization to the user that is being impersonated
        /// </summary>
        /// <param name="requestLog"></param>
        private void SendRequestAuthorization(RequestLog requestLog)
        {
            string SharedResourceFile = "~/DesktopModules/IdentitySwitcher/App_LocalResources/SharedResources.resx";

            string subject = Localization.GetString("RequestSubject", SharedResourceFile);
            string body = Localization.GetString("RequestBody", SharedResourceFile);

            UserInfo user = UserController.GetUserById(PortalSettings.PortalId, requestLog.SwitchToUserId);

            ArrayList parameters = new ArrayList
            {
                user.DisplayName,
                UserInfo.DisplayName,
                UserInfo.Email,
                user.Username,
                requestLog.RequestDate.ToString(),
                requestLog.RequestId
            };

            var tokenizer = new TokenReplace();
            subject = tokenizer.ReplaceEnvironmentTokens(subject);
            body = tokenizer.ReplaceEnvironmentTokens(body, parameters, "Custom");

            Mail.SendEmail(PortalSettings.Email, user.Email, subject, body);
        }

        private bool CheckSecurity(ModuleInfo module, UserInfo user)
        {
            bool isValid = true;

            // module is of proper type
            if (module.DesktopModule.FriendlyName != "IdentitySwitcher")
                isValid = false;

            // user has permissions on the page
            if (!TabPermissionController.CanViewPage(module.ParentTab))
                isValid = false;

            // user has permissions on the module
            if(!ModulePermissionController.CanViewModule(module))
                isValid = false;

            return isValid;
        }

        private void ExecuteSwitchUser(UserInfo selectedUser)
        {
            var repository = new IdentitySwitcherModuleSettingsRepository();
            var settings = repository.GetSettings(ActiveModule);

            if (!settings.IncludeHost.GetValueOrDefault() && selectedUser.IsSuperUser)
                return;

            if (!settings.IncludeAdmin.GetValueOrDefault() && selectedUser.IsInRole(PortalSettings.AdministratorRoleName))
                return;

            DataCache.ClearUserCache(PortalSettings.PortalId, selectedUser.Username);

            // Sign current user out.
            var objPortalSecurity = new PortalSecurity();
            objPortalSecurity.SignOut();

            // Sign new user in.
            UserController.UserLogin(PortalSettings.PortalId, selectedUser, PortalSettings.PortalName,
                HttpContext.Current.Request.UserHostAddress, false);
        }
        #endregion

        public static bool ApproveRequest(string requestId)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<RequestLog>();
                RequestLog log = rep.Find("WHERE RequestId = @0 ", requestId).FirstOrDefault();

                if (log != null)
                {
                    PortalSettings ps = PortalController.Instance.GetCurrentPortalSettings();
                    log.ApprovalByUserId = ps.UserInfo.UserID;
                    log.ApprovalDate = DateTime.Now;
                    log.ApprovalIP = HttpContext.Current.Request.UserHostAddress;
                    rep.Update(log);

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
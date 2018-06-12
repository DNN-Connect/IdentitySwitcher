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


namespace DNN.Modules.IdentitySwitcher
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;
    using DNN.Modules.IdentitySwitcher.Components;
    using DNN.Modules.IdentitySwitcher.Components.Model;
    using DotNetNuke.Common;
    using DotNetNuke.Common.Utilities;
    using DotNetNuke.Entities.Modules;
    using DotNetNuke.Entities.Profile;
    using DotNetNuke.Entities.Users;
    using DotNetNuke.Framework;
    using DotNetNuke.Security;
    using DotNetNuke.Security.Roles;
    using DotNetNuke.Services.Exceptions;
    using DotNetNuke.Services.Localization;
    using global::IdentitySwitcher.DotNetNuke.Web.Client;

    /// -----------------------------------------------------------------------------
    /// <summary>
    ///     The ViewDynamicModule class displays the content
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <history>
    /// </history>
    /// -----------------------------------------------------------------------------
    [DNNtc.ModuleControlProperties("", "IdentitySwitcher", DNNtc.ControlType.View, "", true, false)]
    public partial class ViewIdentitySwitcher : IdentitySwitcherPortalModuleBase
    {
        #region Private Properties

        /// <summary>
        ///     reads the setting for inclusion of the host user. This setting defaults to false
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        private bool IncludeHostUser
        {
            get
            {
                var bRetValue = false;
                if (this.Settings.Contains("includeHost"))
                {
                    bool.TryParse(Convert.ToString(this.Settings["includeHost"].ToString()), out bRetValue);
                }
                return bRetValue;
            }
        }

        private bool UseAjax
        {
            get
            {
                var bRetValue = true;
                if (this.Settings.Contains("useAjax"))
                {
                    bool.TryParse(Convert.ToString(this.Settings["useAjax"].ToString()), out bRetValue);
                }
                return bRetValue;
            }
        }

        private SortBy SortResultsBy
        {
            get
            {
                var bRetValue = SortBy.DisplayName;
                if (this.Settings.Contains("sortBy"))
                {
                    bRetValue = (SortBy)Enum.Parse(typeof(SortBy),
                                                    Convert.ToString(this.Settings["sortBy"].ToString()));
                }
                return bRetValue;
            }
        }

        #endregion

        #region Private Methods

        protected virtual void InitializeModuleInstanceJson(HtmlGenericControl initControl)
        {
            if (initControl != null)
            {
                string initScript = String.Format(CultureInfo.InvariantCulture, "vm.init({0})",
                                                  this.GetModuleInstance().ToJson());

                initControl.Attributes.Add("ng-init", initScript);
            }
        }

        private ListItem AddSearchItem(string name)
        {
            var propertyName = Null.NullString;
            if (!ReferenceEquals(this.Request.QueryString["filterProperty"], null))
            {
                propertyName = this.Request.QueryString["filterProperty"];
            }

            var text = Localization.GetString(name, this.LocalResourceFile);
            if (string.IsNullOrEmpty(text))
            {
                text = name;
            }
            var li = new ListItem(text, name);
            if (name == propertyName)
            {
                li.Selected = true;
            }
            return li;
        }

        //private void BindSearchOptions()
        //{
        //    this.ddlSearchType.Items.Add(this.AddSearchItem("RoleName"));
        //    this.ddlSearchType.Items.Add(this.AddSearchItem("Email"));
        //    this.ddlSearchType.Items.Add(this.AddSearchItem("Username"));
        //    var profileProperties = ProfileController.GetPropertyDefinitionsByPortal(this.PortalId, false);

        //    foreach (ProfilePropertyDefinition definition in profileProperties)
        //    {
        //        this.ddlSearchType.Items.Add(this.AddSearchItem(definition.PropertyName));
        //    }
        //}

        //private void LoadDefaultUsers()
        //{
        //    if (this.IncludeHostUser)
        //    {
        //        var arHostUsers = UserController.GetUsers(Null.NullInteger);
        //        foreach (UserInfo hostUser in arHostUsers)
        //        {
        //            this.cboUsers.Items.Insert(0, new ListItem(hostUser.Username, hostUser.UserID.ToString()));
        //        }
        //    }
        //    this.cboUsers.Items.Insert(
        //        0,
        //        new ListItem(Localization.GetString("Anonymous", this.LocalResourceFile),
        //                     Null.NullInteger.ToString()));
        //}

        //private void LoadAllUsers()
        //{
        //    var users = UserController.GetUsers(this.PortalId).OfType<UserInfo>().ToList();
        //    this.BindUsers(users);

        //    this.LoadDefaultUsers();
        //}

        //private void Filter(string SearchText, string SearchField)
        //{
        //    var users = default(List<UserInfo>);
        //    var total = 0;

        //    switch (SearchField)
        //    {
        //        case "Email":
        //            users = UserController.GetUsersByEmail(this.PortalId, SearchText + "%", -1, -1, ref total)
        //                                  .OfType<UserInfo>().ToList();
        //            break;
        //        case "Username":
        //            users = UserController.GetUsersByUserName(this.PortalId, SearchText + "%", -1, -1, ref total)
        //                                  .OfType<UserInfo>().ToList();
        //            break;
        //        case "RoleName":
        //            users = RoleController.Instance.GetUsersByRole(this.PortalId, SearchText).ToList();
        //            break;

        //        default:
        //            users = UserController
        //                .GetUsersByProfileProperty(this.PortalId, SearchField, SearchText + "%", 0, 1000, ref total)
        //                .OfType<UserInfo>().ToList();
        //            break;
        //    }
        //    this.BindUsers(users);

        //    this.LoadDefaultUsers();
        //}

        //private void BindUsers(IEnumerable<UserInfo> users)
        //{
        //    this.cboUsers.Items.Clear();

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

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles the Init event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                //Typescript
                var jsFolder = Path.Combine(this.ModuleScriptFolder, DistributionFolder);
                var jsPriority = IdentitySwitcherFileOrder.Js.AngularCustomApp;
                this.RegisterScript(jsFolder, "dnn.identityswitcher.js", jsPriority++);

                var componentsFolder = Path.Combine(this.ModuleAngularAppFolderName, "components");

                // Identity Switcher factory
                this.RegisterScript(Path.Combine(componentsFolder, "identitySwitcher"), "identityswitcher.factory.js", jsPriority++);

                //Js Resources
                jsPriority = IdentitySwitcherFileOrder.Js.Angular;
                jsFolder = this.ModuleJsResourcesFolder;
                this.RegisterScript(jsFolder, "angular.min.js", jsPriority++);
                this.RegisterScript(jsFolder, "angular-resource.min.js", jsPriority++);
            }
            catch (Exception exception)
            {
                Exceptions.ProcessModuleLoadException(this, exception);
            }
        }

        /// <summary>
        ///     Runs when the page loads. Databinds the user switcher drop down list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //if (this.UseAjax && AJAX.IsInstalled())
                //{
                //    AJAX.RegisterScriptManager();

                //    new UpdateProgress
                //    {
                //        ID = this.UpdatePanel1.ID + "_Prog",
                //        AssociatedUpdatePanelID = this.UpdatePanel1.ID
                //    };
                //}
                //var repository = new IdentitySwitcherModuleSettingsRepository();
                //var settings = repository.GetSettings(this.ModuleConfiguration);

                //if (settings.UserSwitchingSpeed == UserSwitchingSpeed.UsingOneClick)
                //{
                //   this.cmdSwitch.Visible = false;
                //}

                if (!this.Page.IsPostBack)
                {
                    //this.BindSearchOptions();
                    //this.LoadDefaultUsers();
                    this.InitializeModuleInstanceJson(this.divBaseDiv);
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        //protected void cmdSearch_Click(object sender, EventArgs e)
        //{
        //    if (this.txtSearch.Text == "")
        //    {
        //        this.LoadAllUsers();
        //    }
        //    else
        //    {
        //        this.Filter(this.txtSearch.Text, this.ddlSearchType.SelectedValue);
        //    }
        //}

        //protected void cboUsers_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    var repository = new IdentitySwitcherModuleSettingsRepository();
        //    var settings = repository.GetSettings(this.ModuleConfiguration);

        //    if (settings.UserSwitchingSpeed == UserSwitchingSpeed.UsingOneClick)
        //    {
        //        this.cmdSwitch_Click(sender, e);
        //    }
        //}

        //protected void cmdSwitch_Click(object sender, EventArgs e)
        //{
        //    if (this.cboUsers.SelectedValue != this.UserId.ToString())
        //    {
        //        if (this.cboUsers.SelectedValue == Null.NullInteger.ToString())
        //        {
        //            this.Response.Redirect(Globals.NavigateURL("LogOff"));
        //        }
        //        else
        //        {
        //            var MyUserInfo = UserController.GetUserById(this.PortalId, int.Parse(this.cboUsers.SelectedValue));
        //            if (!ReferenceEquals(MyUserInfo, null))
        //            {
        //                //Remove user from cache
        //                if (this.Page.User != null)
        //                {
        //                    DataCache.ClearUserCache(this.PortalSettings.PortalId, this.Context.User.Identity.Name);
        //                }

        //                // sign current user out
        //                var objPortalSecurity = new PortalSecurity();
        //                objPortalSecurity.SignOut();

        //                // sign new user in
        //                UserController.UserLogin(this.PortalId, MyUserInfo, this.PortalSettings.PortalName,
        //                                         this.Request.UserHostAddress, false);

        //                // redirect to current url
        //                this.Response.Redirect(this.Request.RawUrl, true);
        //            }
        //        }
        //    }
        //}

        #endregion
    }
}
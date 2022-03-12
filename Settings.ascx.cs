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
    using System.Resources;
    using System.Web.UI.WebControls;
    using DNN.Modules.IdentitySwitcher.Model;
    using DNN.Modules.IdentitySwitcher.ModuleSettings;
    using DotNetNuke.Entities.Modules;
    using DotNetNuke.Services.Exceptions;
    using DotNetNuke.Services.Localization;

    /// -----------------------------------------------------------------------------
    /// <summary>
    ///     The Settings class manages Module Settings
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <history>
    /// </history>
    /// -----------------------------------------------------------------------------
    [DNNtc.ModuleControlProperties("Settings", "IdentitySwitcher Settings", DNNtc.ControlType.Host, "", true, false)]
    public partial class Settings : ModuleSettingsBase
    {
        #region Base Method Implementations

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///     LoadSettings loads the settings from the Database and displays them
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public override void LoadSettings()
        {
            try
            {
                if (Page.IsPostBack == false)
                {
                    var repository = new IdentitySwitcherModuleSettingsRepository();
                    var settings = repository.GetSettings(ModuleConfiguration);

                    rbSortBy.Items.Add(new ListItem(Localization.GetString("SortByDisplayName.Text", LocalResourceFile), "0"));
                    rbSortBy.Items.Add(new ListItem(Localization.GetString("SortByUserName.Text", LocalResourceFile), "1"));

                    rbSelectingMethod.Items.Add(new ListItem(Localization.GetString("Fast.Text", LocalResourceFile), "0"));
                    rbSelectingMethod.Items.Add(new ListItem(Localization.GetString("Slow.Text", LocalResourceFile), "1"));

                    if (UserInfo.IsSuperUser)
                    {
                        cbIncludeHostUser.Checked = settings.IncludeHost.GetValueOrDefault();
                    }
                    else
                    {
                        trHostSettings.Visible = false;
                    }

                    if (UserInfo.IsSuperUser || UserInfo.IsInRole(PortalSettings.AdministratorRoleName))
                    {
                        cbIncludeAdminUser.Checked = settings.IncludeAdmin.GetValueOrDefault();
                    }
                    else
                    {
                        trAdminSettings.Visible = false;
                    }

                    rbSortBy.SelectedValue = ((int)settings.SortBy).ToString();
                    rbSelectingMethod.SelectedValue = ((int)settings.UserSwitchingSpeed).ToString();

                    cbRequestAuthorization.Checked = settings.RequestAuthorization.GetValueOrDefault();
                }
            }
            catch (Exception exception) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exception);
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///     UpdateSettings saves the modified settings to the Database
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public override void UpdateSettings()
        {
            try
            {
                var repository = new IdentitySwitcherModuleSettingsRepository();
                var settings = repository.GetSettings(ModuleConfiguration);

                if (UserInfo.IsSuperUser)
                {
                    settings.IncludeHost = cbIncludeHostUser.Checked;
                }
                if (UserInfo.IsSuperUser || UserInfo.IsInRole(PortalSettings.AdministratorRoleName))
                {
                    settings.IncludeAdmin = cbIncludeAdminUser.Checked;
                }
                settings.SortBy = (SortBy)Enum.Parse(typeof(SortBy), rbSortBy.SelectedValue);
                settings.UserSwitchingSpeed = (UserSwitchingSpeed)Enum.Parse(typeof(UserSwitchingSpeed), rbSelectingMethod.SelectedValue);
                settings.RequestAuthorization = cbRequestAuthorization.Checked;

                repository.SaveSettings(ModuleConfiguration, settings);

                // refresh cache
                ModuleController.SynchronizeModule(ModuleId);
            }
            catch (Exception exception) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exception);
            }
        }

        #endregion
    }
}
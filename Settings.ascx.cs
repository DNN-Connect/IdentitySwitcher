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
    using System.Web.UI.WebControls;
    using DNN.Modules.IdentitySwitcher.Installation;
    using DNN.Modules.IdentitySwitcher.Model;
    using DNN.Modules.IdentitySwitcher.ModuleSettings;
    using DotNetNuke.Entities.Modules;
    using DotNetNuke.Services.Exceptions;

    /// -----------------------------------------------------------------------------
    /// <summary>
    ///     The Settings class manages Module Settings
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <history>
    /// </history>
    /// -----------------------------------------------------------------------------
    [ModuleControlProperties("Settings", "IdentitySwitcher Settings", ControlType.Host, "", true, false)]
    public partial class Settings : ModuleSettingsBase
    {
        private void BindEnumToListControls(Type enumType, ListControl listcontrol)
        {
            var countElements = default(int);
            var names = Enum.GetNames(enumType);
            var values = Enum.GetValues(enumType);
            for (countElements = 0; countElements <= names.Length - 1; countElements++)
            {
                listcontrol.Items.Add(new ListItem(names[countElements], values.GetValue(countElements).ToString()));
            }
        }

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
                if (this.Page.IsPostBack == false)
                {
                    var repository = new IdentitySwitcherModuleSettingsRepository();
                    var settings = repository.GetSettings(this.ModuleConfiguration);

                    this.BindEnumToListControls(typeof(SortBy), this.rbSortBy);
                    this.rbSortBy.SelectedIndex = 0;

                    this.BindEnumToListControls(typeof(UserSwitchingSpeed), this.rbSelectingMethod);
                    this.rbSelectingMethod.SelectedIndex = 0;

                    if (this.UserInfo.IsSuperUser)
                    {
                        if (settings.IncludeHost != null)
                        {
                            this.cbIncludeHostUser.Checked = (bool) settings.IncludeHost;
                        }
                    }
                    else
                    {
                        this.trHostSettings.Visible = false;
                    }

                    this.rbSortBy.SelectedValue = settings.SortBy.ToString();

                    this.rbSelectingMethod.SelectedValue = settings.UserSwitchingSpeed.ToString();
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
                var settings = repository.GetSettings(this.ModuleConfiguration);

                if (this.UserInfo.IsSuperUser)
                {
                    settings.IncludeHost = this.cbIncludeHostUser.Checked;
                }
                settings.SortBy = (SortBy) Enum.Parse(typeof(SortBy), this.rbSortBy.SelectedValue);
                settings.UserSwitchingSpeed =
                    (UserSwitchingSpeed) Enum.Parse(typeof(UserSwitchingSpeed), this.rbSelectingMethod.SelectedValue);

                repository.SaveSettings(this.ModuleConfiguration, settings);

                // refresh cache
                ModuleController.SynchronizeModule(this.ModuleId);
            }
            catch (Exception exception) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exception);
            }
        }

        #endregion
    }
}
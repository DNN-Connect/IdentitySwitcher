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
    using System.Globalization;
    using System.IO;
    using System.Web.UI.HtmlControls;
    using DNN.Modules.IdentitySwitcher.Components;
    using DNN.Modules.IdentitySwitcher.Model;
    using DNN.Modules.IdentitySwitcher.ModuleSettings;
    using DotNetNuke.Common;
    using DotNetNuke.Common.Utilities;
    using DotNetNuke.Entities.Modules;
    using DotNetNuke.Entities.Users;
    using DotNetNuke.Services.Exceptions;
    using DotNetNuke.Services.Localization;
    using DotNetNuke.Web.Client.ClientResourceManagement;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="DotNetNuke.Entities.Modules.PortalModuleBase" />
    [DNNtc.ModuleControlProperties("", "IdentitySwitcher", DNNtc.ControlType.View, "", true, false)]
    public partial class ViewIdentitySwitcher : PortalModuleBase
    {
        #region Private properties

        /// <summary>
        /// The module folder name
        /// </summary>
        private string _moduleFolderName;

        /// <summary>
        /// Gets the name of the script folder.
        /// </summary>
        /// <value>
        /// The name of the script folder.
        /// </value>
        private string ScriptFolderName { get; } = "Scripts";

        /// <summary>
        /// The distribution folder name
        /// </summary>
        private const string DistributionFolderName = "dist";

        /// <summary>
        /// The resources folder name
        /// </summary>
        private const string ResourcesFolderName = "resources";

        /// <summary>
        /// Gets the name of the module folder.
        /// </summary>
        /// <value>
        /// The name of the module folder.
        /// </value>
        private string ModuleFolderName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_moduleFolderName))
                {
                    _moduleFolderName =
                        Path.Combine(Globals.DesktopModulePath, ModuleConfiguration.DesktopModule.FolderName);
                }
                return _moduleFolderName;
            }
        }

        /// <summary>
        /// Gets the module script folder.
        /// </summary>
        /// <value>
        /// The module script folder.
        /// </value>
        private string ModuleScriptFolder => Path.Combine(ModuleFolderName, ScriptFolderName);

        #endregion

        #region Private Methods
        /// <summary>
        /// Initializes the module instance json.
        /// </summary>
        /// <param name="initControl">The initialize control.</param>
        private void InitializeModuleInstanceJson(HtmlGenericControl initControl)
        {
            if (initControl != null)
            {
                var initScript = string.Format(CultureInfo.InvariantCulture, "vm.init({0})",
                                               GetModuleInstance().ToJson());

                initControl.Attributes.Add("ng-init", initScript);
            }
        }

        /// <summary>
        /// Registers the script.
        /// </summary>
        /// <param name="folder">The folder.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="priority">The priority.</param>
        private void RegisterScript(string folder, string fileName, int priority)
        {
            var scriptPath = string.IsNullOrWhiteSpace(folder) ? fileName : Path.Combine(folder, fileName);
            ClientResourceManager.RegisterScript(Page, scriptPath, priority);
        }

        /// <summary>
        /// Gets the module instance.
        /// </summary>
        /// <returns></returns>
        private ModuleInstanceBase GetModuleInstance()
        {
            return GetModuleInstance<ModuleInstanceBase>(this);
        }

        /// <summary>
        /// Gets the module instance.
        /// </summary>
        /// <typeparam name="TModuleInstance">The type of the module instance.</typeparam>
        /// <param name="moduleControl">The module control.</param>
        /// <returns></returns>
        private TModuleInstance GetModuleInstance<TModuleInstance>(PortalModuleBase moduleControl)
            where TModuleInstance : ModuleInstanceBase, new()
        {
            var result = new TModuleInstance();

            if (moduleControl != null)
            {
                result.ModuleID = moduleControl.ModuleId;
                result.PortalId = moduleControl.PortalId;
                result.FilterText = Localization.GetString("FilterText.Text", LocalResourceFile);
                result.FilterIconText = Localization.GetString("FilterIcon.Text", LocalResourceFile);
                result.SwitchToText = Localization.GetString("SwitchToText.Text", LocalResourceFile);
                result.WaitingForConfirmation = Localization.GetString("WaitingForConfirmation.Text", LocalResourceFile);
                result.SwitchIconText = Localization.GetString("SwitchIcon.Text", LocalResourceFile);

                var moduleInfo = new ModuleController().GetModule(moduleControl.ModuleId);
                var repository = new IdentitySwitcherModuleSettingsRepository();
                var settings = repository.GetSettings(moduleInfo);

                result.SwitchUserInOneClick = settings.UserSwitchingSpeed == UserSwitchingSpeed.Fast;
            }

            return result;
        }

        /// <summary>
        /// Handles the Init event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Page_Init(object sender, EventArgs e)
        {
            try
            {
                var priority = 0;

                //Javascript Resources
                var jsFolder = Path.Combine(ModuleScriptFolder, ResourcesFolderName);
                RegisterScript(jsFolder, "angular.min.js", priority++);
                RegisterScript(jsFolder, "angular-resource.min.js", priority++);

                //Typescript
                jsFolder = Path.Combine(ModuleScriptFolder, DistributionFolderName);
                RegisterScript(jsFolder, "dnn.identityswitcher.js", priority++);

            }
            catch (Exception exception)
            {
                Exceptions.ProcessModuleLoadException(this, exception);
            }
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DotNetNuke.Framework.ServicesFramework.Instance.RequestAjaxScriptSupport();
                if (!Page.IsPostBack)
                {
                    InitializeModuleInstanceJson(divBaseDiv);
                }
            }
            catch (Exception exception) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exception);
            }
        } 
        #endregion
    }
}
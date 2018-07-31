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
    using DNN.Modules.IdentitySwitcher.Installation;
    using DotNetNuke.Common;
    using DotNetNuke.Common.Utilities;
    using DotNetNuke.Entities.Modules;
    using DotNetNuke.Services.Exceptions;
    using DotNetNuke.Web.Client.ClientResourceManagement;

    [ModuleControlProperties("", "IdentitySwitcher", ControlType.View, "", true, false)]
    public partial class ViewIdentitySwitcher : PortalModuleBase
    {
        #region Private properties
       
        private string _moduleFolderName;

        private string ScriptFolderName { get; } = "Scripts";

        private const string DistributionFolderName = "dist";

        private const string ResourcesFolderName = "resources";

        private string ModuleFolderName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this._moduleFolderName))
                {
                    this._moduleFolderName =
                        Path.Combine(Globals.DesktopModulePath, this.ModuleConfiguration.DesktopModule.FolderName);
                }
                return this._moduleFolderName;
            }
        }

        private string ModuleScriptFolder => Path.Combine(this.ModuleFolderName, this.ScriptFolderName);

        #endregion

        #region Private Methods
        private void InitializeModuleInstanceJson(HtmlGenericControl initControl)
        {
            if (initControl != null)
            {
                var initScript = string.Format(CultureInfo.InvariantCulture, "vm.init({0})",
                                               this.GetModuleInstance().ToJson());

                initControl.Attributes.Add("ng-init", initScript);
            }
        }

        private void RegisterScript(string folder, string fileName, int priority)
        {
            var scriptPath = string.IsNullOrWhiteSpace(folder) ? fileName : Path.Combine(folder, fileName);
            ClientResourceManager.RegisterScript(this.Page, scriptPath, priority);
        }

        private ModuleInstanceBase GetModuleInstance()
        {
            return this.GetModuleInstance<ModuleInstanceBase>(this);
        }

        private TModuleInstance GetModuleInstance<TModuleInstance>(PortalModuleBase moduleControl)
            where TModuleInstance : ModuleInstanceBase, new()
        {
            var result = new TModuleInstance();

            if (moduleControl != null)
            {
                result.ModuleID = moduleControl.ModuleId;
            }

            return result;
        }

        private void Page_Init(object sender, EventArgs e)
        {
            try
            {
                var priority = 0;

                //Javascript Resources
                var jsFolder = Path.Combine(this.ModuleScriptFolder, ResourcesFolderName);
                this.RegisterScript(jsFolder, "angular.min.js", priority++);
                this.RegisterScript(jsFolder, "angular-resource.min.js", priority++);

                //Typescript
                jsFolder = Path.Combine(this.ModuleScriptFolder, DistributionFolderName);
                this.RegisterScript(jsFolder, "dnn.identityswitcher.js", priority++);

            }
            catch (Exception exception)
            {
                Exceptions.ProcessModuleLoadException(this, exception);
            }
        }

        private void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.Page.IsPostBack)
                {
                    this.InitializeModuleInstanceJson(this.divBaseDiv);
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
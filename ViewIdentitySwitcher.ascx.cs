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
    using System.Web.UI.WebControls;
    using DNN.Modules.IdentitySwitcher.Components;
    using DNN.Modules.IdentitySwitcher.Components.Model;
    using DotNetNuke.Common.Utilities;
    using DotNetNuke.Services.Exceptions;
    using DotNetNuke.Services.Localization;
    using global::IdentitySwitcher.DotNetNuke.Web.Client;

    /// <summary>
    ///     The ViewDynamicModule class displays the content
    /// </summary>
    /// <seealso cref="DNN.Modules.IdentitySwitcher.Components.IdentitySwitcherPortalModuleBase" />
    /// -----------------------------------------------------------------------------
    /// <history></history>
    /// -----------------------------------------------------------------------------
    [DNNtc.ModuleControlProperties("", "IdentitySwitcher", DNNtc.ControlType.View, "", true, false)]
    public partial class ViewIdentitySwitcher : IdentitySwitcherPortalModuleBase
    {
        #region Private Methods

        /// <summary>
        ///     Initializes the module instance json.
        /// </summary>
        /// <param name="initControl">The initialize control.</param>
        protected virtual void InitializeModuleInstanceJson(HtmlGenericControl initControl)
        {
            if (initControl != null)
            {
                var initScript = string.Format(CultureInfo.InvariantCulture, "vm.init({0})",
                                               this.GetModuleInstance().ToJson());

                initControl.Attributes.Add("ng-init", initScript);
            }
        }

        #endregion

        #region Event Handlers

        /// <summary>
        ///     Handles the Init event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                //Typescript
                var jsFolder = Path.Combine(this.ModuleScriptFolder, DistributionFolder);
                var jsPriority = IdentitySwitcherFileOrder.Js.AngularCustomApp;
                this.RegisterScript(jsFolder, "dnn.identityswitcher.js", jsPriority++);

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
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.Page.IsPostBack)
                {
                    this.InitializeModuleInstanceJson(this.divBaseDiv);
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        #endregion
    }
}
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
    using DNN.Modules.IdentitySwitcher.Model;
    using DNN.Modules.IdentitySwitcher.ModuleSettings;
    using DotNetNuke.Entities.Modules;
    using DotNetNuke.Services.Localization;

    /// <summary>
    /// </summary>
    public class ModuleInstanceBase
    {
        #region Properties

        /// <summary>
        ///     Gets or sets the module identifier.
        /// </summary>
        /// <value>
        ///     The module identifier.
        /// </value>
        public int ModuleID { get; set; }

        /// <summary>
        /// Gets a value indicating whether [switch user in one click].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [switch user in one click]; otherwise, <c>false</c>.
        /// </value>
        public bool SwitchUserInOneClick
        {
            get
                {
                    var moduleInfo = new ModuleController().GetModule(this.ModuleID);
                    var repository = new IdentitySwitcherModuleSettingsRepository();
                    var settings = repository.GetSettings(moduleInfo);

                    return settings.UserSwitchingSpeed == UserSwitchingSpeed.Fast;
                }
        }

        public string FilterText { get; set; }

        public string SwitchToText { get; set; }

        #endregion
    }
}
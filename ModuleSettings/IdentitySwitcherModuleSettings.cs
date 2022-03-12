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

namespace DNN.Modules.IdentitySwitcher.ModuleSettings
{
    using System;
    using DNN.Modules.IdentitySwitcher.Model;
    using DotNetNuke.Entities.Modules.Settings;

    /// <summary>
    /// </summary>
    [Serializable]
    public class IdentitySwitcherModuleSettings
    {
        // The old version of the module used camelcase for the module settings.
        // In order to avoid difficulties these properties are kept that way and translated with the parametername attribute to
        // pascalcase.

        /// <summary>
        ///     Gets or sets the include host.
        /// </summary>
        /// <value>
        ///     The include host.
        /// </value>
        [TabModuleSetting(ParameterName = "includeHost")]
        public bool? IncludeHost { get; set; }

        /// <summary>
        ///     Gets or sets the include admin.
        /// </summary>
        /// <value>
        ///     The include admin.
        /// </value>
        [TabModuleSetting(ParameterName = "includeAdmin")]
        public bool? IncludeAdmin { get; set; }


        /// <summary>
        ///     Gets or sets the sort by.
        /// </summary>
        /// <value>
        ///     The sort by.
        /// </value>
        [TabModuleSetting(ParameterName = "sortBy")]
        public SortBy SortBy { get; set; } = SortBy.DisplayName;

        /// <summary>
        ///     Gets or sets the user switching speed.
        /// </summary>
        /// <value>
        ///     The user switching speed.
        /// </value>
        [TabModuleSetting(ParameterName = "userSwitchingSpeed")]
        public UserSwitchingSpeed UserSwitchingSpeed { get; set; } = UserSwitchingSpeed.Fast;

        /// <summary>
        ///     Enables the option to request authorization for the impersonation.
        /// </summary>
        /// <value>
        ///     Request Authorization.
        /// </value>
        [TabModuleSetting(ParameterName = "requestAuthorization")]
        public bool? RequestAuthorization { get; set; }
    }
}
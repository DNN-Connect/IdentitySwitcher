// VBConversions Note: VB project level imports
using System.Web.UI.WebControls;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Tabs;
using System.Text.RegularExpressions;
using System;
using DotNetNuke.Entities;
using DotNetNuke.Framework;
using DotNetNuke;
using System.Text;
using System.Web.UI.HtmlControls;
using DotNetNuke.Services;
using DotNetNuke.Common;
using DotNetNuke.UI;
using DotNetNuke.Data;
using System.Collections;
using System.Web.Profile;
using System.Collections.Specialized;
using Microsoft.VisualBasic;
using System.Diagnostics;
using DotNetNuke.Security;
using System.Web.UI;
using System.Web.SessionState;
using System.Data;
using System.Configuration;
using System.Web;
using DotNetNuke.Services.Localization;
using System.Web.Security;
using DotNetNuke.Modules;
using System.Web.Caching;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
// End of VB project level imports

using DotNetNuke.Entities.Modules;

//
// Copyright (c) 2008 - 2009, interApps, Erik van Ballegoij, http://www.interapps.nl
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without modification, are permitted provided that the
// following conditions are met:
//
// * Redistributions of source code must retain the above copyright notice, this list of conditions and the
//   following disclaimer.
// * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the
//   following disclaimer in the documentation and/or other materials provided with the distribution.
// * Neither the name of Apollo Software nor the names of its contributors may be used to endorse or promote products
//  derived from this software without specific prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES,
// INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
// SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
// SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY,
// WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE
// USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//



namespace interApps.DNN.Modules.IdentitySwitcher
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The Settings class manages Module Settings
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <history>
    /// </history>
    /// -----------------------------------------------------------------------------
    [DNNtc.ModuleControlProperties("", "IdentitySwitcher Settings", DNNtc.ControlType.Host, "", true, false)]
    public partial class Settings : DotNetNuke.Entities.Modules.ModuleSettingsBase
	{
		
#region Base Method Implementations
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// LoadSettings loads the settings from the Database and displays them
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
					rbSortBy.Items.Add(new ListItem(Localization.GetString("SortByDisplayName", LocalResourceFile), "DisplayName"));
					rbSortBy.Items.Add(new ListItem(Localization.GetString("SortByUserName", LocalResourceFile), "UserName"));
					rbSortBy.SelectedIndex = 0;
					
					
					if (UserInfo.IsSuperUser)
					{
						if (TabModuleSettings.Contains("includeHost"))
						{
							this.cbIncludeHostUser.Checked = bool.Parse(System.Convert.ToString(TabModuleSettings["includeHost"].ToString()));
						}
					}
					else
					{
						trHostSettings.Visible = false;
					}
					if (TabModuleSettings.Contains("useAjax"))
					{
						this.cbUseAjax.Checked = bool.Parse(System.Convert.ToString(TabModuleSettings["useAjax"].ToString()));
					}
					else
					{
						this.cbUseAjax.Checked = true;
					}
					
					if (TabModuleSettings.Contains("sortBy"))
					{
						rbSortBy.SelectedValue = System.Convert.ToString(TabModuleSettings["sortBy"].ToString());
					}
				}
			}
			catch (Exception exc) //Module failed to load
			{
				Exceptions.ProcessModuleLoadException(this, exc);
			}
		}
		
		/// -----------------------------------------------------------------------------
		/// <summary>
		/// UpdateSettings saves the modified settings to the Database
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
				DotNetNuke.Entities.Modules.ModuleController objModules = new DotNetNuke.Entities.Modules.ModuleController();
				if (UserInfo.IsSuperUser)
				{
					objModules.UpdateTabModuleSetting(TabModuleId, "includeHost", this.cbIncludeHostUser.Checked.ToString());
				}
				objModules.UpdateTabModuleSetting(TabModuleId, "useAjax", this.cbUseAjax.Checked.ToString());
				objModules.UpdateTabModuleSetting(TabModuleId, "sortBy", rbSortBy.SelectedValue);
				
				// refresh cache
				ModuleController.SynchronizeModule(ModuleId);
			}
			catch (Exception exc) //Module failed to load
			{
				Exceptions.ProcessModuleLoadException(this, exc);
			}
		}
		
#endregion
		
	}
	
}



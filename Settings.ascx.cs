// 
// Copyright (c) 2018 DNN Connect, https://www.dnn-connect.org
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this 
// software and associated documentation files (the "Software"), to deal in the Software 
// without restriction, including without limitation the rights to use, copy, modify, merge, 
// publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons 
// to whom the Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or 
// substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR 
// PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE 
// FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, 
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. 
//

using System.Web.UI.WebControls;
using DotNetNuke.Services.Exceptions;
using System;
using DotNetNuke.Services.Localization;
using DotNetNuke.Entities.Modules;

namespace DNN.Modules.IdentitySwitcher
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
    [DNNtc.ModuleControlProperties("Settings", "IdentitySwitcher Settings", DNNtc.ControlType.Edit, "", false, false)]
	public partial class Settings : ModuleSettingsBase
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
						rbSortBy.SelectedValue = Convert.ToString(TabModuleSettings["sortBy"].ToString());
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
                ModuleController objModules = new ModuleController();
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
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

using System.Reflection;
using DotNetNuke.Entities.Users;
using DotNetNuke.Entities.Profile;
using DotNetNuke.Security.Roles;

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
	/// The ViewDynamicModule class displays the content
	/// </summary>
	/// <remarks>
	/// </remarks>
	/// <history>
	/// </history>
	/// -----------------------------------------------------------------------------
	public partial class ViewIdentitySwitcher : DotNetNuke.Entities.Modules.PortalModuleBase
	{
		
#region Private Properties
		
		/// <summary>
		/// reads the setting for inclusion of the host user. This setting defaults to false
		/// </summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		private bool IncludeHostUser
		{
			get
			{
				bool bRetValue = false;
				if (Settings.Contains("includeHost"))
				{
					bool.TryParse(System.Convert.ToString(Settings["includeHost"].ToString()), out bRetValue);
				}
				return bRetValue;
			}
		}
		
		private bool UseAjax
		{
			get
			{
				bool bRetValue = true;
				if (Settings.Contains("useAjax"))
				{
					bool.TryParse(System.Convert.ToString(Settings["useAjax"].ToString()), out bRetValue);
				}
				return bRetValue;
			}
		}
		
		private SortBy SortResultsBy
		{
			get
			{
				SortBy bRetValue = SortBy.DisplayName;
				if (Settings.Contains("sortBy"))
				{
					bRetValue = (SortBy) (Enum.Parse(typeof(SortBy), System.Convert.ToString(Settings["sortBy"].ToString())));
				}
				return bRetValue;
			}
		}
#endregion
		
#region Private Methods
		
		private ListItem AddSearchItem(string name)
		{
			string propertyName = Null.NullString;
			if (!ReferenceEquals(Request.QueryString["filterProperty"], null))
			{
				propertyName = Request.QueryString["filterProperty"];
			}
			
			string text = Localization.GetString(name, this.LocalResourceFile);
			if (string.IsNullOrEmpty(text))
			{
				text = name;
			}
			ListItem li = new ListItem(text, name);
			if (name == propertyName)
			{
				li.Selected = true;
			}
			return li;
		}
		
		private void BindSearchOptions()
		{
			ddlSearchType.Items.Add(AddSearchItem("RoleName"));
			ddlSearchType.Items.Add(AddSearchItem("Email"));
			ddlSearchType.Items.Add(AddSearchItem("Username"));
			ProfilePropertyDefinitionCollection profileProperties = ProfileController.GetPropertyDefinitionsByPortal(PortalId, false);
			
			foreach (ProfilePropertyDefinition definition in profileProperties)
			{
				ddlSearchType.Items.Add(AddSearchItem(definition.PropertyName));
			}
		}
		
		private void LoadDefaultUsers()
		{
			if (IncludeHostUser)
			{
				ArrayList arHostUsers = (ArrayList) (UserController.GetUsers(Null.NullInteger));
				foreach (UserInfo hostUser in arHostUsers)
				{
					cboUsers.Items.Insert(0, new ListItem(hostUser.Username, hostUser.UserID.ToString()));
				}
			}
			cboUsers.Items.Insert(0, new ListItem(Localization.GetString("Anonymous", LocalResourceFile), Null.NullInteger.ToString()));
		}
		
		private void LoadAllUsers()
		{
			ArrayList users = (ArrayList) (UserController.GetUsers(PortalId));
			BindUsers(users);
			
			LoadDefaultUsers();
		}
		
		private void Filter(string SearchText, string SearchField)
		{
			ArrayList users = default(ArrayList);
			int total = 0;
			
			switch (SearchField)
			{
				case "Email":
					users = UserController.GetUsersByEmail(PortalId, false, SearchText + "%", -1, -1, ref total);
					break;
				case "Username":
					users = UserController.GetUsersByUserName(PortalId, false, SearchText + "%", -1, -1, ref total);
					break;
				case "RoleName":
					RoleController objRolecontroller = new RoleController();
					users = objRolecontroller.GetUsersByRoleName(PortalId, SearchText);
					break;
					
				default:
					users = UserController.GetUsersByProfileProperty(PortalId, false, SearchField, SearchText + "%", 0, 1000, ref total);
					break;
			}
			BindUsers(users);
			
			LoadDefaultUsers();
		}
		
		private void BindUsers(ArrayList users)
		{
			cboUsers.Items.Clear();
			
			users.Sort(new Comparer(SortResultsBy));
			
			string display = "";
			foreach (UserInfo user in users)
			{
				
				if (SortResultsBy == SortBy.DisplayName)
				{
					display = string.Format("{0} - {1}", user.DisplayName, user.Username);
				}
				else
				{
					display = string.Format("{0} - {1}", user.Username, user.DisplayName);
				}
				cboUsers.Items.Add(new ListItem(display, user.UserID.ToString()));
			}
		}
#endregion
		
#region Event Handlers
		
		/// <summary>
		/// Runs when the page loads. Databinds the user switcher drop down list.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <remarks></remarks>
		private void Page_Load(System.Object sender, System.EventArgs e)
		{
			try
			{
				if (UseAjax && DotNetNuke.Framework.AJAX.IsInstalled())
				{
					DotNetNuke.Framework.AJAX.RegisterScriptManager();
					
					DotNetNuke.Framework.AJAX.CreateUpdateProgressControl(this.UpdatePanel1.ID);
				}
				if (!Page.IsPostBack)
				{
					
					BindSearchOptions();
					LoadDefaultUsers();
					
				}
			}
			catch (Exception exc) //Module failed to load
			{
				Exceptions.ProcessModuleLoadException(this, exc);
			}
		}
		
		protected void cmdSearch_Click(object sender, EventArgs e)
		{
			if (txtSearch.Text == "")
			{
				LoadAllUsers();
			}
			else
			{
				Filter(txtSearch.Text, ddlSearchType.SelectedValue);
			}
		}
		
		protected void cmdSwitch_Click(object sender, EventArgs e)
		{
			if (cboUsers.SelectedValue != this.UserId.ToString())
			{
				if (cboUsers.SelectedValue == Null.NullInteger.ToString())
				{
					Response.Redirect(DotNetNuke.Common.Globals.NavigateURL("LogOff"));
				}
				else
				{
					UserInfo MyUserInfo = UserController.GetUser(PortalId, int.Parse(cboUsers.SelectedValue), false);
					if (!ReferenceEquals(MyUserInfo, null))
					{
						//Remove user from cache
						if (Page.User != null)
						{
							DataCache.ClearUserCache(this.PortalSettings.PortalId, Context.User.Identity.Name);
						}
						
						// sign current user out
						PortalSecurity objPortalSecurity = new PortalSecurity();
						objPortalSecurity.SignOut();
						
						// sign new user in
						UserController.UserLogin(PortalId, MyUserInfo, PortalSettings.PortalName, Request.UserHostAddress, false);
						
						// redirect to current url
						Response.Redirect(Request.RawUrl, true);
					}
				}
				
			}
		}
		
#endregion
		
	}
	
}


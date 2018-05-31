<%@ Control Language="C#" Inherits="DNN.Modules.IdentitySwitcher.ViewIdentitySwitcher"
AutoEventWireup="true" Explicit="True" CodeBehind="ViewIdentitySwitcher.ascx.cs" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="is_SearchRow">
            <div class="is_SearchLabel">
                <asp:Label ID="lblSearch" CssClass="SubHead" resourcekey="Search" runat="server"/>
            </div>
            <div class="is_SearchTask">
                <asp:TextBox ID="txtSearch" runat="server" CssClass="NormalTextBox is_SearchText"/><span
                                                                                                       class="is_SearchSeperator">
                </span><asp:DropDownList ID="ddlSearchType" runat="server"
                                         CssClass="NormalTextBox is_SearchMenu"/>
                <asp:ImageButton ID="cmdSearch" runat="server" ResourceKey="cmdSearch" ImageUrl="~/images/icon_search_16px.gif"
                                 DisplayLink="false" CausesValidation="false" OnClick="cmdSearch_Click"/>
            </div>
        </div>
        <div class="is_Clear">
        </div>
        <div class="is_SwitchRow">
            <div class="is_SwitchLabel">
                <asp:Label ID="lblSwitchToIdentity" runat="server" CssClass="SubHead" resourcekey="SwitchToIdentity"/>
            </div>
            <div class="is_SwitchTask">
                <asp:DropDownList ID="cboUsers" runat="server" CssClass="NormalTextBox is_SwitchMenu"/>
                <asp:ImageButton ID="cmdSwitch" runat="server" ResourceKey="cmdSwitch" ImageUrl="~/images/action_refresh.gif"
                                 DisplayLink="false" CausesValidation="false" OnClick="cmdSwitch_Click"/>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
<div class="is_Clear">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="is_progress">
                <asp:ImageMap ID="imgProgress" runat="server" ImageUrl="~/images/progressbar.gif">
                </asp:ImageMap>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
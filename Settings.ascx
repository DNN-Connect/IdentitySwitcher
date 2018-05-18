<%@ Control Language="C#" AutoEventWireup="true" Inherits="interApps.DNN.Modules.IdentitySwitcher.Settings" Codebehind="Settings.ascx.cs" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<table cellspacing="0" cellpadding="2" border="0" summary="ModuleName Settings Design Table" id="tblDesignTable" runat="server">
    <tr id="trHostSettings" runat="server">
        <td class="SubHead" width="150"><dnn:label id="plIncludeHostUser" runat="server" controlname="cbIncludeHostUser" suffix=":" /></td>
        <td valign="bottom" >
            <asp:CheckBox ID="cbIncludeHostUser" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="SubHead" width="150"><dnn:label id="plUseAjax" runat="server" controlname="cbUseAjax" suffix=":" /></td>
        <td valign="bottom" >
            <asp:CheckBox ID="cbUseAjax" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="SubHead" width="150"><dnn:label id="plSortBy" runat="server" controlname="rbSortBy" suffix=":" /></td>
        <td valign="bottom" >
            <asp:RadioButtonList ID="rbSortBy" runat="server" CssClass="Normal" RepeatDirection="Horizontal" RepeatLayout="Flow">
            </asp:RadioButtonList>
        </td>
    </tr>
</table>


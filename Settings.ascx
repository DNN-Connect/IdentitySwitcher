<%@ Control Language="C#" AutoEventWireup="true" Inherits="DNN.Modules.IdentitySwitcher.Settings" CodeBehind="Settings.ascx.cs" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>

<asp:Panel runat="server">
    <div id="trHostSettings" class="dnnFormItem" runat="server">
        <dnn:label id="plIncludeHostUser" runat="server" controlname="cbIncludeHostUser" suffix=":" />
        <asp:CheckBox ID="cbIncludeHostUser" runat="server" />
    </div>
    <div id="trAdminSettings" class="dnnFormItem" runat="server">
        <dnn:label id="plIncludeAdminUser" runat="server" controlname="cbIncludeAdminUser" suffix=":" />
        <asp:CheckBox ID="cbIncludeAdminUser" runat="server" />
    </div>
    <div class="dnnFormItem">
        <dnn:label id="plSortBy" runat="server" controlname="rbSortBy" suffix=":" />
        <asp:RadioButtonList ID="rbSortBy" runat="server" CssClass="Normal" RepeatDirection="Vertical" RepeatLayout="Flow">
        </asp:RadioButtonList>
    </div>
    <div class="dnnFormItem">
        <dnn:label id="plSelectingMethod" runat="server" controlname="rbSelectingMethod" suffix=":" />
        <asp:RadioButtonList ID="rbSelectingMethod" runat="server" CssClass="Normal" RepeatDirection="Vertical" RepeatLayout="Flow">
        </asp:RadioButtonList>
    </div>
    <div class="dnnFormItem">
        <dnn:label id="plRequestAuthorization" runat="server" controlname="cbRequestAuthorization" suffix=":" />
        <asp:CheckBox ID="cbRequestAuthorization" runat="server" />
    </div>
</asp:Panel>

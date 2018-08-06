<%@ Control Language="C#" AutoEventWireup="true" Inherits="DNN.Modules.IdentitySwitcher.Settings" CodeBehind="Settings.ascx.cs" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<div>
    <div id="trHostSettings" runat="server">
        <div>
            <dnn:label id="plIncludeHostUser" runat="server" controlname="cbIncludeHostUser" suffix=":" />
        </div>
        <div>
            <asp:CheckBox ID="cbIncludeHostUser" runat="server" />
        </div>
    </div>
    <div>
        <dnn:label id="plSortBy" runat="server" controlname="rbSortBy" suffix=":" />
    </div>
    <div>
        <asp:RadioButtonList ID="rbSortBy" runat="server" CssClass="Normal" RepeatDirection="Horizontal" RepeatLayout="Flow">
        </asp:RadioButtonList>
    </div>
    <div>
        <dnn:label id="plSelectingMethod" runat="server" controlname="rbSelectingMethod" suffix=":" />
    </div>
    <div>
        <asp:RadioButtonList ID="rbSelectingMethod" runat="server" CssClass="Normal" RepeatDirection="Horizontal" RepeatLayout="Flow">
        </asp:RadioButtonList>
    </div>
</div>

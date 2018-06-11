<%@ Control Language="C#" Inherits="DNN.Modules.IdentitySwitcher.ViewIdentitySwitcher"
    AutoEventWireup="true" Explicit="True" CodeBehind="ViewIdentitySwitcher.ascx.cs" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="is_SearchRow">
            <div class="is_SearchLabel">
                <asp:Label ID="lblSearch" CssClass="SubHead" resourcekey="Search" runat="server" />
            </div>
            <div class="is_SearchTask">
                <asp:TextBox ID="txtSearch" runat="server" CssClass="NormalTextBox is_SearchText" /><span
                    class="is_SearchSeperator">
                </span>
                <asp:DropDownList ID="ddlSearchType" runat="server"
                    CssClass="NormalTextBox is_SearchMenu" />
                <asp:ImageButton ID="cmdSearch" runat="server" ResourceKey="cmdSearch" ImageUrl="~/images/icon_search_16px.gif"
                    DisplayLink="false" CausesValidation="false" OnClick="cmdSearch_Click" />
            </div>
        </div>
        <div class="is_Clear">
        </div>
        <div class="is_SwitchRow">
            <div class="is_SwitchLabel">
                <asp:Label ID="lblSwitchToIdentity" runat="server" CssClass="SubHead" resourcekey="SwitchToIdentity" />
            </div>
            <div class="is_SwitchTask">
                <asp:DropDownList ID="cboUsers" runat="server" CssClass="NormalTextBox is_SwitchMenu" OnSelectedIndexChanged="cboUsers_SelectedIndexChanged" AutoPostBack="true" />
                <asp:ImageButton ID="cmdSwitch" runat="server" ResourceKey="cmdSwitch" ImageUrl="~/images/action_refresh.gif"
                    DisplayLink="false" CausesValidation="false" OnClick="cmdSwitch_Click" />
            </div>
        </div>
        <div class="is_SwitchRow" id="OneClickSelector" runat="server" visible="False">
            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="NormalTextBox is_SwitchMenu" />
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
<div ng-app="dnn.identityswitcher" ng-controller="ApplicationController as main">
    <div ng-controller="IdentitySwitcherController as vm" runat="server" id="divBaseDiv">
        <ng-form class="form-inline">
            <div class="form-group">
              <%--  <label for="exampleInputAmount">Filter:</label>--%>
                <div class="input-group">
                    <div class="input-group-addon">Filter:</div>
                    <%--<input type="text" class="form-control" id="exampleInputAmount" placeholder="Amount"> --%>
                    <select class="form-control" ng-model="vm.selectedItem">
                        <option value="" disabled selected>Choose type</option>
                        <option ng-repeat="option in vm.searchItems" value="{{option}}">{{option}}</option>
                    </select>
                </div>
            </div>
            <span type="button" class="btn btn default" ng-click="vm.search()">
                <i class="glyphicon glyphicon-search"></i>
            </span>
        </ng-form>
    </div>
</div>

<%@ Control Language="C#" Inherits="DNN.Modules.IdentitySwitcher.ViewIdentitySwitcher"
    AutoEventWireup="true" Explicit="True" CodeBehind="ViewIdentitySwitcher.ascx.cs" %>
<div ng-app="dnn.identityswitcher" ng-controller="IdentitySwitcherController as vm" runat="server" id="divBaseDiv">
    <div class="is_SearchRow" ng-show="!vm.moduleInstance.value.SwitchUserInOneClick">
        <div class="is_SearchLabel">
            <span class="SubHead">Filter:</span>
        </div>
        <div class="is_SearchTask">
            <input type="text" class="NormalTextBox_is_SearchText" ng-model="vm.selectedSearchText">
            <span class="is_SearchSeparator"></span>
            <select class="NormalTextBox_is_SearchMenu" ng-model="vm.selectedItem">
                <option ng-repeat="option in vm.searchItems" value="{{option}}">{{option}}</option>
            </select>
            <button class="CommandButton" type="button" ng-click="vm.search()">
                <img src="/images/icon_search_16px.gif" title="Filter" alt="Filter" />
            </button>
        </div>
    </div>
    <div class="is_Clear"></div>
    <div class="is_SwitchRow">
        <div class="is_SwitchLabel">
            <span class="SubHead">Switch to:</span>
        </div>
        <div class="is_SwitchTask">
            <select class="NormalTextBox_is_SearchMenu" ng-model="vm.selectedUser" ng-options="user as user.userAndDisplayName for user in vm.foundUsers" ng-change="vm.userSelected()">
            </select>
            <button class="CommandButton" type="button" ng-click="vm.switchUser()" ng-show="!vm.moduleInstance.value.SwitchUserInOneClick">
                <img src="/images/action_refresh.gif" title="Switch identity" alt="Switch identity" />
            </button>
        </div>
    </div>
</div>

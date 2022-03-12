<%@ Control Language="C#" Inherits="DNN.Modules.IdentitySwitcher.ViewIdentitySwitcher" AutoEventWireup="true" Explicit="True" CodeBehind="ViewIdentitySwitcher.ascx.cs" %>
<div ng-app="dnn.identityswitcher" class="ng-cloak" ng-controller="IdentitySwitcherController as vm" runat="server" id="divBaseDiv">
    <div class="is_SearchRow" ng-show="!vm.moduleInstance.value.SwitchUserInOneClick">
        <div class="is_SearchLabel">
            <span class="SubHead">{{vm.moduleInstance.value.FilterText}}</span>
        </div>
        <div class="is_SearchTask">
            <input type="text" class="NormalTextBox is_SearchText" ng-model="vm.selectedSearchText">
            <span class="is_SearchSeparator"></span>
            <select class="NormalTextBox is_SearchMenu" ng-model="vm.selectedItem">
                <option ng-repeat="option in vm.searchItems" value="{{option}}">{{option}}</option>
            </select>
            <button class="CommandButton" type="button" ng-click="vm.search()">
                <img src="/Icons/Sigma/Search_16x16_Standard.png" title="Filter" alt="Filter" />
                <span class="SubHead">{{vm.moduleInstance.value.FilterIconText}}</span>
            </button>
        </div>
    </div>
    <div class="is_SwitchRow">
        <div class="is_SwitchLabel">
            <span class="SubHead">{{vm.moduleInstance.value.SwitchToText}}</span>
        </div>
        <div class="is_SwitchTask">
            <select class="NormalTextBox is_SearchMenu" ng-model="vm.selectedUser" ng-options="user as user.userAndDisplayName for user in vm.foundUsers" ng-change="vm.userSelected()">
            </select>
            <button class="CommandButton" type="button" ng-click="vm.switchUser()" ng-show="!vm.moduleInstance.value.SwitchUserInOneClick">
                <img src="/Icons/Sigma/Refresh_16x16_Standard.png" title="Switch identity" alt="Switch identity" />
                <span class="SubHead">{{vm.moduleInstance.value.SwitchIconText}}</span>
            </button>
        </div>
    </div>
    <div class="is_WaitRow" ng-show="vm.request.requestAuthorization">
        <span class="SubHead">{{vm.moduleInstance.value.WaitingForConfirmation}}</span>
        <br />
        <img src="/images/loading.gif" />
    </div>
</div>

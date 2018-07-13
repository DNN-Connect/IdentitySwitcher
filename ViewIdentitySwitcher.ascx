<%@ Control Language="C#" Inherits="DNN.Modules.IdentitySwitcher.ViewIdentitySwitcher"
    AutoEventWireup="true" Explicit="True" CodeBehind="ViewIdentitySwitcher.ascx.cs" %>
<div ng-app="dnn.identityswitcher" ng-controller="IdentitySwitcherController as vm" runat="server" id="divBaseDiv">
    <ng-form class="form-inline">
            <div class="form-group">
                <p class="form-control-static">Filter:</p>
            </div>
            <div class="form-group">
                <input type="text" class="form-control" id="searchText" ng-model="vm.selectedSearchText">
            </div>
            <div class="form-group">
                <select class="form-control" ng-model="vm.selectedItem">
                    <option value="" disabled selected>Choose type</option>
                    <option ng-repeat="option in vm.searchItems" value="{{option}}">{{option}}</option>
                </select>
            </div>
            <div class="form-group">
                <span type="button" class="btn btn default" ng-click="vm.search()">
                    <i class="glyphicon glyphicon-search"></i>
                </span>
            </div>
            <div class="clearfix"></div>
            <div class="form-group">
                <p class="form-control-static">Switch to:</p>
            </div>
            <div class="form-group">
                <select class="form-control" ng-model="vm.selectedUser" ng-options="user as user.userAndDisplayName for user in vm.foundUsers" ng-change="vm.userSelected()">
                    <option value="" disabled selected>Choose User</option>
                </select>
            </div>
            <div class="form-group" ng-show="!vm.moduleInstance.SwitchDirectly">
                <span type="button" class="btn btn default" ng-click="vm.switchUser()">
                    <i class="glyphicon glyphicon-refresh"></i>
                </span>
            </div>
        </ng-form>
</div>

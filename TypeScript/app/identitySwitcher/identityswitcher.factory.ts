module IdentitySwitcher {
    class IdentitySwitcherFactory implements IIdentitySwitcherFactory {
        constructor(
            private $q: ng.IQService,
            private $http: ng.IHttpService,
            private config: IIdentitySwitcherConstants) { }

        getSearchItems(moduleInstance: IModuleInstance): angular.IHttpPromise<string[]> {
            const apiUrl: string = moduleInstance.ApplicationPath + this.config.apiUrl +
                "identityswitcher/getsearchitems";

            return this.$http.get<string[]>(apiUrl,
                {
                    headers: {
                        "PortalId": moduleInstance.PortalId,
                        "ModuleId": moduleInstance.ModuleID,
                        "TabId": moduleInstance.ServicesFramework.getTabId()
                    }
                });
        }

        getUsers(moduleInstance: IModuleInstance, selectedSearchText: string, selectedSearchItem: string, onlyDefault: boolean): angular.IHttpPromise<IUserCollection> {
            const apiUrl: string = moduleInstance.ApplicationPath + this.config.apiUrl +
                "identityswitcher/getusers?searchtext=" +
                selectedSearchText +
                "&selectedsearchitem=" +
                selectedSearchItem +
                "&onlyDefault=" + onlyDefault;

            return this.$http.get<IUserCollection>(apiUrl,
                {
                    headers: {
                        "PortalId": moduleInstance.PortalId,
                        "ModuleId": moduleInstance.ModuleID,
                        "TabId": moduleInstance.ServicesFramework.getTabId()
                    }
                });
        }

        switchUser(moduleInstance: IModuleInstance, selectedUserId: number, selectedUserName: string): angular.IHttpPromise<ISwitchRequest> {
            const apiUrl: string = moduleInstance.ApplicationPath + this.config.apiUrl +
                "identityswitcher/switchuser?selecteduserid=" +
                selectedUserId +
                "&selectedusername=" +
                selectedUserName;

            return this.$http.post<ISwitchRequest>(apiUrl, null,
                {
                    headers: {
                        "PortalId": moduleInstance.PortalId,
                        "ModuleId": moduleInstance.ModuleID,
                        "TabId": moduleInstance.ServicesFramework.getTabId()
                    }
                });
        }

        checkStatus(moduleInstance: IModuleInstance, id: number): angular.IHttpPromise<boolean> {
            const apiUrl: string = moduleInstance.ApplicationPath + this.config.apiUrl +
                "identityswitcher/checkstatus?id=" + id;

            return this.$http.get<boolean>(apiUrl,
                {
                    headers: {
                        "PortalId": moduleInstance.PortalId,
                        "ModuleId": moduleInstance.ModuleID,
                        "TabId": moduleInstance.ServicesFramework.getTabId()
                    }
                });
        }

    }
    angular.module(IdentitySwitcher.appName)
        .factory("IdentitySwitcherFactory", ["$q", "$http", "IdentitySwitcherConstants", ($q, $http, identitySwitcherConstants) => new IdentitySwitcherFactory($q, $http, identitySwitcherConstants)]);
}
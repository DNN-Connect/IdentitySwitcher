module IdentitySwitcher {
    class IdentitySwitcherFactory {
        static $inject = [
            "$q", "$http", "IdentitySwitcherConstants"
        ];

        constructor(
            private $q: ng.IQService,
            private $http: ng.IHttpService,
            private config: IIdentitySwitcherConstants) { }

        getSearchItems(moduleInstance: IModuleInstance): angular.IHttpPromise<string[]> {
            const apiUrl: string = this.config.restfulApiUrl +
                "identityswitcher/getsearchitems";

            return this.$http.get<string[]>(apiUrl,
                {
                    headers: {
                        "ModuleId": moduleInstance.ModuleID,
                        "TabId": moduleInstance.ServicesFramework.getTabId()
                    }
                });
        }

        getUsers(moduleInstance: IModuleInstance, selectedSearchText: string, selectedSearchItem: string): angular.IHttpPromise<IUser[]> {
            const apiUrl: string = this.config.restfulApiUrl +
                "identityswitcher/getusers?searchtext=" +
                selectedSearchText +
                "&selectedsearchitem=" +
                selectedSearchItem;

            return this.$http.get<IUser[]>(apiUrl,
                {
                    headers: {
                        "ModuleId": moduleInstance.ModuleID,
                        "TabId": moduleInstance.ServicesFramework.getTabId()
                    }
                });
        }

        switchUser(moduleInstance: IModuleInstance, selectedUserId: string, selectedUserName: string): angular.IHttpPromise<void> {
            const apiUrl: string = this.config.restfulApiUrl +
                "identityswitcher/switchuser?selecteduserid=" +
                selectedUserId +
                "&selectedusername=" +
                selectedUserName;

            return this.$http.post<void>(apiUrl, null,
                {
                    headers: {
                        "ModuleId": moduleInstance.ModuleID,
                        "TabId": moduleInstance.ServicesFramework.getTabId()
                    }
                });
        }

        static create() {
            const instance = ($q: ng.IQService, $http: ng.IHttpService, identitySwitcherConstants: IIdentitySwitcherConstants) =>
                new IdentitySwitcherFactory($q, $http, identitySwitcherConstants);

            instance.$inject = ["$q", "$http", "IdentitySwitcherConstants"];

            return instance;
        }
    }
    angular.module(IdentitySwitcher.appName)
        .factory("IdentitySwitcherFactory", IdentitySwitcherFactory.create());
}
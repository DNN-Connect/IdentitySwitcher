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
                });
        }

        getUsers(moduleInstance: IModuleInstance, selectedSearchText: string, selectedSearchItem: string, moduleID: number): angular.IHttpPromise<IUser[]> {
            const apiUrl: string = this.config.restfulApiUrl +
                "identityswitcher/getusers?searchtext=" + selectedSearchText + "&selectedsearchitem=" + selectedSearchItem + "&moduleid=" + moduleID;

            return this.$http.get<IUser[]>(apiUrl,
                {
                });
        }

        switchUser(moduleInstance: IModuleInstance, selectedUserId: string, selectedUserName: string): angular.IHttpPromise<void> {
            const apiUrl: string = this.config.restfulApiUrl +
                "identityswitcher/switchuser?selecteduserid=" + selectedUserId + "&selectedusername=" + selectedUserName;

            return this.$http.get<void>(apiUrl,
                {
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
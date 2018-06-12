var IdentitySwitcher;
(function (IdentitySwitcher) {
    IdentitySwitcher.appName = "dnn.identityswitcher";
    angular.module(IdentitySwitcher.appName, [
        "ngResource"
    ])
        .value("moduleInstance", { value: null, servicesFramework: null });
})(IdentitySwitcher || (IdentitySwitcher = {}));
var IdentitySwitcher;
(function (IdentitySwitcher) {
    var IdentitySwitcherConstants = (function () {
        function IdentitySwitcherConstants() {
            this.viewTemplatesFolder = "pages/templates/";
            this.restfulApiUrl = "/DesktopModules/Identity%20Switcher/API/";
        }
        Object.defineProperty(IdentitySwitcherConstants, "getConstants", {
            get: function () {
                return new IdentitySwitcherConstants();
            },
            enumerable: true,
            configurable: true
        });
        return IdentitySwitcherConstants;
    }());
    angular
        .module(IdentitySwitcher.appName)
        .constant("IdentitySwitcherConstants", IdentitySwitcherConstants.getConstants);
})(IdentitySwitcher || (IdentitySwitcher = {}));
var IdentitySwitcher;
(function (IdentitySwitcher) {
    var ApplicationController = (function () {
        function ApplicationController(constants) {
            this.constants = constants;
        }
        ApplicationController.$inject = [
            "IdentitySwitcherConstants"
        ];
        return ApplicationController;
    }());
    angular.module(IdentitySwitcher.appName)
        .controller("ApplicationController", ApplicationController);
})(IdentitySwitcher || (IdentitySwitcher = {}));
var IdentitySwitcher;
(function (IdentitySwitcher) {
    var IdentitySwitcherController = (function () {
        function IdentitySwitcherController(identitySwitcherService) {
            this.identitySwitcherService = identitySwitcherService;
            this.searchItems = [];
            this.foundUsers = [];
            this.obtainSearchItems();
        }
        IdentitySwitcherController.prototype.search = function () {
            var _this = this;
            this.identitySwitcherService.getUsers(this.selectedSearchText, this.selectedItem, this.moduleInstance.ModuleID)
                .then(function (serverData) {
                _this.foundUsers = serverData;
            }, function () {
            });
        };
        IdentitySwitcherController.prototype.switchUser = function () {
            this.identitySwitcherService.switchUser(this.selectedUser.id, this.selectedUser.userName)
                .then(function () {
                location.reload();
            }, function () {
            });
            ;
        };
        IdentitySwitcherController.prototype.init = function (moduleInstance) {
            this.moduleInstance = moduleInstance;
        };
        IdentitySwitcherController.prototype.obtainSearchItems = function () {
            var _this = this;
            this.identitySwitcherService.getSearchItems()
                .then(function (serverData) {
                _this.searchItems = serverData;
            }, function () {
            });
        };
        IdentitySwitcherController.$inject = [
            "IdentitySwitcherService"
        ];
        return IdentitySwitcherController;
    }());
    angular.module(IdentitySwitcher.appName)
        .controller("IdentitySwitcherController", IdentitySwitcherController);
})(IdentitySwitcher || (IdentitySwitcher = {}));
var IdentitySwitcher;
(function (IdentitySwitcher) {
    var IdentitySwitcherService = (function () {
        function IdentitySwitcherService(identitySwitcherFactory) {
            this.identitySwitcherFactory = identitySwitcherFactory;
        }
        IdentitySwitcherService.prototype.getSearchItems = function () {
            var deferred = this.identitySwitcherFactory.getSearchItems();
            return deferred.$promise;
        };
        IdentitySwitcherService.prototype.getUsers = function (searchText, selectedSearchItem, moduleId) {
            var deferred = this.identitySwitcherFactory.getUsers({
                searchText: searchText,
                selectedSearchItem: selectedSearchItem,
                moduleId: moduleId
            });
            return deferred.$promise;
        };
        IdentitySwitcherService.prototype.switchUser = function (selectedUserId, selectedUserUserName) {
            var deferred = this.identitySwitcherFactory.switchUser({
                selectedUserId: selectedUserId,
                selectedUserUserName: selectedUserUserName
            });
            return deferred.$promise;
        };
        IdentitySwitcherService.$inject = [
            "IdentitySwitcherFactory"
        ];
        return IdentitySwitcherService;
    }());
    angular.module(IdentitySwitcher.appName)
        .service("IdentitySwitcherService", IdentitySwitcherService);
})(IdentitySwitcher || (IdentitySwitcher = {}));
//# sourceMappingURL=dnn.identityswitcher.js.map
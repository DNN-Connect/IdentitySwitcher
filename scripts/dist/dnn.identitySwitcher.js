var IdentitySwitcher;
(function (IdentitySwitcher) {
    IdentitySwitcher.appName = "dnn.identityswitcher";
    angular.module(IdentitySwitcher.appName, [
        "ngResource"
    ]);
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
            this.obtainSearchItems();
        }
        IdentitySwitcherController.prototype.search = function () {
            var bla = this.selectedItem;
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
        IdentitySwitcherService.$inject = [
            "IdentitySwitcherFactory"
        ];
        return IdentitySwitcherService;
    }());
    angular.module(IdentitySwitcher.appName)
        .service("IdentitySwitcherService", IdentitySwitcherService);
})(IdentitySwitcher || (IdentitySwitcher = {}));
//# sourceMappingURL=dnn.identityswitcher.js.map
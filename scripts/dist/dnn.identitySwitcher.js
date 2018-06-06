var IdentitySwitcher;
(function (IdentitySwitcher) {
    IdentitySwitcher.appName = "dnn.identityswitcher";
    angular.module(IdentitySwitcher.appName, []);
})(IdentitySwitcher || (IdentitySwitcher = {}));
var IdentitySwitcher;
(function (IdentitySwitcher) {
    var IdentitySwitcherConstants = (function () {
        function IdentitySwitcherConstants() {
            this.viewTemplatesFolder = "pages/templates/";
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
        function ApplicationController($rootScope, constants) {
            this.$rootScope = $rootScope;
            this.constants = constants;
            this.identitySwitcherView = "";
            this.moduleSettings = {};
            this.setTemplates();
        }
        ApplicationController.prototype.onUpdateModuleSettings = function (serverData) {
            this.moduleSettings = serverData;
            this.setTemplates();
        };
        ApplicationController.prototype.setTemplates = function () {
            var moduleFolder = this.moduleSettings.moduleFolder;
            this.identitySwitcherView = moduleFolder + this.constants.viewTemplatesFolder + "IdentitySwitcherView.html";
        };
        ApplicationController.$inject = [
            "$rootScope",
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
        function IdentitySwitcherController() {
        }
        IdentitySwitcherController.$inject = [];
        return IdentitySwitcherController;
    }());
    angular.module(IdentitySwitcher.appName)
        .controller("IdentitySwitcherController", IdentitySwitcherController);
})(IdentitySwitcher || (IdentitySwitcher = {}));
//# sourceMappingURL=dnn.identitySwitcher.js.map
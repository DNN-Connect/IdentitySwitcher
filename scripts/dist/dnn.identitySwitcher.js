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
        function IdentitySwitcherController() {
            this.bla = 2;
        }
        IdentitySwitcherController.prototype.click = function () {
            var bla = 2;
        };
        IdentitySwitcherController.$inject = [];
        return IdentitySwitcherController;
    }());
    angular.module(IdentitySwitcher.appName)
        .controller("IdentitySwitcherController", IdentitySwitcherController);
})(IdentitySwitcher || (IdentitySwitcher = {}));
//# sourceMappingURL=dnn.identityswitcher.js.map
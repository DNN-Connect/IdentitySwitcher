(function () {
    "use strict";
    angular
        .module("dnn.identityswitcher")
        .factory("identitySwitcherFactory", identitySwitcherFactory);

    function initialiseBaseFactory(moduleInstance) {
        return {
            init: function (moduleInstanceValue) {
                moduleInstance.value = moduleInstanceValue;
                moduleInstance.servicesFramework = $.ServicesFramework(moduleInstance.value.ModuleID);
            }
        };
    }
})();
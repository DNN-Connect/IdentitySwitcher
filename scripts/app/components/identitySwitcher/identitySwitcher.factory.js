(function() {
    "use strict";

    angular.module("dnn.identityswitcher")
        .factory("IdentitySwitcherFactory", identitySwitcherFactory);

    identitySwitcherFactory.$inject = ["$resource", "IdentitySwitcherConstants"];

    function identitySwitcherFactory($resource, config) {
        return $resource(config.restfulApiUrl + "identityswitcher",
            {},
            {
                'getSearchItems': {
                    method: "GET",
                    isArray: true,
                    url: config.restfulApiUrl + "identityswitcher/getsearchitems"
                },
                'getUsers': {
                    method: "GET",
                    params: {
                        searchtext: "@searchText",
                        selectedsearchitem: "@selectedSearchItem",
                        moduleid: "@moduleId"
                    },
                    isArray: true,
                    url: config.restfulApiUrl + "identityswitcher/getusers"
                },
                'switchUser': {
                    method: "GET",
                    params: {
                        selectedUserId: "@searchText",
                        selectedUserName: "@selectedUserName"
                    },
                    isArray: true,
                    url: config.restfulApiUrl + "identityswitcher/switchuser"
                }
            });
    }

    angular.module("dnn.identityswitcher")
        .factory("InitializeBaseFactory", initializeBaseFactory);

    initializeBaseFactory.$inject = ["moduleInstance"];

    function initializeBaseFactory(moduleInstance) {
        return {
            init: function(moduleInstanceValue) {
                moduleInstance.value = moduleInstanceValue;
                moduleInstance.servicesFramework = $.ServicesFramework(moduleInstance.value.ModuleID);
            }
        };
    }
})();
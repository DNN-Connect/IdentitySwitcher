(function () {
    "use strict";

    angular.module("dnn.identityswitcher")
        .factory("IdentitySwitcherFactory", identitySwitcherFactory);

    identitySwitcherFactory.$inject = ["$resource", "IdentitySwitcherConstants"];

    function identitySwitcherFactory($resource, config) {
        return $resource(config.restfulApiUrl + "identityswitcher",
            { login: "@login" },
            {
                'getSearchItems': {
                    method: "GET",
                    //params: { login: "@login" },
                    isArray: true,
                    url: config.restfulApiUrl + "identityswitcher/getsearchitems"
                }
            });
    }
})();
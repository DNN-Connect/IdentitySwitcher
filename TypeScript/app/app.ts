/// <reference types="angular" />
/// <reference types="angular-resource" />

module IdentitySwitcher {
    export const appName = "dnn.identityswitcher";

    /**************************************************************************/
    /* ANGULAR REGISTRATION                                                   */
    /**************************************************************************/
    angular.module(IdentitySwitcher.appName,
        [
            "ngResource"
        ]).factory("IdentitySwitcherFactory", identitySwitcherFactory);

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
}
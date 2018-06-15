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
            ])
        .value("moduleInstance", { value: null, servicesFramework: null });
}
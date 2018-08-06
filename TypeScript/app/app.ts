/// <reference types="angular" />
/// <reference types="angular-resource" />

module IdentitySwitcher {
    class ModuleInstanceValue implements IModuleInstanceValue {
        value: IModuleInstance
    }

    export const appName = "dnn.identityswitcher";

    /**************************************************************************/
    /* ANGULAR REGISTRATION                                                   */
    /**************************************************************************/
    angular.module(IdentitySwitcher.appName,
            [
                "ngResource"
            ])
        .constant("moduleInstance", ModuleInstanceValue);
}
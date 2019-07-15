/// <reference path="..\..\Scripts\typings\angularjs\angular.d.ts" />
/// <reference path="..\..\Scripts\typings\angularjs\angular-resource.d.ts" />

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
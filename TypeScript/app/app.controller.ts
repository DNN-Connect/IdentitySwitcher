module IdentitySwitcher {
    class ApplicationController {
        static $inject = [
            "IdentitySwitcherConstants"
        ];
    }

    /**************************************************************************/
    /* ANGULAR REGISTRATION                                                   */
    /**************************************************************************/
    angular.module(IdentitySwitcher.appName)
        .controller("ApplicationController", ApplicationController);
}
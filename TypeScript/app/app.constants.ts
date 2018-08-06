module IdentitySwitcher {
    class IdentitySwitcherConstants implements IIdentitySwitcherConstants {
        static get getConstants() {
            return new IdentitySwitcherConstants();
        }

        /**************************************************************************/
        /* ctor                                                                   */
        /**************************************************************************/
        constructor() {
            this.apiUrl = "/DesktopModules/IdentitySwitcher/API/";
        }

        /**************************************************************************/
        /* PUBLIC PROPERTIES                                                      */
        /**************************************************************************/
        readonly apiUrl: string;
    }

    /**************************************************************************/
    /* ANGULAR REGISTRATION                                                   */
    /**************************************************************************/
    angular
        .module(IdentitySwitcher.appName)
        .constant("IdentitySwitcherConstants", IdentitySwitcherConstants.getConstants);
}
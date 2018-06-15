module IdentitySwitcher {
    class IdentitySwitcherConstants implements IIdentitySwitcherConstants {
        static get getConstants() {
            return new IdentitySwitcherConstants();
        }

        /**************************************************************************/
        /* ctor                                                                   */
        /**************************************************************************/
        constructor() {
            this.viewTemplatesFolder = "pages/templates/";
            this.restfulApiUrl = "/DesktopModules/Identity%20Switcher/API/";
        }

        /**************************************************************************/
        /* PUBLIC PROPERTIES                                                      */
        /**************************************************************************/
        readonly viewTemplatesFolder: string;
        readonly restfulApiUrl: string;
    }

    /**************************************************************************/
    /* ANGULAR REGISTRATION                                                   */
    /**************************************************************************/
    angular
        .module(IdentitySwitcher.appName)
        .constant("IdentitySwitcherConstants", IdentitySwitcherConstants.getConstants);
}
module IdentitySwitcher {
    class IdentitySwitcherConstants implements IIdentitySwitcherConstants {
        static get getConstants() {
            return new IdentitySwitcherConstants();
        }

        /**************************************************************************/
        /* ctor                                                                   */
        /**************************************************************************/
        constructor() {
            //this.viewFolder = "pages/";
            this.viewTemplatesFolder = "pages/templates/";
            this.restfulApiUrl = "/DesktopModules/Identity%20Switcher/API/";
        }

        /**************************************************************************/
        /* PUBLIC PROPERTIES                                                      */
        /**************************************************************************/
        //readonly viewFolder: string;
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
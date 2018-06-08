module IdentitySwitcher {
    class ApplicationController {
        static $inject = [
            //"$rootScope",
            "IdentitySwitcherConstants"
            //"VrisService"
        ];

        /**************************************************************************/
        /* ctor                                                                   */
        /**************************************************************************/
        constructor(
        //    //private $rootScope: ng.IRootScopeService,
            private constants: IIdentitySwitcherConstants,
            //private initialiseBaseFactory: any
        //    //private vrisService: any
        ) {
        //    this.setTemplates();

        //    //this.registerBroadcasts();

        //    //this.vrisService
        //    //    .getModuleSettings()
        //    //    .then((serverData) => this.onUpdateModuleSettings(serverData));
        }

        //init(moduleInstance): void {
        //    this.initialiseBaseFactory.init(moduleInstance);
        //}


        /**************************************************************************/
        /* PUBLIC PROPERTIES                                                      */
        /**************************************************************************/
        //identitySwitcherView: string = "";
        //userOverview: string = "";
        //userDetails: string = "";
        //activeTemplate: string = "";

        /**************************************************************************/
        /* PRIVATE PROPERTIES                                                     */
        /**************************************************************************/
        //private moduleSettings: any = {};

        /**************************************************************************/
        /* PRIVATE METHODS                                                        */
        /**************************************************************************/
        /*
        * onUserSelected()
        * @param selected
        */
        //private onUserSelected(selected: boolean): void {
        //    this.activeTemplate = selected ? this.userDetails : this.userOverview;
        //}

        /*
       * onUpdateModuleSettings()
       * @param serverData
       */
        //private onUpdateModuleSettings(serverData: any): void {
        //    this.moduleSettings = serverData;
        //    this.setTemplates();
        //}

        /*
        * setTemplates()
        */
        //private setTemplates(): void {
        //    const moduleFolder = this.moduleSettings.moduleFolder;

        //    this.identitySwitcherView = moduleFolder + this.constants.viewTemplatesFolder + "IdentitySwitcherView.html";
        //    //this.userOverview = moduleFolder + this.constants.viewTemplatesFolder + "UserOverview.html";
        //    //this.userDetails = moduleFolder + this.constants.viewTemplatesFolder + "Userdetails.html";

        //    //this.activeTemplate = this.userOverview;
        //}

        /*
          * registerBroadcasts()
          */
        //private registerBroadcasts(): void {
        //    this.$rootScope.$on("userSelected", (e, args) => this.onUserSelected(args.selected));
        //}
    }

    /**************************************************************************/
    /* ANGULAR REGISTRATION                                                   */
    /**************************************************************************/
    angular.module(IdentitySwitcher.appName)
        .controller("ApplicationController", ApplicationController);
}
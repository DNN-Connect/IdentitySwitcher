module IdentitySwitcher {
    class IdentitySwitcherController {
        static $inject = [
            //"toaster",
            //"$rootScope",
            //"RightsService",
            //"RightsDataService"
        ];

        constructor(
            //private toaster,
            //private $rootScope: ng.IRootScopeService,
            //private rightsService: IRightsService,
            //private rightsDataService: IRightsDataService
        ) {
            //this.getSelectedUserRights();
        }

        /**************************************************************************/
        /* PUBLIC PROPERTIES                                                      */
        /**************************************************************************/
        //selectedUserRights: RrsVrisDnnSystem.IActivityType[] = [];

        /**************************************************************************/
        /* PUBLIC METHODS                                                         */
        /**************************************************************************/
        /*
         * returnToOverview() 
         */
        //returnToOverview(): void {
        //    this.rightsDataService.selectedUser = null;
        //    this.selectedUserRights = null;
        //    this.$rootScope.$broadcast("userSelected", { selected: false });
        //}

        /**************************************************************************/
        /* PRIVATE METHODS                                                        */
        /**************************************************************************/
        /*
        * onUserSelected()
        * @param user
        */
        //private getSelectedUserRights(): void {
        //    this.rightsService.getActivities(this.rightsDataService.selectedUser.loginName)
        //        .then((serverData) => {
        //                this.selectedUserRights = serverData;
        //            },
        //            (serverData) => {
        //                this.toaster.pop("error",
        //                    "Er is iets fout gegaan",
        //                    `Het ophalen van details van gebruiker ${this.rightsDataService.selectedUser.name} is mislukt!`);
        //            }
        //        );
        //}
    }

    /**************************************************************************/
    /* ANGULAR REGISTRATION                                                   */
    /**************************************************************************/
    angular.module(IdentitySwitcher.appName)
        .controller("IdentitySwitcherController", IdentitySwitcherController);
}
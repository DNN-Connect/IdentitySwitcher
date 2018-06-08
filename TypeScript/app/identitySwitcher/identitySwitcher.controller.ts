module IdentitySwitcher {
    class IdentitySwitcherController {
        static $inject = [
            //"toaster",
            //"$rootScope",
            //"RightsService",
            //"RightsDataService"
            "IdentitySwitcherService"
        ];

        constructor(
            //private toaster,
            //private $rootScope: ng.IRootScopeService,
            //private rightsService: IRightsService,
            //private rightsDataService: IRightsDataService
            private identitySwitcherService: any
        ) {
            this.obtainSearchItems();
            //this.bla();
            //this.getSelectedUserRights();
        }

        /**************************************************************************/
        /* PUBLIC PROPERTIES                                                      */
        /**************************************************************************/
        //selectedUserRights: RrsVrisDnnSystem.IActivityType[] = [];
        searchItems: string[] = [];
        selectedItem: string;

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

        search(): void {
            var bla = this.selectedItem;
        }

        /**************************************************************************/
        /* PRIVATE METHODS                                                        */
        /**************************************************************************/
        obtainSearchItems(): void {
            this.identitySwitcherService.getSearchItems()
                .then((serverData) => {
                    this.searchItems = serverData;
                },
                () => {
                    //Error
                }
                );
        }


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
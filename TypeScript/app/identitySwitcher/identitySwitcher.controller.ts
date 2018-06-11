module IdentitySwitcher {
    class IdentitySwitcherController {
        static $inject = [
            //"toaster",
            //"$rootScope",
            //"RightsService",
            //"RightsDataService"
            "IdentitySwitcherService"
            //"moduleInstance"
            //"InitializeBaseFactory"
        ];

        constructor(
            //private toaster,
            //private $rootScope: ng.IRootScopeService,
            //private rightsService: IRightsService,
            //private rightsDataService: IRightsDataService
            private identitySwitcherService: any
            //private moduleInstance: any
            //private initializeBaseFactory: any
        ) {
            //this.init(moduleInstance);
            //this.moduleId = this.moduleInstance.value.ModuleID;
            this.obtainSearchItems();
            //this.bla();
            //this.getSelectedUserRights();

            //this.moduleInstance = moduleInstance;
        }

        /**************************************************************************/
        /* PUBLIC PROPERTIES                                                      */
        /**************************************************************************/
        //selectedUserRights: RrsVrisDnnSystem.IActivityType[] = [];
        searchItems: string[] = [];
        selectedItem: string;
        moduleInstance: IModuleInstance;

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
            this.identitySwitcherService.getUsers("mark", this.selectedItem, this.moduleInstance.ModuleID)
                .then((serverData) => {
                        var bla = 2;
                    },
                    () => {
                        //Error
                    }
                );


            var bla = this.selectedItem;
            //var bla2 = moduleInstance;
        }

        init(moduleInstance): void {
            //this.initializeBaseFactory.init(moduleInstance);
            this.moduleInstance = moduleInstance;
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
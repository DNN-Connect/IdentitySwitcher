module IdentitySwitcher {
    class IdentitySwitcherController {
        static $inject = [
            "IdentitySwitcherService"
        ];

        constructor(
            private identitySwitcherService: IIdentitySwitcherService
        ) {
            this.obtainSearchItems();
        }

        /**************************************************************************/
        /* PUBLIC PROPERTIES                                                      */
        /**************************************************************************/
        searchItems: string[] = [];
        selectedSearchText: string;
        selectedItem: string;
        moduleInstance: IModuleInstance;
        foundUsers: IUser[] = [];
        selectedUser: IUser;

        /**************************************************************************/
        /* PUBLIC METHODS                                                         */
        /**************************************************************************/
        /*
        * search()
        */
        search(): void {
            this.identitySwitcherService.getUsers(this.selectedSearchText,
                this.selectedItem,
                this.moduleInstance.ModuleID).then((serverData) => {
                    this.foundUsers = serverData;
                }
            );

        }

        /*
        * userSelected()
        */
        userSelected(): void {
            if (this.moduleInstance.SwitchDirectly) {
                this.switchUser();
            }
        }

        /*
        * switchUser()
        */
        switchUser(): void {
            this.identitySwitcherService.switchUser(this.selectedUser.id, this.selectedUser.userName)
                .then((serverData) => {
                        location.reload();
                    },
                    (serverData) => {
                        location.reload();
                    }
                );
    }

    /*
    * init()
    */
        init(moduleInstance): void {
            this.moduleInstance = moduleInstance;
        }

        /**************************************************************************/
        /* PRIVATE METHODS                                                        */
        /**************************************************************************/
        /*
        * obtainSearchItems()
        */
        private obtainSearchItems(): void {
            this.identitySwitcherService.getSearchItems()
                .then((serverData) => {
                        this.searchItems = serverData;
                    }
                );
        }
    }

    /**************************************************************************/
    /* ANGULAR REGISTRATION                                                   */
    /**************************************************************************/
    angular.module(IdentitySwitcher.appName)
        .controller("IdentitySwitcherController", IdentitySwitcherController);
}
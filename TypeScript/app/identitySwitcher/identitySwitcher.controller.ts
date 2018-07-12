module IdentitySwitcher {
    class IdentitySwitcherController {
        static $inject = [
            "IdentitySwitcherFactory"
        ];

        constructor(
            private identitySwitcherFactory: IIdentitySwitcherFactory
        ) {
            this.getSearchItems();
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
            this.identitySwitcherFactory.getUsers(this.moduleInstance,
                this.selectedSearchText,
                this.selectedItem,
                this.moduleInstance.ModuleID).then((serverData) => {
                    this.foundUsers = serverData.data;
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
            this.identitySwitcherFactory.switchUser(this.moduleInstance,
                    this.selectedUser.id,
                    this.selectedUser.userName)
                .then((serverData) => {
                        // Success
                        location.reload();
                    },
                    (serverData) => {
                        // Error
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
        private getSearchItems(): void {
            this.identitySwitcherFactory.getSearchItems(this.moduleInstance)
                .then((serverData) => {
                        // Success
                        this.searchItems = serverData.data;
                    },
                    (serverData) => {
                        // Error
                        alert('Something went wrong whilst collecting the search items.')
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
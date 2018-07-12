module IdentitySwitcher {
    declare var $: any;

    class IdentitySwitcherController {
        static $inject = [
            "IdentitySwitcherFactory", "moduleInstance"
        ];

        constructor(
            private identitySwitcherFactory: IIdentitySwitcherFactory,
            private moduleInstance: IModuleInstanceValue
        ) {
            //this.getSearchItems();
        }

        /**************************************************************************/
        /* PUBLIC PROPERTIES                                                      */
        /**************************************************************************/
        searchItems: string[] = [];
        selectedSearchText: string;
        selectedItem: string;
       
        foundUsers: IUser[] = [];
        selectedUser: IUser;

        /**************************************************************************/
        /* PUBLIC METHODS                                                         */
        /**************************************************************************/
        /*
        * search()
        */
        search(): void {
            this.identitySwitcherFactory.getUsers(this.moduleInstance.value, this.selectedSearchText,
                this.selectedItem).then((serverData) => {
                    this.foundUsers = serverData.data;
                }
            );

        }

        /*
        * userSelected()
        */
        userSelected(): void {
            if (this.moduleInstance.value.SwitchDirectly) {
                this.switchUser();
            }
        }

        /*
        * switchUser()
        */
        switchUser(): void {
            this.identitySwitcherFactory.switchUser(this.moduleInstance.value, this.selectedUser.id,
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
        init(moduleInstance: IModuleInstance): void {
            this.moduleInstance.value = moduleInstance;
            this.moduleInstance.value.ServicesFramework = $.ServicesFramework(moduleInstance.ModuleID);

            this.getSearchItems();
        }

        /**************************************************************************/
        /* PRIVATE METHODS                                                        */
        /**************************************************************************/
        /*
        * obtainSearchItems()
        */
        private getSearchItems(): void {
            this.identitySwitcherFactory.getSearchItems(this.moduleInstance.value)
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
module IdentitySwitcher {
    declare var $: any;

    class IdentitySwitcherController {
        static $inject = [
            "IdentitySwitcherFactory", "moduleInstance", "$window"
        ];

        constructor(
            private identitySwitcherFactory: IIdentitySwitcherFactory,
            private moduleInstance: IModuleInstanceValue,
            private $window: ng.IWindowService
        ) {
        }

        /**************************************************************************/
        /* PUBLIC PROPERTIES                                                      */
        /**************************************************************************/
        searchItems: string[] = [];
        selectedSearchText: string = "";
        selectedItem: string = "";
       
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
            if (this.moduleInstance.value.SwitchUserInOneClick) {
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
                    },
                    (serverData) => {
                        // Error
                        alert('Something went wrong whilst switching users.');
                    }
            ).then(() => {
                    this.$window.location.reload();
                });
        }

        /*
        * init()
        */
        init(moduleInstance: IModuleInstance): void {
            this.moduleInstance.value = moduleInstance;
            this.moduleInstance.value.ServicesFramework = $.ServicesFramework(moduleInstance.ModuleID);

            // This if/else is called here and not in the constructor because it needs the module instance.
            if (this.moduleInstance.value.SwitchUserInOneClick) {
                // Call the search method with the initial (empty) values so as to obtain all users.
                this.search();
            } else {
                // Else get the search items ready so the user can search by them.
                this.getSearchItems();
            }
        }

        /**************************************************************************/
        /* PRIVATE METHODS                                                        */
        /**************************************************************************/
        /*
        * getSearchItems()
        */
        private getSearchItems(): void {
            this.identitySwitcherFactory.getSearchItems(this.moduleInstance.value)
                .then((serverData) => {
                        // Success
                        this.searchItems = serverData.data;
                    },
                    (serverData) => {
                        // Error
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
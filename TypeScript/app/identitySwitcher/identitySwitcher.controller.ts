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
        request: ISwitchRequest;
        timerId;

        /**************************************************************************/
        /* PUBLIC METHODS                                                         */
        /**************************************************************************/
        /*
        * search()
        */
        search(onlyDefault: boolean = false): void {
            this.request = null;
            if (this.timerId)
                clearInterval(this.timerId);

            this.identitySwitcherFactory.getUsers(this.moduleInstance.value,
                this.selectedSearchText,
                this.selectedItem,
                onlyDefault).then((serverData) => {
                    this.foundUsers = serverData.data.users;
                    angular.forEach(this.foundUsers,
                        (user) => {
                            // See if the selected user matches one from the found users and select that one..
                            if (user.id === serverData.data.selectedUserId) {
                                this.selectedUser = user;
                            } else if (user.id === -1) { // ..else select anonymous.
                                this.selectedUser = user;
                            }
                        });
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
            this.identitySwitcherFactory.switchUser(this.moduleInstance.value,
                this.selectedUser.id,
                this.selectedUser.userName)
                .then((serverData) => {
                    // Success
                    this.request = serverData.data;
                    this.timerId = setInterval(() => this.checkRequestStatus(this.request.requestId), 2000);
                },
                    (serverData) => {
                        // Error
                        alert('Something went wrong whilst switching users.');
                    }
                ).then(() => {
                    if (!this.request.requestAuthorization)
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
                // Else get the anonymous and (if checked) host users and get the search items ready so the user can search by them.
                this.getSearchItems();
                this.search(true);
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
                    this.selectedItem = this.searchItems[0];
                },
                    (serverData) => {
                        // Error
                    }
                );
        }

        private checkRequestStatus(id) {
            this.identitySwitcherFactory.checkStatus(this.moduleInstance.value, id)
                .then((serverData) => {
                    // Success
                    if (serverData.data) {
                        this.$window.location.reload();
                    }
                },
                    (serverData) => {
                        // Error
                        alert('Something went wrong whilst switching users.');
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
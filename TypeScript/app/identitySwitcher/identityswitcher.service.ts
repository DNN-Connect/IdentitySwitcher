module IdentitySwitcher {
    class IdentitySwitcherService implements IIdentitySwitcherService {
        static $inject = [
            "IdentitySwitcherFactory"
        ];

        /**************************************************************************/
        /* ctor                                                                   */
        /**************************************************************************/
        constructor(
            private identitySwitcherFactory: any
        ) {
        }

        /**************************************************************************/
        /* PUBLIC METHODS                                                         */
        /**************************************************************************/
        /*
        * getSearchItems()      
        */
        getSearchItems(): angular.IPromise<any[]> {
            const deferred = this.identitySwitcherFactory.getSearchItems();
            return deferred.$promise;
        }

        /*
        * getUsers()
        * @param searchText
        * @param selectedSearchItem
        * @param moduleId
        */
        getUsers(searchText: string, selectedSearchItem: string, moduleId: number): angular.IPromise<IUser[]> {
            const deferred = this.identitySwitcherFactory.getUsers({
                searchText: searchText,
                selectedSearchItem: selectedSearchItem,
                moduleId: moduleId
            });
            return deferred.$promise;
        }

        /*
        * switchUser()
        * @param selectedUserId
        * @param selectedUserName 
        */
        switchUser(selectedUserId: number, selectedUserName: string): angular.IPromise<any> {
            const deferred = this.identitySwitcherFactory.switchUser({
                selectedUserId: selectedUserId,
                selectedUserName: selectedUserName
            });
            return deferred.$promise;
        }
    }

    /**************************************************************************/
    /* ANGULAR REGISTRATION                                                   */
    /**************************************************************************/
    angular.module(IdentitySwitcher.appName)
        .service("IdentitySwitcherService", IdentitySwitcherService);
}
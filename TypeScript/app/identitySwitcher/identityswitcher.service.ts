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
         * search()
         * @param login
         */
        //search(login: string): angular.IPromise<any> {
        //    const deferred = this.userFactory.search({ login: login });
        //    return deferred.$promise;
        //}

        ///*
        //* getActivities()
        //* @param login
        //*/
        //getActivities(login: string): angular.IPromise<any> {
        //    const deferred = this.activitiesFactory.getActivities({ login: login });
        //    return deferred.$promise;
        //}

        /*
       * getSearchItems()
       * @param login
       */
        getSearchItems(): angular.IPromise<any> {
            const deferred = this.identitySwitcherFactory.getSearchItems();
            return deferred.$promise;
        }
    }

    /**************************************************************************/
    /* ANGULAR REGISTRATION                                                   */
    /**************************************************************************/
    angular.module(IdentitySwitcher.appName)
        .service("IdentitySwitcherService", IdentitySwitcherService);
}
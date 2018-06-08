//module IdentitySwitcher {
//    class IdentitySwitcherFactory {
//        static $inject = [
//            "IdentitySwitcherConstants",
//            "$resource"
//        ];

//        constructor(
//            private config: IIdentitySwitcherConstants,
//            private $resource: any
//        ) {
//        }

//        getSearchItems(): angular.IPromise<any> {
//            return this.$resource(this.config.restfulApiUrl + "identityswitcher",
//                { login: "@login" },
//                {
//                    'getSearchItems': {
//                        method: "GET",
//                        //params: { login: "@login" },
//                        isArray: true,
//                        url: this.config.restfulApiUrl + "identityswitcher/getsearchitems"
//                    }
//                });
//        }
//    }

//    /**************************************************************************/
//    /* ANGULAR REGISTRATION                                                   */
//    /**************************************************************************/
//    angular.module(IdentitySwitcher.appName)
//        .service("IdentitySwitcherFactory", IdentitySwitcherFactory);
//}
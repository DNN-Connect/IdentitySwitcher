declare namespace IdentitySwitcher {
    interface IIdentitySwitcherConstants {
        //readonly viewFolder: string;
        readonly viewTemplatesFolder: string;
        readonly restfulApiUrl: string;
    }

    interface IIdentitySwitcherService {
        //search(login: string): angular.IPromise<any>;
        getSearchItems(): angular.IPromise<any>;
    }

    //interface IRightsDataService {
    //    searchInput: string;
    //    foundUsers: RrsVrisDnnSystem.IUser[];
    //    selectedUser: RrsVrisDnnSystem.IUser;
    //    selectedUserRights: RrsVrisDnnSystem.IActivityType[];
    //}
}
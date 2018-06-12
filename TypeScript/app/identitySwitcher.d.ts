declare namespace IdentitySwitcher {
    interface IIdentitySwitcherConstants {
        //readonly viewFolder: string;
        readonly viewTemplatesFolder: string;
        readonly restfulApiUrl: string;
    }

    interface IIdentitySwitcherService {
        //search(login: string): angular.IPromise<any>;
        getSearchItems(): angular.IPromise<any>;
        getUsers(searchText: string, selectedSearchItem: string, moduleId: number): angular.IPromise<any>;
    }

    interface IUser {
        id: number;
        userAndDisplayName: string;
    }

    export interface IModuleInstance {
        ModuleID: string;
    }

    //interface IRightsDataService {
    //    searchInput: string;
    //    foundUsers: RrsVrisDnnSystem.IUser[];
    //    selectedUser: RrsVrisDnnSystem.IUser;
    //    selectedUserRights: RrsVrisDnnSystem.IActivityType[];
    //}
}
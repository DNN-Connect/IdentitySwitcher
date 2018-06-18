declare namespace IdentitySwitcher {
    interface IIdentitySwitcherConstants {
        readonly viewTemplatesFolder: string;
        readonly restfulApiUrl: string;
    }

    interface IIdentitySwitcherService {
        switchUser(selectedUserId: number, selectedUserName: string): angular.IPromise<any>;
        getSearchItems(): angular.IPromise<any>;
        getUsers(searchText: string, selectedSearchItem: string, moduleId: number): angular.IPromise<IUser[]>;
    }

    interface IUser {
        id: number;
        userName: string;
        userAndDisplayName: string;
    }

    export interface IModuleInstance {
        ModuleID: number;
        SwitchDirectly: boolean;
    }
}
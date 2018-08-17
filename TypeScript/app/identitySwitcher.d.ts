declare namespace IdentitySwitcher {
    interface IIdentitySwitcherConstants {
        readonly apiUrl: string;
    }

    interface IIdentitySwitcherFactory {
        getSearchItems(moduleInstance: IModuleInstance): angular.IHttpPromise<string[]>;
        getUsers(moduleInstance: IModuleInstance, selectedSearchText: string, selectedSearchItem: string, onlyDefault: boolean): angular.IHttpPromise<IUser[]>;
        switchUser(moduleInstance: IModuleInstance, selectedUserId: number, selectedUserName: string): angular.IHttpPromise<void>;
    }

    interface IUser {
        id: number;
        userName: string;
        userAndDisplayName: string;
    }

    interface IModuleInstanceValue {
        value: IModuleInstance;
    }

    export interface IModuleInstance {
        ServicesFramework: any;
        ModuleID: number;
        SwitchUserInOneClick: boolean;
    }
}
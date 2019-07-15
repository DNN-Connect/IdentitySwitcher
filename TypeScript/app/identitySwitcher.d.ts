declare namespace IdentitySwitcher {
    interface IIdentitySwitcherConstants {
		readonly apiUrl: string;
    }

    interface IIdentitySwitcherFactory {
        getSearchItems(appPath: string, moduleInstance: IModuleInstance): angular.IHttpPromise<string[]>;
		getUsers(appPath: string, moduleInstance: IModuleInstance, selectedSearchText: string, selectedSearchItem: string, onlyDefault: boolean): angular.IHttpPromise<IUserCollection>;
		switchUser(appPath: string, moduleInstance: IModuleInstance, selectedUserId: number, selectedUserName: string): angular.IHttpPromise<void>;
    }

    interface IUser {
        id: number;
        userName: string;
        userAndDisplayName: string;
    }

    interface IUserCollection {
        users: IUser[];
        selectedUserId: number;
    }

    interface IModuleInstanceValue {
        value: IModuleInstance;
    }

    export interface IModuleInstance {
        ServicesFramework: any;
        ModuleID: number;
        FilterText: string;
        SwitchToText: string;
        SwitchUserInOneClick: boolean;
    }
}
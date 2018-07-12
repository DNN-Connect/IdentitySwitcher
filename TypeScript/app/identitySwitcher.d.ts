declare namespace IdentitySwitcher {
    interface IIdentitySwitcherConstants {
        readonly viewTemplatesFolder: string;
        readonly restfulApiUrl: string;
    }

    interface IIdentitySwitcherFactory {
        getSearchItems(moduleInstance: IModuleInstance): angular.IHttpPromise<string[]>;
        getUsers(moduleInstance: IModuleInstance, selectedSearchText: string, selectedSearchItem: string, moduleID: number): angular.IHttpPromise<IUser[]>;
        switchUser(moduleInstance: IModuleInstance, selectedUserId: number, selectedUserName: string): angular.IHttpPromise<void>;
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
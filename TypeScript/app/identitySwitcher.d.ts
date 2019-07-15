declare namespace IdentitySwitcher {
    interface IIdentitySwitcherConstants {
        readonly viewTemplatesFolder: string;
        readonly restfulApiUrl: string;
    }

    interface IIdentitySwitcherFactory {
        getSearchItems(moduleInstance: IModuleInstance): angular.IHttpPromise<string[]>;
        getUsers(moduleInstance: IModuleInstance, selectedSearchText: string, selectedSearchItem: string): angular.IHttpPromise<IUser[]>;
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
        ApplicationPath: string;
        ModuleID: number;
        ServicesFramework: any;
        SwitchDirectly: boolean;
    }
}
declare namespace IdentitySwitcher {
    interface IIdentitySwitcherConstants {
        readonly apiUrl: string;
    }

    interface IIdentitySwitcherFactory {
        getSearchItems(moduleInstance: IModuleInstance): angular.IHttpPromise<string[]>;
        getUsers(moduleInstance: IModuleInstance, selectedSearchText: string, selectedSearchItem: string, onlyDefault: boolean): angular.IHttpPromise<IUserCollection>;
        switchUser(moduleInstance: IModuleInstance, selectedUserId: number, selectedUserName: string): angular.IHttpPromise<ISwitchRequest>;
        checkStatus(moduleInstance: IModuleInstance, id: number): angular.IHttpPromise<boolean>;
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

    interface ISwitchRequest {
        requestAuthorization: boolean;
        requestId: string;
    }

    interface IModuleInstanceValue {
        value: IModuleInstance;
    }

    export interface IModuleInstance {
        ApplicationPath: string;
        ServicesFramework: any;
        ModuleID: number;
        PortalId: number;
        FilterText: string;
        SwitchToText: string;
        WaitingForConfirmation: string;
        SwitchUserInOneClick: boolean;
    }
}
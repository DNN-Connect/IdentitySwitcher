namespace DNN.Modules.IdentitySwitcher.Components
{
    using System.IO;
    using DotNetNuke.Common;
    using DotNetNuke.Entities.Modules;
    using DotNetNuke.Web.Client.ClientResourceManagement;
    using global::IdentitySwitcher.DotNetNuke.Web.Client;

    public abstract class IdentitySwitcherPortalModuleBase : PortalModuleBase
    {
        public const string DistributionFolder = "dist";

        private string _moduleFolderName;

        public virtual string ScriptFolderName { get; } = "Scripts";

        public virtual string JsResourcesFolderName { get; } = "jsResources";

        public virtual string ModuleAngularAppFolderName => Path.Combine(
            this.ModuleFolderName, this.ScriptFolderName, "app");

        public virtual string ModuleFolderName
        {
            get
                {
                    if (string.IsNullOrWhiteSpace(this._moduleFolderName))
                    {
                        this._moduleFolderName =
                            Path.Combine(Globals.DesktopModulePath, this.ModuleConfiguration.DesktopModule.FolderName);
                    }
                    return this._moduleFolderName;
                }
        }

        protected virtual string ModuleScriptFolder => Path.Combine(this.ModuleFolderName, this.ScriptFolderName);

        protected virtual string ModuleJsResourcesFolder => Path.Combine(
            this.ModuleFolderName, this.JsResourcesFolderName);

        protected virtual ModuleInstanceBase GetModuleInstance()
        {
            return IdentitySwitcherClient.GetModuleInstance<ModuleInstanceBase>(this);
        }

        protected void RegisterScript(string folder, string fileName, IdentitySwitcherFileOrder.Js priority)
        {
            var scriptPath = string.IsNullOrWhiteSpace(folder) ? fileName : Path.Combine(folder, fileName);
            ClientResourceManager.RegisterScript(this.Page, scriptPath, (int) priority);
        }
    }
}
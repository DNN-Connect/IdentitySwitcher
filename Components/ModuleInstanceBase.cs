using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DNN.Modules.IdentitySwitcher.Components
{
    using DNN.Modules.IdentitySwitcher.Components.Model;
    using DotNetNuke.Entities.Modules;

    public class ModuleInstanceBase
    {
        #region Properties
        /// <summary>
        /// Gets or sets the module identifier.
        /// </summary>
        /// <value>
        /// The module identifier.
        /// </value>
        public int ModuleID { get; set; }

        public bool SwitchDirectly
        {
            get
            {
                var moduleInfo = new ModuleController().GetModule(this.ModuleID);
                var repository = new IdentitySwitcherModuleSettingsRepository();
                var settings = repository.GetSettings(moduleInfo);

                return settings.UserSwitchingSpeed == UserSwitchingSpeed.UsingOneClick;
            }
        }

        #endregion
    }
}
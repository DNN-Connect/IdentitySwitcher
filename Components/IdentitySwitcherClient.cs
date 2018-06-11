using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DNN.Modules.IdentitySwitcher.Components
{
    using DotNetNuke.Common.Utilities;

    public class IdentitySwitcherClient
    {
        /// <summary>
        /// Gets the module instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="moduleControl">The module control.</param>
        /// <returns></returns>
        public static T GetModuleInstance<T>(IdentitySwitcherPortalModuleBase moduleControl) where T : ModuleInstanceBase, new()
        {
            T result = new T();

            if (moduleControl != null)
            {
                result.ModuleID = moduleControl.ModuleId;
            }

            return result;
        }
    }
}
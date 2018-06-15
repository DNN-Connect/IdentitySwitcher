namespace DNN.Modules.IdentitySwitcher.Components
{
    public class IdentitySwitcherClient
    {
        /// <summary>
        ///     Gets the module instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="moduleControl">The module control.</param>
        /// <returns></returns>
        public static T GetModuleInstance<T>(IdentitySwitcherPortalModuleBase moduleControl)
            where T : ModuleInstanceBase, new()
        {
            var result = new T();

            if (moduleControl != null)
            {
                result.ModuleID = moduleControl.ModuleId;
            }

            return result;
        }
    }
}
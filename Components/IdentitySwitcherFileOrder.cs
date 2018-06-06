using DotNetNuke.Web.Client;

// ReSharper disable InconsistentNaming

namespace IdentitySwitcher.DotNetNuke.Web.Client
{
    /// <summary>
    /// </summary>
    public class IdentitySwitcherFileOrder
    {
        public enum Css
        {
            AngularUI = FileOrder.Css.DefaultPriority + 10,

            UI = AngularUI + 10
        }

        public enum Js
        {
            Angular = FileOrder.Js.DefaultPriority + 10,

            AngularUI = Angular + 50,

            AngularAddOn = AngularUI + 50,

            AngularDnn = AngularAddOn + 50,

            Libraries = AngularDnn + 50,

            AngularApp = Libraries + 50,

            AngularCustomApp = AngularApp + 50
        }
    }
}
#region Copyright

// 
// DotNetNuke® - http://www.dotnetnuke.com
// Copyright (c) 2002-2018
// by DotNetNuke Corporation
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
// the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
// to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions 
// of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.
//

#endregion

// ReSharper disable InconsistentNaming

namespace IdentitySwitcher.DotNetNuke.Web.Client
{
    using global::DotNetNuke.Web.Client;

    /// <summary>
    /// </summary>
    public class IdentitySwitcherFileOrder
    {
        /// <summary>
        /// </summary>
        public enum Css
        {
            /// <summary>
            ///     The angular UI
            /// </summary>
            AngularUI = FileOrder.Css.DefaultPriority + 10,

            /// <summary>
            ///     The UI
            /// </summary>
            UI = AngularUI + 10
        }

        /// <summary>
        /// </summary>
        public enum Js
        {
            /// <summary>
            ///     The angular
            /// </summary>
            Angular = FileOrder.Js.DefaultPriority + 10,

            /// <summary>
            ///     The angular UI
            /// </summary>
            AngularUI = Angular + 50,

            /// <summary>
            ///     The angular add on
            /// </summary>
            AngularAddOn = AngularUI + 50,

            /// <summary>
            ///     The angular DNN
            /// </summary>
            AngularDnn = AngularAddOn + 50,

            /// <summary>
            ///     The libraries
            /// </summary>
            Libraries = AngularDnn + 50,

            /// <summary>
            ///     The angular application
            /// </summary>
            AngularApp = Libraries + 50,

            /// <summary>
            ///     The angular custom application
            /// </summary>
            AngularCustomApp = AngularApp + 50
        }
    }
}
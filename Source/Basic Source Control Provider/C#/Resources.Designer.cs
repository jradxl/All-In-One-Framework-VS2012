﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18046
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Microsoft.Samples.VisualStudio.SourceControlIntegration.BasicSccProvider {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Microsoft.Samples.VisualStudio.SourceControlIntegration.BasicSccProvider.Resource" +
                            "s", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Managed Source Control Sample Basic Provider.
        /// </summary>
        internal static string _100 {
            get {
                return ResourceManager.GetString("100", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This package implements the basic functionalities of a source control provider package..
        /// </summary>
        internal static string _101 {
            get {
                return ResourceManager.GetString("101", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Icon similar to (Icon).
        /// </summary>
        internal static System.Drawing.Icon _400 {
            get {
                object obj = ResourceManager.GetObject("400", resourceCulture);
                return ((System.Drawing.Icon)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap _500 {
            get {
                object obj = ResourceManager.GetObject("500", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap _501 {
            get {
                object obj = ResourceManager.GetObject("501", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &quot;Press Cancel to cancel this OnApply.  OK to continue&quot;.
        /// </summary>
        internal static string ApplyProviderOptions {
            get {
                return ResourceManager.GetString("ApplyProviderOptions", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Managed Source Code Control Basic Provider.
        /// </summary>
        internal static string ProviderName {
            get {
                return ResourceManager.GetString("ProviderName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Sample source control provider ToolWindow.
        /// </summary>
        internal static string ToolWindowCaption {
            get {
                return ResourceManager.GetString("ToolWindowCaption", resourceCulture);
            }
        }
    }
}

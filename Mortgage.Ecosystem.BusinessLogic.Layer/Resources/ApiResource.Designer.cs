﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mortgage.Ecosystem.BusinessLogic.Layer.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ApiResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ApiResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Mortgage.Ecosystem.BusinessLogic.Layer.Resources.ApiResource", typeof(ApiResource).Assembly);
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
        ///   Looks up a localized string similar to application/json.
        /// </summary>
        internal static string ApplicationJson {
            get {
                return ResourceManager.GetString("ApplicationJson", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://testcorebanking.fmbn.gov.ng:4438/api/v1/.
        /// </summary>
        internal static string baseAddress {
            get {
                return ResourceManager.GetString("baseAddress", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to customer/iscustomer-exist.
        /// </summary>
        internal static string customerExist {
            get {
                return ResourceManager.GetString("customerExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://remitademo.net/remita/exapp/api/v1/send/api/echannelsvc/merchant/api/paymentinit/.
        /// </summary>
        internal static string generateRRR {
            get {
                return ResourceManager.GetString("generateRRR", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to loan/get-loans.
        /// </summary>
        internal static string getLoans {
            get {
                return ResourceManager.GetString("getLoans", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to customer/individual-existing.
        /// </summary>
        internal static string individualExisting {
            get {
                return ResourceManager.GetString("individualExisting", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to loan/loan-affordability-check.
        /// </summary>
        internal static string loanAffordability {
            get {
                return ResourceManager.GetString("loanAffordability", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to loan/loan-application.
        /// </summary>
        internal static string loanApplicationUpdate {
            get {
                return ResourceManager.GetString("loanApplicationUpdate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to product.
        /// </summary>
        internal static string loanProduct {
            get {
                return ResourceManager.GetString("loanProduct", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to loan/loan-schedule.
        /// </summary>
        internal static string loanSchedule {
            get {
                return ResourceManager.GetString("loanSchedule", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://remitademo.net/remita/exapp/api/v1/send/api/echannelsvc/.
        /// </summary>
        internal static string rrrStatus {
            get {
                return ResourceManager.GetString("rrrStatus", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to customer/update-customer.
        /// </summary>
        internal static string updateCustomer {
            get {
                return ResourceManager.GetString("updateCustomer", resourceCulture);
            }
        }
    }
}

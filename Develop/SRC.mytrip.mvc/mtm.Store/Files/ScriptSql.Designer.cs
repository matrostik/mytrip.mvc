﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace mtm.Store.Files {
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
    internal class ScriptSql {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ScriptSql() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("mtm.Store.Files.ScriptSql", typeof(ScriptSql).Assembly);
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
        ///   Looks up a localized string similar to IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N&apos;[dbo].[FK_mytrip_storedepartment_mytrip_storedepartment]&apos;) AND parent_object_id = OBJECT_ID(N&apos;[dbo].[mytrip_storedepartment]&apos;))
        ///ALTER TABLE [dbo].[mytrip_storedepartment] DROP CONSTRAINT [FK_mytrip_storedepartment_mytrip_storedepartment]
        ///IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N&apos;[dbo].[FK_mytrip_storedepartment_mytrip_storesale]&apos;) AND parent_object_id = OBJECT_ID(N&apos;[dbo].[mytrip_storedepartment]&apos;))
        ///ALT [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string deleteScriptMSSQL {
            get {
                return ResourceManager.GetString("deleteScriptMSSQL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DROP TABLE IF EXISTS mytrip_storevotes;                 
        ///DROP TABLE IF EXISTS mytrip_storeorderisproduct;
        ///DROP TABLE IF EXISTS mytrip_storeproduct;
        ///DROP TABLE IF EXISTS mytrip_storeproducer;
        ///DROP TABLE IF EXISTS mytrip_storeorder;
        ///DROP TABLE IF EXISTS mytrip_storedepartment;
        ///DROP TABLE IF EXISTS mytrip_storeseller;
        ///DROP TABLE IF EXISTS mytrip_storesale;
        ///DROP TABLE IF EXISTS mytrip_storeprofile;.
        /// </summary>
        internal static string deleteScriptMysql {
            get {
                return ResourceManager.GetString("deleteScriptMysql", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N&apos;[dbo].[mytrip_storeseller]&apos;) AND type in (N&apos;U&apos;))
        ///BEGIN
        ///CREATE TABLE [dbo].[mytrip_storeseller](
        ///[SellerId] [int] NOT NULL,
        ///[Organization] [nvarchar](256) NULL,
        ///[Address] [nvarchar](max) NULL,
        ///[Phone] [nvarchar](256) NULL,
        ///[Email] [nvarchar](256) NULL,
        ///[OrganizationINN] [nvarchar](256) NULL,
        ///[OrganizationKPP] [nvarchar](256) NULL,
        ///[BankAccountSeller] [nvarchar](256) NULL,
        ///[BankAccountBIK] [nvarchar](256) NULL,
        ///[Bank] [nvarchar [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string ScriptMSSQL {
            get {
                return ResourceManager.GetString("ScriptMSSQL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CREATE TABLE IF NOT EXISTS mytrip_storeprofile(
        ///ProfileId INT (11) NOT NULL,
        ///UserName VARCHAR (256) NOT NULL,
        ///UserEmail VARCHAR (256) NOT NULL,
        ///Address TEXT DEFAULT NULL,
        ///Phone VARCHAR (256) DEFAULT NULL,
        ///IsAnonym BIT (1) NOT NULL,
        ///FirstName VARCHAR (256) NOT NULL,
        ///LastName VARCHAR (256) DEFAULT NULL,
        ///Organization VARCHAR (256) DEFAULT NULL,
        ///OrganizationINN VARCHAR (256) DEFAULT NULL,
        ///OrganizationKPP VARCHAR (256) DEFAULT NULL,
        ///UserIP VARCHAR (256) NOT NULL,
        ///PRIMARY KEY (ProfileId)
        ///)
        ///ENGINE = [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string ScriptMySql {
            get {
                return ResourceManager.GetString("ScriptMySql", resourceCulture);
            }
        }
    }
}

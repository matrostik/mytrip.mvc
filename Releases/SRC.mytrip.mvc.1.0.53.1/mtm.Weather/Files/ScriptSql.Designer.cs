﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace mtm.Weather.Files {
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("mtm.Weather.Files.ScriptSql", typeof(ScriptSql).Assembly);
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
        ///   Looks up a localized string similar to IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N&apos;[dbo].[mytrip_weather]&apos;) AND type in (N&apos;U&apos;))
        ///DROP TABLE [dbo].[mytrip_weather].
        /// </summary>
        internal static string deleteScriptMSSQL {
            get {
                return ResourceManager.GetString("deleteScriptMSSQL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DROP TABLE IF EXISTS mytrip_weather;.
        /// </summary>
        internal static string deleteScriptMysql {
            get {
                return ResourceManager.GetString("deleteScriptMysql", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N&apos;[dbo].[mytrip_weather]&apos;) AND type in (N&apos;U&apos;))
        ///BEGIN
        ///CREATE TABLE [dbo].[mytrip_weather] (
        ///[weatherId] [int]  NOT NULL,
        ///[Title] [nvarchar](256)  NOT NULL,
        ///[UrlXml] [nvarchar](256)  NOT NULL,
        ///[Culture] [nvarchar](50)  NOT NULL,
        ///[AllCulture] [bit]  NOT NULL,
        ///[UserName] [nvarchar](100)  NOT NULL,
        ///[CreateDate] [datetime]  NOT NULL,
        ///[VisibleInformer] [bit]  NOT NULL,
        ///CONSTRAINT [PK_mytrip_weather] PRIMARY KEY CLUSTERED 
        ///(
        ///[weatherI [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string ScriptMSSQL {
            get {
                return ResourceManager.GetString("ScriptMSSQL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CREATE TABLE IF NOT EXISTS mytrip_weather(
        ///weatherId INT(11) NOT NULL,
        ///Title VARCHAR(256) NOT NULL,
        ///UrlXml VARCHAR(256) NOT NULL,
        ///Culture VARCHAR(50) NOT NULL,
        ///AllCulture BIT(1) NOT NULL,
        ///UserName VARCHAR(100) NOT NULL,
        ///CreateDate DATETIME NOT NULL,            
        ///VisibleInformer BIT(1) NOT NULL,
        ///PRIMARY KEY (weatherId)
        ///)
        ///ENGINE = INNODB
        ///AVG_ROW_LENGTH = 4096
        ///CHARACTER SET cp1251
        ///COLLATE cp1251_general_ci;.
        /// </summary>
        internal static string ScriptMySql {
            get {
                return ResourceManager.GetString("ScriptMySql", resourceCulture);
            }
        }
    }
}

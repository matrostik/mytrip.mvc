﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace mtm.Articles.Files {
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("mtm.Articles.Files.ScriptSql", typeof(ScriptSql).Assembly);
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
        ///   Looks up a localized string similar to IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N&apos;[dbo].[FK_mytrip_Article_mytrip_ArticleCategory]&apos;) AND parent_object_id = OBJECT_ID(N&apos;[dbo].[mytrip_articles]&apos;))
        ///ALTER TABLE [dbo].[mytrip_articles] DROP CONSTRAINT [FK_mytrip_Article_mytrip_ArticleCategory]
        ///IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N&apos;[dbo].[FK_mytrip_articles_mytrip_articles]&apos;) AND parent_object_id = OBJECT_ID(N&apos;[dbo].[mytrip_articles]&apos;))
        ///ALTER TABLE [dbo].[mytrip_articles] DROP CONSTRA [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string deleteScriptMSSQL {
            get {
                return ResourceManager.GetString("deleteScriptMSSQL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DROP TABLE IF EXISTS mytrip_articlesvotes;               
        ///DROP TABLE IF EXISTS mytrip_articlesintags;
        ///DROP TABLE IF EXISTS mytrip_commentvotes;
        ///DROP TABLE IF EXISTS mytrip_articlescomments;
        ///DROP TABLE IF EXISTS mytrip_articlessubscription;
        ///DROP TABLE IF EXISTS mytrip_articles;
        ///DROP TABLE IF EXISTS mytrip_articlestag;
        ///DROP TABLE IF EXISTS mytrip_articlescategory;.
        /// </summary>
        internal static string deleteScriptMysql {
            get {
                return ResourceManager.GetString("deleteScriptMysql", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N&apos;[dbo].[mytrip_articlestag]&apos;) AND type in (N&apos;U&apos;))
        ///BEGIN
        ///CREATE TABLE [dbo].[mytrip_articlestag](
        ///[TagId] [int] NOT NULL,
        ///[TagName] [nvarchar](256) NOT NULL,
        ///[Path] [nvarchar](256) NOT NULL,
        ///CONSTRAINT [PK_mytrip_ArticlesTag] PRIMARY KEY CLUSTERED 
        ///(
        ///[TagId] ASC
        ///)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
        ///) ON [PRIMARY]
        ///END
        ///IF NOT EXIS [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string ScriptMSSQL {
            get {
                return ResourceManager.GetString("ScriptMSSQL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CREATE TABLE IF NOT EXISTS mytrip_articlescategory(
        ///CategoryId INT(11) NOT NULL,
        ///Title VARCHAR(256) NOT NULL,
        ///Path VARCHAR(256) NOT NULL,
        ///CreateDate DATETIME NOT NULL,
        ///UserName VARCHAR(100) NOT NULL,
        ///UserEmail VARCHAR(100) NOT NULL,
        ///SeparateBlock BIT(1) NOT NULL,
        ///Blog BIT(1) NOT NULL,
        ///Views INT(11) NOT NULL,
        ///SubCategoryId INT(11) NOT NULL,
        ///Culture VARCHAR(100) NOT NULL,
        ///AllCulture BIT(1) NOT NULL,
        ///PRIMARY KEY (CategoryId),
        ///INDEX IX_mytrip_ArticleCategory_mytrip_ArticleCategory (SubCategoryId), [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string ScriptMySql {
            get {
                return ResourceManager.GetString("ScriptMySql", resourceCulture);
            }
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Mytrip.Rssparser.Repository.DataEntities
{
    public partial class mytrip_rssparser
    {
        #region Primitive Properties
    
        public virtual int RssparserId
        {
            get;
            set;
        }
    
        public virtual string Title
        {
            get;
            set;
        }
    
        public virtual string Path
        {
            get;
            set;
        }
    
        public virtual System.DateTime CreateDate
        {
            get;
            set;
        }
    
        public virtual string UserName
        {
            get;
            set;
        }
    
        public virtual int Views
        {
            get;
            set;
        }
    
        public virtual string Culture
        {
            get;
            set;
        }
    
        public virtual bool AllCulture
        {
            get;
            set;
        }
    
        public virtual string RssUrl
        {
            get;
            set;
        }
    
        public virtual string ImageUrl
        {
            get;
            set;
        }

        #endregion
    }
}
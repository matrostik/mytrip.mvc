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

namespace mtm.Weather.Repository.DataEntities
{
    public partial class mytrip_weather
    {
        #region Primitive Properties
    
        public virtual int weatherId
        {
            get;
            set;
        }
    
        public virtual string Title
        {
            get;
            set;
        }
    
        public virtual string UrlXml
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
    
        public virtual string UserName
        {
            get;
            set;
        }
    
        public virtual System.DateTime CreateDate
        {
            get;
            set;
        }
    
        public virtual bool VisibleInformer
        {
            get;
            set;
        }

        #endregion
    }
}

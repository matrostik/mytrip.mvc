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

namespace Mytrip.Store.Repository.DataEntities
{
    public partial class mytrip_storeproducer
    {
        #region Primitive Properties
    
        public virtual int ProducerId
        {
            get;
            set;
        }
    
        public virtual string Title
        {
            get;
            set;
        }
    
        public virtual string Body
        {
            get;
            set;
        }
    
        public virtual string Path
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
    
        public virtual System.DateTime CreationDate
        {
            get;
            set;
        }
    
        public virtual string Image
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual ICollection<mytrip_storeproduct> mytrip_storeproduct
        {
            get
            {
                if (_mytrip_storeproduct == null)
                {
                    var newCollection = new FixupCollection<mytrip_storeproduct>();
                    newCollection.CollectionChanged += Fixupmytrip_storeproduct;
                    _mytrip_storeproduct = newCollection;
                }
                return _mytrip_storeproduct;
            }
            set
            {
                if (!ReferenceEquals(_mytrip_storeproduct, value))
                {
                    var previousValue = _mytrip_storeproduct as FixupCollection<mytrip_storeproduct>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= Fixupmytrip_storeproduct;
                    }
                    _mytrip_storeproduct = value;
                    var newValue = value as FixupCollection<mytrip_storeproduct>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += Fixupmytrip_storeproduct;
                    }
                }
            }
        }
        private ICollection<mytrip_storeproduct> _mytrip_storeproduct;

        #endregion
        #region Association Fixup
    
        private void Fixupmytrip_storeproduct(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (mytrip_storeproduct item in e.NewItems)
                {
                    item.mytrip_storeproducer = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (mytrip_storeproduct item in e.OldItems)
                {
                    if (ReferenceEquals(item.mytrip_storeproducer, this))
                    {
                        item.mytrip_storeproducer = null;
                    }
                }
            }
        }

        #endregion
    }
}
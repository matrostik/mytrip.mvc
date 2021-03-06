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

namespace mtm.Store.Repository.DataEntities
{
    public partial class mytrip_storeorder
    {
        #region Primitive Properties
    
        public virtual string AccountPage
        {
            get;
            set;
        }
    
        public virtual System.DateTime CreationDate
        {
            get;
            set;
        }
    
        public virtual string Culture
        {
            get;
            set;
        }
    
        public virtual Nullable<System.DateTime> DateAccount
        {
            get;
            set;
        }
    
        public virtual decimal Delivery
        {
            get;
            set;
        }
    
        public virtual string ManagerName
        {
            get;
            set;
        }
    
        public virtual string MoneyId
        {
            get;
            set;
        }
    
        public virtual Nullable<int> NamberAccount
        {
            get;
            set;
        }
    
        public virtual int OrderId
        {
            get;
            set;
        }
    
        public virtual string PriceInWords
        {
            get;
            set;
        }
    
        public virtual int ProfileId
        {
            get { return _profileId; }
            set
            {
                if (_profileId != value)
                {
                    if (mytrip_storeprofile != null && mytrip_storeprofile.ProfileId != value)
                    {
                        mytrip_storeprofile = null;
                    }
                    _profileId = value;
                }
            }
        }
        private int _profileId;
    
        public virtual int Status
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual ICollection<mytrip_storeorderisproduct> mytrip_storeorderisproduct
        {
            get
            {
                if (_mytrip_storeorderisproduct == null)
                {
                    var newCollection = new FixupCollection<mytrip_storeorderisproduct>();
                    newCollection.CollectionChanged += Fixupmytrip_storeorderisproduct;
                    _mytrip_storeorderisproduct = newCollection;
                }
                return _mytrip_storeorderisproduct;
            }
            set
            {
                if (!ReferenceEquals(_mytrip_storeorderisproduct, value))
                {
                    var previousValue = _mytrip_storeorderisproduct as FixupCollection<mytrip_storeorderisproduct>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= Fixupmytrip_storeorderisproduct;
                    }
                    _mytrip_storeorderisproduct = value;
                    var newValue = value as FixupCollection<mytrip_storeorderisproduct>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += Fixupmytrip_storeorderisproduct;
                    }
                }
            }
        }
        private ICollection<mytrip_storeorderisproduct> _mytrip_storeorderisproduct;
    
        public virtual mytrip_storeprofile mytrip_storeprofile
        {
            get { return _mytrip_storeprofile; }
            set
            {
                if (!ReferenceEquals(_mytrip_storeprofile, value))
                {
                    var previousValue = _mytrip_storeprofile;
                    _mytrip_storeprofile = value;
                    Fixupmytrip_storeprofile(previousValue);
                }
            }
        }
        private mytrip_storeprofile _mytrip_storeprofile;

        #endregion
        #region Association Fixup
    
        private void Fixupmytrip_storeprofile(mytrip_storeprofile previousValue)
        {
            if (previousValue != null && previousValue.mytrip_storeorder.Contains(this))
            {
                previousValue.mytrip_storeorder.Remove(this);
            }
    
            if (mytrip_storeprofile != null)
            {
                if (!mytrip_storeprofile.mytrip_storeorder.Contains(this))
                {
                    mytrip_storeprofile.mytrip_storeorder.Add(this);
                }
                if (ProfileId != mytrip_storeprofile.ProfileId)
                {
                    ProfileId = mytrip_storeprofile.ProfileId;
                }
            }
        }
    
        private void Fixupmytrip_storeorderisproduct(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (mytrip_storeorderisproduct item in e.NewItems)
                {
                    item.mytrip_storeorder = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (mytrip_storeorderisproduct item in e.OldItems)
                {
                    if (ReferenceEquals(item.mytrip_storeorder, this))
                    {
                        item.mytrip_storeorder = null;
                    }
                }
            }
        }

        #endregion
    }
}

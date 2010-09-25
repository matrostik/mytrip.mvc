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
    public partial class mytrip_storevotes
    {
        #region Primitive Properties
    
        public virtual int VotesId
        {
            get;
            set;
        }
    
        public virtual int ProductId
        {
            get { return _productId; }
            set
            {
                if (_productId != value)
                {
                    if (mytrip_storeproduct != null && mytrip_storeproduct.ProductId != value)
                    {
                        mytrip_storeproduct = null;
                    }
                    _productId = value;
                }
            }
        }
        private int _productId;
    
        public virtual int Vote
        {
            get;
            set;
        }
    
        public virtual string UserName
        {
            get;
            set;
        }
    
        public virtual string Reviews
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual mytrip_storeproduct mytrip_storeproduct
        {
            get { return _mytrip_storeproduct; }
            set
            {
                if (!ReferenceEquals(_mytrip_storeproduct, value))
                {
                    var previousValue = _mytrip_storeproduct;
                    _mytrip_storeproduct = value;
                    Fixupmytrip_storeproduct(previousValue);
                }
            }
        }
        private mytrip_storeproduct _mytrip_storeproduct;

        #endregion
        #region Association Fixup
    
        private void Fixupmytrip_storeproduct(mytrip_storeproduct previousValue)
        {
            if (previousValue != null && previousValue.mytrip_storevotes.Contains(this))
            {
                previousValue.mytrip_storevotes.Remove(this);
            }
    
            if (mytrip_storeproduct != null)
            {
                if (!mytrip_storeproduct.mytrip_storevotes.Contains(this))
                {
                    mytrip_storeproduct.mytrip_storevotes.Add(this);
                }
                if (ProductId != mytrip_storeproduct.ProductId)
                {
                    ProductId = mytrip_storeproduct.ProductId;
                }
            }
        }

        #endregion
    }
}

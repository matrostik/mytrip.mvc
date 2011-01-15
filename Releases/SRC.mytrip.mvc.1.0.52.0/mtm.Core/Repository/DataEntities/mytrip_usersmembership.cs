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

namespace mtm.Core.Repository.DataEntities
{
    public partial class mytrip_usersmembership
    {
        #region Primitive Properties
    
        public virtual string UserId
        {
            get { return _userId; }
            set
            {
                if (_userId != value)
                {
                    if (mytrip_users != null && mytrip_users.UserId != value)
                    {
                        mytrip_users = null;
                    }
                    _userId = value;
                }
            }
        }
        private string _userId;
    
        public virtual string Password
        {
            get;
            set;
        }
    
        public virtual string PasswordSalt
        {
            get;
            set;
        }
    
        public virtual string Email
        {
            get;
            set;
        }
    
        public virtual bool IsApproved
        {
            get;
            set;
        }
    
        public virtual System.DateTime CreationDate
        {
            get;
            set;
        }
    
        public virtual System.DateTime LastLockoutDate
        {
            get;
            set;
        }
    
        public virtual System.DateTime LastLoginDate
        {
            get;
            set;
        }
    
        public virtual System.DateTime LastPasswordChangedDate
        {
            get;
            set;
        }
    
        public virtual string UserIP
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual mytrip_users mytrip_users
        {
            get { return _mytrip_users; }
            set
            {
                if (!ReferenceEquals(_mytrip_users, value))
                {
                    var previousValue = _mytrip_users;
                    _mytrip_users = value;
                    Fixupmytrip_users(previousValue);
                }
            }
        }
        private mytrip_users _mytrip_users;

        #endregion
        #region Association Fixup
    
        private void Fixupmytrip_users(mytrip_users previousValue)
        {
            if (previousValue != null && ReferenceEquals(previousValue.mytrip_usersmembership, this))
            {
                previousValue.mytrip_usersmembership = null;
            }
    
            if (mytrip_users != null)
            {
                mytrip_users.mytrip_usersmembership = this;
                if (UserId != mytrip_users.UserId)
                {
                    UserId = mytrip_users.UserId;
                }
            }
        }

        #endregion
    }
}

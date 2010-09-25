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

namespace Mytrip.Votes.Repository.DataEntities
{
    public partial class mytrip_votesquestion
    {
        #region Primitive Properties
    
        public virtual int QuestionId
        {
            get;
            set;
        }
    
        public virtual string Question
        {
            get;
            set;
        }
    
        public virtual int TotalVotes
        {
            get;
            set;
        }
    
        public virtual bool Active
        {
            get;
            set;
        }
    
        public virtual System.DateTime CreateDate
        {
            get;
            set;
        }
    
        public virtual System.DateTime CloseDate
        {
            get;
            set;
        }
    
        public virtual string UserName
        {
            get;
            set;
        }
    
        public virtual bool OnlyForRegisterUser
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
    
        public virtual string Path
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual ICollection<mytrip_votesanswer> mytrip_votesanswer
        {
            get
            {
                if (_mytrip_votesanswer == null)
                {
                    var newCollection = new FixupCollection<mytrip_votesanswer>();
                    newCollection.CollectionChanged += Fixupmytrip_votesanswer;
                    _mytrip_votesanswer = newCollection;
                }
                return _mytrip_votesanswer;
            }
            set
            {
                if (!ReferenceEquals(_mytrip_votesanswer, value))
                {
                    var previousValue = _mytrip_votesanswer as FixupCollection<mytrip_votesanswer>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= Fixupmytrip_votesanswer;
                    }
                    _mytrip_votesanswer = value;
                    var newValue = value as FixupCollection<mytrip_votesanswer>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += Fixupmytrip_votesanswer;
                    }
                }
            }
        }
        private ICollection<mytrip_votesanswer> _mytrip_votesanswer;

        #endregion
        #region Association Fixup
    
        private void Fixupmytrip_votesanswer(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (mytrip_votesanswer item in e.NewItems)
                {
                    item.mytrip_votesquestion = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (mytrip_votesanswer item in e.OldItems)
                {
                    if (ReferenceEquals(item.mytrip_votesquestion, this))
                    {
                        item.mytrip_votesquestion = null;
                    }
                }
            }
        }

        #endregion
    }
}
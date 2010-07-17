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

namespace Mytrip.Articles.Repository.DataEntities
{
    public partial class mytrip_articles
    {
        #region Primitive Properties
    
        public virtual int ArticleId
        {
            get;
            set;
        }
    
        public virtual int CategoryId
        {
            get { return _categoryId; }
            set
            {
                if (_categoryId != value)
                {
                    if (mytrip_articlescategory != null && mytrip_articlescategory.CategoryId != value)
                    {
                        mytrip_articlescategory = null;
                    }
                    _categoryId = value;
                }
            }
        }
        private int _categoryId;
    
        public virtual string Title
        {
            get;
            set;
        }
    
        public virtual string Abstract
        {
            get;
            set;
        }
    
        public virtual string Body
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
    
        public virtual bool ApprovedComment
        {
            get;
            set;
        }
    
        public virtual bool IncludeAnonymComment
        {
            get;
            set;
        }
    
        public virtual string ImageForAbstract
        {
            get;
            set;
        }
    
        public virtual string Path
        {
            get;
            set;
        }
    
        public virtual bool OnlyForRegisterUser
        {
            get;
            set;
        }
    
        public virtual bool ApprovedVotes
        {
            get;
            set;
        }
    
        public virtual System.DateTime CloseDate
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
    
        public virtual decimal TotalVotes
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual mytrip_articlescategory mytrip_articlescategory
        {
            get { return _mytrip_articlescategory; }
            set
            {
                if (!ReferenceEquals(_mytrip_articlescategory, value))
                {
                    var previousValue = _mytrip_articlescategory;
                    _mytrip_articlescategory = value;
                    Fixupmytrip_articlescategory(previousValue);
                }
            }
        }
        private mytrip_articlescategory _mytrip_articlescategory;
    
        public virtual ICollection<mytrip_articlescomments> mytrip_articlescomments
        {
            get
            {
                if (_mytrip_articlescomments == null)
                {
                    var newCollection = new FixupCollection<mytrip_articlescomments>();
                    newCollection.CollectionChanged += Fixupmytrip_articlescomments;
                    _mytrip_articlescomments = newCollection;
                }
                return _mytrip_articlescomments;
            }
            set
            {
                if (!ReferenceEquals(_mytrip_articlescomments, value))
                {
                    var previousValue = _mytrip_articlescomments as FixupCollection<mytrip_articlescomments>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= Fixupmytrip_articlescomments;
                    }
                    _mytrip_articlescomments = value;
                    var newValue = value as FixupCollection<mytrip_articlescomments>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += Fixupmytrip_articlescomments;
                    }
                }
            }
        }
        private ICollection<mytrip_articlescomments> _mytrip_articlescomments;
    
        public virtual ICollection<mytrip_articlesvotes> mytrip_articlesvotes
        {
            get
            {
                if (_mytrip_articlesvotes == null)
                {
                    var newCollection = new FixupCollection<mytrip_articlesvotes>();
                    newCollection.CollectionChanged += Fixupmytrip_articlesvotes;
                    _mytrip_articlesvotes = newCollection;
                }
                return _mytrip_articlesvotes;
            }
            set
            {
                if (!ReferenceEquals(_mytrip_articlesvotes, value))
                {
                    var previousValue = _mytrip_articlesvotes as FixupCollection<mytrip_articlesvotes>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= Fixupmytrip_articlesvotes;
                    }
                    _mytrip_articlesvotes = value;
                    var newValue = value as FixupCollection<mytrip_articlesvotes>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += Fixupmytrip_articlesvotes;
                    }
                }
            }
        }
        private ICollection<mytrip_articlesvotes> _mytrip_articlesvotes;
    
        public virtual ICollection<mytrip_articlestag> mytrip_articlestag
        {
            get
            {
                if (_mytrip_articlestag == null)
                {
                    var newCollection = new FixupCollection<mytrip_articlestag>();
                    newCollection.CollectionChanged += Fixupmytrip_articlestag;
                    _mytrip_articlestag = newCollection;
                }
                return _mytrip_articlestag;
            }
            set
            {
                if (!ReferenceEquals(_mytrip_articlestag, value))
                {
                    var previousValue = _mytrip_articlestag as FixupCollection<mytrip_articlestag>;
                    if (previousValue != null)
                    {
                        previousValue.CollectionChanged -= Fixupmytrip_articlestag;
                    }
                    _mytrip_articlestag = value;
                    var newValue = value as FixupCollection<mytrip_articlestag>;
                    if (newValue != null)
                    {
                        newValue.CollectionChanged += Fixupmytrip_articlestag;
                    }
                }
            }
        }
        private ICollection<mytrip_articlestag> _mytrip_articlestag;

        #endregion
        #region Association Fixup
    
        private void Fixupmytrip_articlescategory(mytrip_articlescategory previousValue)
        {
            if (previousValue != null && previousValue.mytrip_articles.Contains(this))
            {
                previousValue.mytrip_articles.Remove(this);
            }
    
            if (mytrip_articlescategory != null)
            {
                if (!mytrip_articlescategory.mytrip_articles.Contains(this))
                {
                    mytrip_articlescategory.mytrip_articles.Add(this);
                }
                if (CategoryId != mytrip_articlescategory.CategoryId)
                {
                    CategoryId = mytrip_articlescategory.CategoryId;
                }
            }
        }
    
        private void Fixupmytrip_articlescomments(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (mytrip_articlescomments item in e.NewItems)
                {
                    item.mytrip_articles = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (mytrip_articlescomments item in e.OldItems)
                {
                    if (ReferenceEquals(item.mytrip_articles, this))
                    {
                        item.mytrip_articles = null;
                    }
                }
            }
        }
    
        private void Fixupmytrip_articlesvotes(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (mytrip_articlesvotes item in e.NewItems)
                {
                    item.mytrip_articles = this;
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (mytrip_articlesvotes item in e.OldItems)
                {
                    if (ReferenceEquals(item.mytrip_articles, this))
                    {
                        item.mytrip_articles = null;
                    }
                }
            }
        }
    
        private void Fixupmytrip_articlestag(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (mytrip_articlestag item in e.NewItems)
                {
                    if (!item.mytrip_articles.Contains(this))
                    {
                        item.mytrip_articles.Add(this);
                    }
                }
            }
    
            if (e.OldItems != null)
            {
                foreach (mytrip_articlestag item in e.OldItems)
                {
                    if (item.mytrip_articles.Contains(this))
                    {
                        item.mytrip_articles.Remove(this);
                    }
                }
            }
        }

        #endregion
    }
}

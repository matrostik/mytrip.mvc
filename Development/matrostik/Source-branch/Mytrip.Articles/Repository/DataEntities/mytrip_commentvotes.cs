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
    public partial class mytrip_commentvotes
    {
        #region Primitive Properties
    
        public virtual int CommentId
        {
            get { return _commentId; }
            set
            {
                if (_commentId != value)
                {
                    if (mytrip_articlescomments != null && mytrip_articlescomments.CommentId != value)
                    {
                        mytrip_articlescomments = null;
                    }
                    _commentId = value;
                }
            }
        }
        private int _commentId;
    
        public virtual int Id
        {
            get;
            set;
        }
    
        public virtual string UserName
        {
            get;
            set;
        }

        #endregion
        #region Navigation Properties
    
        public virtual mytrip_articlescomments mytrip_articlescomments
        {
            get { return _mytrip_articlescomments; }
            set
            {
                if (!ReferenceEquals(_mytrip_articlescomments, value))
                {
                    var previousValue = _mytrip_articlescomments;
                    _mytrip_articlescomments = value;
                    Fixupmytrip_articlescomments(previousValue);
                }
            }
        }
        private mytrip_articlescomments _mytrip_articlescomments;

        #endregion
        #region Association Fixup
    
        private void Fixupmytrip_articlescomments(mytrip_articlescomments previousValue)
        {
            if (previousValue != null && previousValue.mytrip_commentvotes.Contains(this))
            {
                previousValue.mytrip_commentvotes.Remove(this);
            }
    
            if (mytrip_articlescomments != null)
            {
                if (!mytrip_articlescomments.mytrip_commentvotes.Contains(this))
                {
                    mytrip_articlescomments.mytrip_commentvotes.Add(this);
                }
                if (CommentId != mytrip_articlescomments.CommentId)
                {
                    CommentId = mytrip_articlescomments.CommentId;
                }
            }
        }

        #endregion
    }
}

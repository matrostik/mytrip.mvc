//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.EntityClient;

namespace Mytrip.Articles.Repository.DataEntities
{
    public partial class Entities : ObjectContext
    {
        public const string ConnectionString = "name=Entities";
        public const string ContainerName = "Entities";
    
        #region Constructors
    
        public Entities()
            : base(ConnectionString, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = false;
        }
    
        public Entities(string connectionString)
            : base(connectionString, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = false;
        }
    
        public Entities(EntityConnection connection)
            : base(connection, ContainerName)
        {
            this.ContextOptions.LazyLoadingEnabled = false;
        }
    
        #endregion
    
        #region ObjectSet Properties
    
        public ObjectSet<mytrip_articles> mytrip_articles
        {
            get { return _mytrip_articles  ?? (_mytrip_articles = CreateObjectSet<mytrip_articles>("mytrip_articles")); }
        }
        private ObjectSet<mytrip_articles> _mytrip_articles;
    
        public ObjectSet<mytrip_articlescategory> mytrip_articlescategory
        {
            get { return _mytrip_articlescategory  ?? (_mytrip_articlescategory = CreateObjectSet<mytrip_articlescategory>("mytrip_articlescategory")); }
        }
        private ObjectSet<mytrip_articlescategory> _mytrip_articlescategory;
    
        public ObjectSet<mytrip_articlescomments> mytrip_articlescomments
        {
            get { return _mytrip_articlescomments  ?? (_mytrip_articlescomments = CreateObjectSet<mytrip_articlescomments>("mytrip_articlescomments")); }
        }
        private ObjectSet<mytrip_articlescomments> _mytrip_articlescomments;
    
        public ObjectSet<mytrip_articlessubscription> mytrip_articlessubscription
        {
            get { return _mytrip_articlessubscription  ?? (_mytrip_articlessubscription = CreateObjectSet<mytrip_articlessubscription>("mytrip_articlessubscription")); }
        }
        private ObjectSet<mytrip_articlessubscription> _mytrip_articlessubscription;
    
        public ObjectSet<mytrip_articlestag> mytrip_articlestag
        {
            get { return _mytrip_articlestag  ?? (_mytrip_articlestag = CreateObjectSet<mytrip_articlestag>("mytrip_articlestag")); }
        }
        private ObjectSet<mytrip_articlestag> _mytrip_articlestag;
    
        public ObjectSet<mytrip_articlesvotes> mytrip_articlesvotes
        {
            get { return _mytrip_articlesvotes  ?? (_mytrip_articlesvotes = CreateObjectSet<mytrip_articlesvotes>("mytrip_articlesvotes")); }
        }
        private ObjectSet<mytrip_articlesvotes> _mytrip_articlesvotes;
    
        public ObjectSet<mytrip_commentvotes> mytrip_commentvotes
        {
            get { return _mytrip_commentvotes  ?? (_mytrip_commentvotes = CreateObjectSet<mytrip_commentvotes>("mytrip_commentvotes")); }
        }
        private ObjectSet<mytrip_commentvotes> _mytrip_commentvotes;

        #endregion
    }
}

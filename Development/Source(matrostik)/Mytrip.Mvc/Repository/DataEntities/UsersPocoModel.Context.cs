//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Data.Objects;
using System.Data.EntityClient;

namespace Mytrip.Mvc.Repository.DataEntities
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
    
        public ObjectSet<mytrip_users> mytrip_users
        {
            get { return _mytrip_users  ?? (_mytrip_users = CreateObjectSet<mytrip_users>("mytrip_users")); }
        }
        private ObjectSet<mytrip_users> _mytrip_users;
    
        public ObjectSet<mytrip_usersmembership> mytrip_usersmembership
        {
            get { return _mytrip_usersmembership  ?? (_mytrip_usersmembership = CreateObjectSet<mytrip_usersmembership>("mytrip_usersmembership")); }
        }
        private ObjectSet<mytrip_usersmembership> _mytrip_usersmembership;
    
        public ObjectSet<mytrip_usersroles> mytrip_usersroles
        {
            get { return _mytrip_usersroles  ?? (_mytrip_usersroles = CreateObjectSet<mytrip_usersroles>("mytrip_usersroles")); }
        }
        private ObjectSet<mytrip_usersroles> _mytrip_usersroles;

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mytrip.Core.Repository.MsSqlUsers;
using Mytrip.Core.Repository.XmlUsers;

namespace Mytrip.Core.Repository
{
  public  class ICoreRepository
    {
        MembershipRepository _membershipRepo;
        public MembershipRepository membershipRepo
        {
            get
            {
                if (_membershipRepo == null)
                    _membershipRepo = new MembershipRepository();
                return _membershipRepo;
            }
        }
        RoleRepository _roleRepo;
        public RoleRepository roleRepo
        {
            get
            {
                if (_roleRepo == null)
                    _roleRepo = new RoleRepository();
                return _roleRepo;
            }
        }
        MsSqlMembershipRepository _mssqlMembershipRepo;
        public MsSqlMembershipRepository mssqlMembershipRepo
        {
            get
            {
                if (_mssqlMembershipRepo == null)
                    _mssqlMembershipRepo = new MsSqlMembershipRepository();
                return _mssqlMembershipRepo;
            }
        }
        XmlMembershipRepository _xmlMembershipRepo;
        public XmlMembershipRepository xmlMembershipRepo
        {
            get
            {
                if (_xmlMembershipRepo == null)
                    _xmlMembershipRepo = new XmlMembershipRepository();
                return _xmlMembershipRepo;
            }
        }
    }
}

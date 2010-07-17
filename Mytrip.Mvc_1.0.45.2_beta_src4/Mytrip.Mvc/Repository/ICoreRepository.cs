using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mytrip.Mvc.Repository
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
        EmailRepository _emailRepo;
        public EmailRepository emailRepo
        {
            get
            {
                if (_emailRepo == null)
                    _emailRepo = new EmailRepository();
                return _emailRepo;
            }
        }
        AboutRepository _aboutRepo;
        public AboutRepository aboutRepo
        {
            get
            {
                if (_aboutRepo == null)
                    _aboutRepo = new AboutRepository();
                return _aboutRepo;
            }
        }
    }
}

/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about mytrip.mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
namespace mtm.Core.Repository
{
    /// <summary>Инициализация Repository
    /// </summary>
    internal class ICoreRepository
    {
        MembershipRepository _membershipRepo;

        /// <summary> Инициализация MembershipRepository
        /// </summary>
        internal MembershipRepository membershipRepo
        {
            get
            {
                if (_membershipRepo == null)
                    _membershipRepo = new MembershipRepository();
                return _membershipRepo;
            }
        }

        EmailRepository _emailRepo;

        /// <summary>Инициализация EmailRepository
        /// </summary>
        internal EmailRepository emailRepo
        {
            get
            {
                if (_emailRepo == null)
                    _emailRepo = new EmailRepository();
                return _emailRepo;
            }
        }

        AccountMembershipRepository _membershipService;

        /// <summary>Инициализация AccountMembershipRepository
        /// </summary>
        internal AccountMembershipRepository membershipService
        {
            get
            {
                if (_membershipService == null)
                    _membershipService = new AccountMembershipRepository();
                return _membershipService;
            }
        }

        FormsAuthenticationRepository _formsService;

        /// <summary>Инициализация FormsAuthenticationRepository
        /// </summary>
        internal FormsAuthenticationRepository formsService
        {
            get
            {
                if (_formsService == null)
                    _formsService = new FormsAuthenticationRepository();
                return _formsService;
            }
        }

        RoleRepository _roleRepo;

        /// <summary>Инициализация RoleRepository
        /// </summary>
        internal RoleRepository roleRepo
        {
            get
            {
                if (_roleRepo == null)
                    _roleRepo = new RoleRepository();
                return _roleRepo;
            }
        }

        FileRepository _fileRepo;

        /// <summary>Инициализация FileRepository
        /// </summary>
        internal FileRepository fileRepo
        {
            get
            {
                if (_fileRepo == null)
                    _fileRepo = new FileRepository();
                return _fileRepo;
            }
        }

        InstallModuleRepository _installModuleRepo;

        /// <summary>Инициализация InstallModuleRepository
        /// </summary>
        internal InstallModuleRepository installModuleRepo
        {
            get
            {
                if (_installModuleRepo == null)
                    _installModuleRepo = new InstallModuleRepository();
                return _installModuleRepo;
            }
        }
        CorePageRepository _corePageRepo;

        /// <summary>Инициализация CorePageRepository
        /// </summary>
        internal CorePageRepository corePageRepo
        {
            get
            {
                if (_corePageRepo == null)
                    _corePageRepo = new CorePageRepository();
                return _corePageRepo;
            }
        }
        StatisticRepository _StatisticRepository;
        public StatisticRepository statisticRepo
        {
            get
            {
                if (_StatisticRepository == null)
                    _StatisticRepository = new StatisticRepository();
                return _StatisticRepository;
            }
        }
        GeoRepository _GeoRepository;
        public GeoRepository geoRepo
        {
            get
            {
                if (_GeoRepository == null)
                    _GeoRepository = new GeoRepository();
                return _GeoRepository;
            }
        }
    }
}

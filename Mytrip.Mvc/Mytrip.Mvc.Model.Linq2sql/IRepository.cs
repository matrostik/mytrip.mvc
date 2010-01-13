/*   Mytrip.Mvc.Model.Linq2sql   Copyright ©  2009 Стюхин Олег Анатольевич   */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mytrip.Mvc.Model.Linq2sql.SiteModel;
using Mytrip.Mvc.Model.Linq2sql.Artycles;
using Mytrip.Mvc.Model.Linq2sql.Search;
using Mytrip.Mvc.Model.Linq2sql.Files;
using Mytrip.Mvc.Model.Linq2sql.Mail;

namespace Mytrip.Mvc.Model.Linq2sql
{
    public class IRepository
    {
        /*  РАЗДЕЛ 1 SQL */

        #region SqlRepositoryDataContext
        private SqlRepositoryDataContext _db;
        public IRepository(string connectionString)
        {
            _db = new SqlRepositoryDataContext(connectionString);
        }
        #endregion

        /*  РАЗДЕЛ 2 MODEL */

        #region SiteModelRepository
        private SiteModelRepository _SiteModelRepository;
        public SiteModelRepository dm_model
        {
            get
            {
                if (_SiteModelRepository == null)
                    _SiteModelRepository = new SiteModelRepository(_db);
                return _SiteModelRepository;
            }
        }
        #endregion

        /*  РАЗДЕЛ 3 ARTYCLES */

        #region CategoryRepository
        private CategoryRepository _categoryRepository;
        public CategoryRepository dm_artycle_category
        {
            get
            {
                if (_categoryRepository == null)
                    _categoryRepository = new CategoryRepository(_db);
                return _categoryRepository;
            }
        }
        #endregion

        #region ArtycleRepository
        private ArtycleRepository _artycleRepository;
        public ArtycleRepository dm_artycle
        {
            get
            {
                if (_artycleRepository == null)
                    _artycleRepository = new ArtycleRepository(_db);
                return _artycleRepository;
            }
        }
        #endregion

        #region CommentRepository
        private CommentRepository _commentRepository;
        public CommentRepository dm_artycle_comment
        {
            get
            {
                if (_commentRepository == null)
                    _commentRepository = new CommentRepository(_db);
                return _commentRepository;
            }
        }
        #endregion

        /*  РАЗДЕЛ 4 SEARCH */

        #region SearchRepository
        private SearchRepository _SearchRepository;
        public SearchRepository dm_search
        {
            get
            {
                if (_SearchRepository == null)
                    _SearchRepository = new SearchRepository(_db);
                return _SearchRepository;
            }
        }
        #endregion

        /*  РАЗДЕЛ 5 Files */
        private FileRepository _FileRepository;
        public FileRepository dm_file
        {
            get
            {
                if (_FileRepository == null)
                    _FileRepository = new FileRepository();
                return _FileRepository;
            }
        }

        /*  РАЗДЕЛ 5 Mail */
        private MailRepository _MailRepository;
        public MailRepository dm_mail
        {
            get
            {
                if (_MailRepository == null)
                    _MailRepository = new MailRepository(_db);
                return _MailRepository;
            }
        }
    }
}

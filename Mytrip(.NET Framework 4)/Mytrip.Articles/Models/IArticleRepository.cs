using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mytrip.Articles.Models
{
    public class IArticleRepository
    {
        private ArticleRepository _articleRepository;
        public ArticleRepository article
        {
            get
            {
                if (_articleRepository == null)
                    _articleRepository = new ArticleRepository();
                return _articleRepository;
            }
        }
        private CategoryRepository _categoryRepository;
        public CategoryRepository category
        {
            get
            {
                if (_categoryRepository == null)
                    _categoryRepository = new CategoryRepository();
                return _categoryRepository;
            }
        }
        private CommentRepository _commentRepository;
        public CommentRepository comment
        {
            get
            {
                if (_commentRepository == null)
                    _commentRepository = new CommentRepository();
                return _commentRepository;
            }
        }
    }
}

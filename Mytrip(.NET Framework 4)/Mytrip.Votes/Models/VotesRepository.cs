using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Mytrip.Votes.Models
{
    public class VotesRepository
    {
        #region Подключение к Entity Репозиторию
        votesEntities _entities;
        public votesEntities entities
        {
            get
            {
                if (_entities == null)
                    _entities = new votesEntities(VotesSetting.connectionString);
                return _entities;
            }
        }
        #endregion
        public IQueryable<mytrip_VotesQuestion> GetQuestionsForHomePage(string culture, int take)
        {
            var a= entities.mytrip_VotesQuestion.Where(x=>x.Active==true).Where(x => x.Culture == culture);
            var b = entities.mytrip_VotesQuestion.Where(x=>x.Active==true).Where(x => x.AllCulture == true);
            return a.Union(b).OrderByDescending(x => x.CreateDate).Take(take);
        }
        public mytrip_VotesQuestion GetQuestion(int id)
        {
            return entities.mytrip_VotesQuestion.FirstOrDefault(x => x.QuestionId == id);
        }
      /*  public void CreateQuestion(string question,bool active,bool allCulture,string culture)
        {
            mytrip_VotesQuestion x = new mytrip_VotesQuestion
            {
                QuestionId = CreateQuestionId(),
                Question=question,
                UserName=HttpContext.Current.User.Identity.Name,
                Active=active,
                AllCulture=allCulture,
                Culture=culture,
                TotalVotes=0,
                Path=
            };
        
        }
        /*Private*/
        private int CreateQuestionId()
        {
            int catId;
            for (catId = 1; GetQuestion(catId) != null; catId++) ;
            return catId;
        }
    }
}

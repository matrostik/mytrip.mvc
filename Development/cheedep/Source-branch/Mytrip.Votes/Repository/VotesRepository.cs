using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Mytrip.Mvc;
using Mytrip.Votes.Repository.DataEntities;
using Mytrip.Mvc.Settings;

namespace Mytrip.Votes.Repository
{
    public class VotesRepository
    {
        #region Подключение к Entity Репозиторию
        Entities _entities;
        public Entities entities
        {
            get
            {
                if (_entities == null)
                    _entities = new Entities(ModuleSetting.connectionString());
                return _entities;
            }
        }
        #endregion

        #region Get Questions and Answers

        public IQueryable<mytrip_votesquestion> GetQuestionsForHomePage(string culture, int take)
        {
            var a = entities.mytrip_votesquestion.Where(x => x.Active == true).Where(x => x.Culture == culture);
            var b = entities.mytrip_votesquestion.Where(x => x.Active == true).Where(x => x.AllCulture == true);
            return a.Union(b).OrderByDescending(x => x.CreateDate).Take(take);
        }
        /// <summary>
        /// Get question by QuestionId
        /// </summary>
        /// <param name="id">QuestionId</param>
        /// <returns>mytrip_votesquestion</returns>
        public mytrip_votesquestion GetQuestion(int id)
        {
            return entities.mytrip_votesquestion.Include("mytrip_votesanswer").FirstOrDefault(x => x.QuestionId == id);
        }
        /// <summary>
        /// Get Active Question (Vote)
        /// </summary>
        /// <returns>mytrip_votesquestion</returns>
        public mytrip_votesquestion GetActiveQuestion(string culture)
        {
            var q = entities.mytrip_votesquestion.Include("mytrip_votesanswer")
                .FirstOrDefault(x => x.Active == true && (x.AllCulture == true || x.Culture == culture));
            return q;
        }
        /// <summary>
        /// Get All Questions
        /// </summary>
        /// <returns>mytrip_votesquestion</returns>
        public IQueryable<mytrip_votesquestion> GetAllQuestions()
        {
            return entities.mytrip_votesquestion.Include("mytrip_votesanswer").OrderByDescending(z => z.CreateDate);
        }
        /// <summary>
        /// Get answer by AnswerId
        /// </summary>
        /// <param name="id">AnswerId</param>
        /// <returns>mytrip_votesanswer</returns>
        public mytrip_votesanswer GetAnswer(int id)
        {
            return entities.mytrip_votesanswer.FirstOrDefault(x => x.AnswerId == id);
        }
        /// <summary>
        /// Get answers by QuestionId
        /// </summary>
        /// <param name="questionId">QuestionId</param>
        /// <returns>mytrip_votesanswer</returns>
        public IQueryable<mytrip_votesanswer> GetAnswers(int questionId)
        {
            return entities.mytrip_votesanswer.Where(a => a.QuestionId == questionId).OrderBy(x => x.AnswerId);
        }
        #endregion

        #region Create Edit Delete Update
        /// <summary>
        /// Create new question
        /// </summary>
        /// <param name="question">question</param>
        /// <param name="active">isActive</param>
        /// <param name="forRegister">for registered users</param>
        /// <param name="allCulture">far all languages</param>
        /// <param name="culture">culture</param>
        /// <param name="closeDate">closedate</param>
        /// <returns>int QuestionId</returns>
        public int CreateQuestion(string question, bool active, bool forRegister, bool allCulture, string culture, DateTime closeDate)
        {
            mytrip_votesquestion x = new mytrip_votesquestion
            {
                QuestionId = CreateQuestionId(),
                Question = question,
                UserName = HttpContext.Current.User.Identity.Name,
                Active = active,
                OnlyForRegisterUser = forRegister,
                AllCulture = allCulture,
                Culture = culture,
                TotalVotes = 0,
                Path = GeneralMethods.DecodingString(question),
                CreateDate = DateTime.Now,
                CloseDate = closeDate
            };
            entities.mytrip_votesquestion.AddObject(x);
            if (active)
            {
                var aq = entities.mytrip_votesquestion.FirstOrDefault(q => q.Active == true);
                if (aq != null)
                    aq.Active = false;
            }
            entities.SaveChanges();
            return x.QuestionId;
        }
        /// <summary>
        /// Create answer
        /// </summary>
        /// <param name="questionId">questionId</param>
        /// <param name="answer">answer</param>
        public void CreateAnswer(int questionId, string answer)
        {
            mytrip_votesanswer a = new mytrip_votesanswer()
            {
                AnswerId = CreateAnswerId(),
                QuestionId = questionId,
                Answer = answer,
                TotalVotes = 0
            };
            entities.mytrip_votesanswer.AddObject(a);
            entities.SaveChanges();
        }
        /// <summary>
        /// Edit question 
        /// </summary>
        /// <param name="questionId">questionId</param>
        /// <param name="question">question</param>
        /// <param name="active">active</param>
        /// <param name="forRegister">forRegister</param>
        /// <param name="allCulture">allCulture</param>
        /// <param name="culture">culture</param>
        /// <param name="closeDate">closeDate</param>
        /// <param name="answers">answers</param>
        public void EditQuestion(int questionId, string question, bool active, bool forRegister, bool allCulture, DateTime closeDate, string[] answers)
        {
            var q = GetQuestion(questionId);
            if (q.Active != active)
                OnOffQuestion(questionId);
            q.Question = question;
            q.Active = active;
            q.OnlyForRegisterUser = forRegister;
            q.AllCulture = allCulture;
            q.Path = GeneralMethods.DecodingString(question);
            q.CloseDate = closeDate;
            q.TotalVotes -= EditAnswers(questionId, answers);
            entities.SaveChanges();
        }
        int EditAnswers(int questionId, string[] answers)
        {
            var ans = entities.mytrip_votesanswer.Where(x => x.QuestionId == questionId).OrderBy(x => x.AnswerId);
            int votes = 0;
            if (answers.Length == ans.Count())
            {
                for (int i = 0; i < answers.Length; i++)
                {
                    ans.ToList()[i].Answer = answers[i];
                }
            }
            else if (answers.Length > ans.Count())
            {
                for (int i = 0; i < ans.Count(); i++)
                {
                    ans.ToList()[i].Answer = answers[i];
                }
                for (int i = ans.Count(); i < answers.Length; i++)
                {
                    CreateAnswer(questionId, answers[i]);
                }
            }
            else if (answers.Length < ans.Count())
            {
                for (int i = 0; i < answers.Length; i++)
                {
                    ans.ToList()[i].Answer = answers[i];
                }
                for (int i = answers.Length; i < ans.Count(); i++)
                {
                    int id = ans.ToList()[i].AnswerId;
                    var a = entities.mytrip_votesanswer.FirstOrDefault(x => x.AnswerId == id);
                    votes += a.TotalVotes;
                    entities.mytrip_votesanswer.DeleteObject(a);
                }
            }
            entities.SaveChanges();
            return votes;
        }
        /// <summary>
        /// Delete question (Vote)
        /// </summary>
        /// <param name="id">questionId</param>
        public void DeleteQuestion(int questionId)
        {
            var question = entities.mytrip_votesquestion.Include("mytrip_votesanswer").FirstOrDefault(x => x.QuestionId == questionId);
            foreach (var answer in question.mytrip_votesanswer.ToList())
            {
                entities.mytrip_votesanswer.DeleteObject(answer);
            }
            entities.mytrip_votesquestion.DeleteObject(question);
            entities.SaveChanges();
        }
        /// <summary>
        /// Delete answers by questionId
        /// </summary>
        /// <param name="questionId">questionId</param>
        public void DeleteAnswers(int questionId)
        {
            var answers = entities.mytrip_votesanswer.Where(x => x.QuestionId == questionId);
            foreach (var a in answers)
            {
                entities.mytrip_votesanswer.DeleteObject(a);
            }
            entities.SaveChanges();
        }
        /// <summary>
        /// Set question active or unactive
        /// </summary>
        /// <param name="questionId">questionId</param>
        public void OnOffQuestion(int questionId)
        {
            var question = entities.mytrip_votesquestion.FirstOrDefault(x => x.QuestionId == questionId);
            if (!question.Active)
            {
                question.Active = true;
                if (question.AllCulture)
                {
                    var aqs = entities.mytrip_votesquestion.Where(x => x.Active == true);
                    foreach (var a in aqs)
                    {
                        a.Active = false;
                    }
                }
                else
                {
                    var asd = entities.mytrip_votesquestion.Where(x => x.Active == true && (x.Culture == question.Culture || x.AllCulture == true));
                    foreach (var a in asd)
                    {
                        a.Active = false;
                    }
                }
            }
            else
                question.Active = false;
            entities.SaveChanges();
        }
        /// <summary>
        /// Add new user vote
        /// </summary>
        /// <param name="answerId">answerId</param>
        /// <returns>mytrip_votesquestion</returns>
        public mytrip_votesquestion AddVote(int answerId)
        {
            var a = entities.mytrip_votesanswer.FirstOrDefault(x => x.AnswerId == answerId);
            var q = entities.mytrip_votesquestion.Include("mytrip_votesanswer").FirstOrDefault(x => x.QuestionId == a.QuestionId);
            q.TotalVotes++;
            a.TotalVotes++;
            entities.SaveChanges();
            return q;
        }
        #endregion

        #region /*Private*/
        private int CreateQuestionId()
        {
            int id;
            for (id = 1; entities.mytrip_votesquestion.Count(x => x.QuestionId == id) != 0; id++) ;
            return id;
        }
        private int CreateAnswerId()
        {
            int id;
            for (id = 1; entities.mytrip_votesanswer.Count(x => x.AnswerId == id) != 0; id++) ;
            return id;
        }
        #endregion
    }
}

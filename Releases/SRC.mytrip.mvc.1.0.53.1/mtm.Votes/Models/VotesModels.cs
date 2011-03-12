using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using mtm.Votes.Repository.DataEntities;

namespace mtm.Votes.Models
{
    public class VotesModel
    {
        
        public int QuestionId { get; set; }
        [Required(ErrorMessageResourceType = typeof(VotesLanguage), ErrorMessageResourceName = "question_lenght")]
        [RegularExpression("^(.){3,255}$", ErrorMessageResourceType = typeof(VotesLanguage), ErrorMessageResourceName = "question_lenght")]
        public string Question { get; set; }
        [Required(ErrorMessageResourceType = typeof(VotesLanguage), ErrorMessageResourceName = "answer_can_not_be_empty")]
        public string Answers1 { get; set; }
        [Required(ErrorMessageResourceType = typeof(VotesLanguage), ErrorMessageResourceName = "answer_can_not_be_empty")]
        public string Answers2 { get; set; }
        [Required(ErrorMessageResourceType = typeof(VotesLanguage), ErrorMessageResourceName = "answer_can_not_be_empty")]
        public string Answers3 { get; set; }
        [Required(ErrorMessageResourceType = typeof(VotesLanguage), ErrorMessageResourceName = "answer_can_not_be_empty")]
        public string Answers4 { get; set; }
        [Required(ErrorMessageResourceType = typeof(VotesLanguage), ErrorMessageResourceName = "answer_can_not_be_empty")]
        public string Answers5 { get; set; }
        [Required(ErrorMessageResourceType = typeof(VotesLanguage), ErrorMessageResourceName = "answer_can_not_be_empty")]
        public string Answers6 { get; set; }
        [Required(ErrorMessageResourceType = typeof(VotesLanguage), ErrorMessageResourceName = "answer_can_not_be_empty")]
        public string Answers7 { get; set; }
        [Required(ErrorMessageResourceType = typeof(VotesLanguage), ErrorMessageResourceName = "answer_can_not_be_empty")]
        public string Answers8 { get; set; }
        [Required(ErrorMessageResourceType = typeof(VotesLanguage), ErrorMessageResourceName = "answer_can_not_be_empty")]
        public string Answers9 { get; set; }
        [Required(ErrorMessageResourceType = typeof(VotesLanguage), ErrorMessageResourceName = "answer_can_not_be_empty")]
        public string Answers10 { get; set; }
        //public string[] Answers { get; set; }
        public SelectList QtAnswers { get; set; }
        public string Count { get; set; }
        public string Path { get; set; }
        public bool AllCulture { get; set; }
        [RegularExpression(@"^\s*(\d{2,4})\-(\d{1,2})\-(\d{1,2})\s*$", ErrorMessage = "invalid_date")]
        [Required( ErrorMessage = "close_date_empty")]
        public string CloseDate { get; set; }
        public bool OnlyForRegisterUser { get; set; }
        public bool Active { get; set; }
        public string Theme { get; set; }
        public string[] bread { get; set; }
    }
    public class VotesSettingModel
    {
        public bool unlockVotes { get; set; }
        [Required(ErrorMessageResourceType = typeof(VotesLanguage), ErrorMessageResourceName = "nullvalue")]
        public string nameVotes_ru_ru { get; set; }
        [Required(ErrorMessageResourceType = typeof(VotesLanguage), ErrorMessageResourceName = "nullvalue")]
        public string nameVotes_en_us { get; set; }
        public string view_ru_ru { get; set; }
        public string view_en_us { get; set; }
        public string[] bread { get; set; }
        [Required(ErrorMessageResourceType = typeof(VotesLanguage), ErrorMessageResourceName = "nullvalue")]
        public string partialAccordion { get; set; }
        [Required(ErrorMessageResourceType = typeof(VotesLanguage), ErrorMessageResourceName = "nullvalue")]
        public string partialNoAccordion { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Mytrip.Votes.Repository.DataEntities;

namespace Mytrip.Votes.Models
{
    public class VotesModel
    {
        public int QuestionId { get; set; }
        public string Question { get; set; }
        public string[] Answers { get; set; }
        public SelectList QtAnswers { get; set; }
        public int Count { get; set; }
        public string Path { get; set; }
        public bool AllCulture { get; set; }
        [RegularExpression(@"^\s*(\d{2,4})\-(\d{1,2})\-(\d{1,2})\s*$", ErrorMessage = "invalid_date")]
        [Required( ErrorMessage = "close_date_empty")]
        public string CloseDate { get; set; }
        public bool OnlyForRegisterUser { get; set; }
        public bool Active { get; set; }
        public string Theme { get; set; }
    }
    public class VotesSettingModel
    {
        public bool unlockVotes { get; set; }
        [Required(ErrorMessageResourceType = typeof(VotesLanguage), ErrorMessageResourceName = "nullvalue")]
        public string nameVotes { get; set; }
    }
}

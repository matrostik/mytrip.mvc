using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using mtm.Votes.Repository;
using mtm.Votes.Repository.DataEntities;
using mtm.Core;
using mtm.Core.Helpers;
using mtm.Core.Settings;

namespace mtm.Votes.Helpers
{
    public static class VotesHelpers
    {
        #region Votes Manager
        public static HtmlString VotesList(this HtmlHelper html)
        {
            VotesRepository vr = new VotesRepository();
            TagBuilder table = new TagBuilder("table");
            table.AddCssClass("noborders");
            #region table
            int ctr = 1;
            TagBuilder tr = null;
            var questions = vr.GetAllQuestions();
            int count = questions.Count();
            foreach (var q in questions)
            {
                if (ctr % 2 == 1)
                {
                    tr = new TagBuilder("tr");
                    TagBuilder td1 = new TagBuilder("td");
                    td1.MergeAttribute("style", "width: 50%;");
                    td1.InnerHtml = SetView(q);
                    tr.InnerHtml = td1.ToString();
                }
                else
                {
                    TagBuilder td1 = new TagBuilder("td");
                    td1.InnerHtml = SetView(q);
                    tr.InnerHtml += td1.ToString();
                    table.InnerHtml += tr.ToString();
                }
                if (count == ctr && ctr % 2 == 1)
                {
                    TagBuilder td1 = new TagBuilder("td");
                    tr.InnerHtml += td1.ToString();
                    table.InnerHtml += tr.ToString();
                }
                ctr++;
            }

            #endregion
            return new HtmlString(table.ToString());
        }
        static string SetView(mytrip_votesquestion question)
        {
            TagBuilder div_accordion = new TagBuilder("div");

            TagBuilder div_accordiontitle = new TagBuilder("div");

            div_accordiontitle.InnerHtml = onOff(question.QuestionId, question.Active) + " " + EditDelete(question.QuestionId) + "<b>"
                + question.Question + "</b> " + GeneralMethods.Globe(question.AllCulture, VotesLanguage.all_languages) + " " + GeneralMethods.Flag(question.Culture)
                + " " + GeneralMethods.Keys(question.OnlyForRegisterUser, VotesLanguage.only_for_registered_users, 17);
            TagBuilder div_accordioncontent = new TagBuilder("div");

            div_accordioncontent.MergeAttribute("style", "padding:5px;");
            div_accordioncontent.InnerHtml = BuildResult(question);
            div_accordion.InnerHtml = div_accordiontitle.ToString() + div_accordioncontent.ToString();
            return div_accordion.ToString();
        }
        #endregion

        #region MenuHelpers
        /// <summary>
        /// Check if vote need to be displayed
        /// </summary>
        /// <returns></returns>
        public static string isVotesEnable()
        {
            VotesRepository vr = new VotesRepository();
            string culture = LocalisationSetting.culture();
            var q = vr.GetActiveQuestion(culture);
            if (q == null)
                return "display:none;";
            else if (q.OnlyForRegisterUser && !HttpContext.Current.User.Identity.IsAuthenticated)
                return "display:none;";
            else if (!q.AllCulture && q.Culture != culture)
                return "display:none;";
            return "display:show;";
        }
        /// <summary>
        /// Display Vote in menu
        /// </summary>
        /// <returns>string</returns>
        public static string _AccordionVote()
        {
            VotesRepository vr = new VotesRepository();
            var q = vr.GetActiveQuestion(LocalisationSetting.culture());
            if (q == null)
                return "";
            TagBuilder div = new TagBuilder("div");
            div.MergeAttribute("id", "message");
            div.InnerHtml = q.Question + "<br/>";
            bool flag = true;
            foreach (var a in q.mytrip_votesanswer)
            {
                TagBuilder radio = new TagBuilder("input");
                radio.MergeAttribute("id", a.AnswerId.ToString());
                if (flag)
                {
                    radio.MergeAttribute("checked", "checked");
                    flag = false;
                }
                radio.MergeAttribute("name", "ChosenAnswer");
                radio.MergeAttribute("type", "radio");
                radio.MergeAttribute("value", a.AnswerId.ToString());
                TagBuilder label = new TagBuilder("label");
                label.MergeAttribute("for", a.AnswerId.ToString());
                label.InnerHtml = a.Answer;
                div.InnerHtml += radio.ToString() + label.ToString() + "<br/>";
            }
            TagBuilder hidden = new TagBuilder("input");
            hidden.MergeAttribute("type", "hidden");
            hidden.MergeAttribute("value", q.QuestionId.ToString());
            div.InnerHtml += "<div class=\"button\">"
                + GeneralMethods.Button(VotesLanguage.vote, true, "Vote1", "VoteButton", "left") +
                GeneralMethods.Button(VotesLanguage.results, false, "Vote2", "VoteButton", "left")
                + "</div>";
            return div.ToString();
        }
        /// <summary>
        /// Get results of voting
        /// </summary>
        /// <param name="question">question</param>
        /// <returns>string</returns>
        public static string BuildResult(mytrip_votesquestion question)
        {
            TagBuilder div = new TagBuilder("div");
            div.InnerHtml = "" + question.Question + "<br/><p>" + VotesLanguage.total_responses + " "
                    + question.TotalVotes + "</p>";
            float percent = float.Parse(question.TotalVotes.ToString()) / 100;
            TagBuilder ol = new TagBuilder("ul");
            ol.AddCssClass("styled");
            foreach (var a in question.mytrip_votesanswer.OrderBy(x => x.AnswerId))
            {
                int percents = (int)Math.Round(a.TotalVotes / percent);
                TagBuilder li = new TagBuilder("li");
                li.InnerHtml = a.Answer;
                if (question.TotalVotes != 0)
                {
                    TagBuilder res = new TagBuilder("img");
                    res.MergeAttribute("height", "10px");
                    res.MergeAttribute("width", percents.ToString() + "px ");
                    res.MergeAttribute("src", "/Theme/" + ThemeSetting.theme() + "/images/poll.png");
                    li.InnerHtml += " - " + a.TotalVotes + " (" + percents + "%)<br/>" + res;
                }
                ol.InnerHtml += li;
            }
            div.InnerHtml += ol;
            return div.ToString();
        }
        #endregion

        #region Icons
        static string EditDelete(int questionId)
        {
            return GeneralMethods.ImgInput("/images/edite.png", "/Votes/Edit/" + questionId, "rename", VotesLanguage.edit, 14) +
                       " " + GeneralMethods.ImgInput("/images/delete.png", "/Votes/Delete/" + questionId, "delete", VotesLanguage.delete, 14) + " ";
























        }











        static string onOff(int questionId, bool isActive)
        {
            //if (isActive)
            //   return GeneralMethods.ImageLink("", "Votes/OnOff/" + questionId , VotesLanguage.off_vote, "", "/images/approved.png", "off", 14);
            //else
            //    return GeneralMethods.ImageLink("", "Votes/OnOff/" + questionId , VotesLanguage.on_vote, "", "/images/noapproved.png", "on", 14);
            if (isActive)
                return GeneralMethods.ImgInput("/images/approved.png", "Votes/OnOff/" + questionId, "rename", VotesLanguage.off_vote, 14);




            else
                return GeneralMethods.ImgInput("/images/noapproved.png", "Votes/OnOff/" + questionId, "rename", VotesLanguage.on_vote, 14);








        }
        #endregion

    }
}

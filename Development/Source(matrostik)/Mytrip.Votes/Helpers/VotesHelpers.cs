﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using Mytrip.Votes.Repository;
using Mytrip.Votes.Repository.DataEntities;
using Mytrip.Mvc;
using Mytrip.Mvc.Helpers;
using Mytrip.Mvc.Settings;

namespace Mytrip.Votes.Helpers
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
                    td1.InnerHtml = SetAccordion(q);
                    tr.InnerHtml = td1.ToString();
                }
                else
                {
                    TagBuilder td1 = new TagBuilder("td");
                    td1.InnerHtml = SetAccordion(q);
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
        static string SetAccordion(mytrip_votesquestion question)
        {
            TagBuilder div_accordion = new TagBuilder("div");
            div_accordion.AddCssClass("accordion2");
            TagBuilder div_accordiontitle = new TagBuilder("div");
            div_accordiontitle.AddCssClass("accordiontitle2");
            div_accordiontitle.InnerHtml = onOff(question.QuestionId, question.Active) + " " + EditDelete(question.QuestionId) + "<b>"
                + question.Question + "</b> " + Globe(question.AllCulture) + " " + Flag(question.Culture) + " " + Keys(question.OnlyForRegisterUser);
            TagBuilder div_accordioncontent = new TagBuilder("div");
            div_accordioncontent.AddCssClass("accordioncontent2");
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
            div.InnerHtml = "<center><h4>" + q.Question + "</h4></center>";
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
            div.InnerHtml = "<center><h4>" + question.Question + "</h4></center>" + VotesLanguage.total_responses + " "
                    + question.TotalVotes;
            float percent = float.Parse(question.TotalVotes.ToString()) / 100;
            TagBuilder ol = new TagBuilder("ol");
            ol.AddCssClass("numbered");
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
            string linkEdit = "/Votes/Edit/" + questionId;
            string linkDelete = "/Votes/Delete/" + questionId;
            string a = GeneralMethods.ImgInput("/images/edite.png", linkEdit, "rename", 14) +
                       " " + GeneralMethods.ImgInput("/images/delete.png", linkDelete, "delete", 14);
            return a + " ";
        }
        static string Flag(string culture)
        {
            culture = culture.ToLower();
            TagBuilder img = new TagBuilder("img");
            img.MergeAttribute("src", "/Theme/" + ThemeSetting.theme() + "/images/" + culture + ".png");
            img.MergeAttribute("style", "border-width:0px;width:15px");
            img.MergeAttribute("alt", culture);
            img.MergeAttribute("title", culture);
            return img.ToString();
        }
        static string Globe(bool show)
        {
            if (!show)
                return string.Empty;
            TagBuilder img = new TagBuilder("img");
            img.MergeAttribute("src", "/Theme/" + ThemeSetting.theme() + "/images/globe.png");
            img.MergeAttribute("style", "border-width:0px;width:15px");
            img.MergeAttribute("alt", "all languages");
            img.MergeAttribute("title", VotesLanguage.all_languages);
            return img.ToString();
        }
        static string Keys(bool show)
        {
            if (!show)
                return string.Empty;
            TagBuilder img = new TagBuilder("img");
            img.MergeAttribute("src", "/Theme/" + ThemeSetting.theme() + "/images/Keys.png");
            img.MergeAttribute("style", "border-width:0px;width:15px");
            img.MergeAttribute("alt", "for registered users");
            img.MergeAttribute("title", VotesLanguage.only_for_registered_users);
            return img.ToString();
        }
        static string onOff(int questionId, bool isActive)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder img = new TagBuilder("img");
            img.MergeAttribute("style", "width:14px;border:0px;");
            if (isActive)
            {
                img.MergeAttribute("src", "/Theme/" + ThemeSetting.theme() + "/images/approved.png");
                img.MergeAttribute("title", VotesLanguage.off_vote);
                img.MergeAttribute("alt", "off");
            }
            else
            {
                img.MergeAttribute("src", "/Theme/" + ThemeSetting.theme() + "/images/noapproved.png");
                img.MergeAttribute("title", VotesLanguage.on_vote);
                img.MergeAttribute("alt", "on");
            }
            TagBuilder a = new TagBuilder("a");
            a.MergeAttribute("href", "/Votes/OnOff/" + questionId + "/");
            a.InnerHtml = img.ToString();
            return a.ToString();
        }
        #endregion

    }
}

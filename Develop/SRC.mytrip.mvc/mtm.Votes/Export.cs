using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using mtm.Votes.Helpers;
using mtm.Core;
using System.Web;
using mtm.Core.Settings;
using mtm.Votes.Repository;
using mtm.Votes.Repository.DataEntities;

namespace mtm.Votes
{
   public static class Export
   {
       /// <summary>
       /// Votes block for sidebar
       /// </summary>
       /// <returns>HtmlString</returns>
       public static HtmlString AccordionVotes()
       {
           if (ModuleSetting.unlockVotes())
           {
               VotesRepository vr = new VotesRepository();
               mytrip_votesquestion q = vr.GetActiveQuestion(LocalisationSetting.culture());
               if (q != null && (HttpContext.Current.Request.Cookies["mtVotes" + q.QuestionId] == null
                   || HttpContext.Current.Request.Cookies["mtVotes" + q.QuestionId].Value != "votes" + q.CreateDate.ToLongTimeString()))
               {
                   TagBuilder form = new TagBuilder("form");
                   form.MergeAttribute("action", "/Votes/View");
                   form.MergeAttribute("method", "post");
                   form.MergeAttribute("onclick", "Sys.Mvc.AsyncForm.handleClick(this, new Sys.UI.DomEvent(event));");
                   form.MergeAttribute("onsubmit", "Sys.Mvc.AsyncForm.handleSubmit(this, new Sys.UI.DomEvent(event), { insertionMode: Sys.Mvc.InsertionMode.replace, updateTargetId:\"message\" });");
                   form.InnerHtml = VotesHelpers._AccordionVote();
                   return new HtmlString(GeneralMethods.Accordion(ModuleSetting.NameVotesPage(), form.ToString()));
               }
               else if (q != null)
               {

                   return new HtmlString(GeneralMethods.Accordion(ModuleSetting.NameVotesPage(), VotesHelpers.BuildResult(q)));
               }
               else return null;
           }
           else
               return null;
       }
       /// <summary>
       /// Control panel link for Votes Settings
       /// </summary>
       /// <returns>HtmlString</returns>
       public static HtmlString Setting()
       {
           TagBuilder li = new TagBuilder("li");
           TagBuilder a = new TagBuilder("a");
           a.MergeAttribute("href", "/Votes/Setting");
           a.InnerHtml = VotesLanguage.votessetting;
           li.InnerHtml = a.ToString();
           HtmlString htmlresult = new HtmlString(li.ToString());
           return htmlresult;

       }
       /// <summary>
       /// Control panel link for Votes Manager
       /// </summary>
       /// <returns>HtmlString</returns>
       public static HtmlString Manager()
       {
           TagBuilder li_votes = new TagBuilder("li");
           TagBuilder a_archive = new TagBuilder("a");
           a_archive.MergeAttribute("href", "/Votes");
           a_archive.InnerHtml = VotesLanguage.votes_manager;
           li_votes.InnerHtml = a_archive.ToString();
           HtmlString htmlresult = new HtmlString(li_votes.ToString());
           return htmlresult;
       }
    }
}

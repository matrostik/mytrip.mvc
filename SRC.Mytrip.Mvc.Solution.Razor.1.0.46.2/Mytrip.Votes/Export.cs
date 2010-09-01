using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Mytrip.Votes.Helpers;
using Mytrip.Mvc;
using System.Web;
using Mytrip.Mvc.Settings;

namespace Mytrip.Votes
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
               TagBuilder form = new TagBuilder("form");
               form.MergeAttribute("action", "/Votes/View");
               form.MergeAttribute("method", "post");
               form.MergeAttribute("onclick", "Sys.Mvc.AsyncForm.handleClick(this, new Sys.UI.DomEvent(event));");
               form.MergeAttribute("onsubmit", "Sys.Mvc.AsyncForm.handleSubmit(this, new Sys.UI.DomEvent(event), { insertionMode: Sys.Mvc.InsertionMode.replace, updateTargetId:\"message\" });");
               form.InnerHtml = VotesHelpers._AccordionVote();
               return new HtmlString(GeneralMethods.Accordion(ModuleSetting.NameVotesPage(), form.ToString()));
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

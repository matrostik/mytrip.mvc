using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Mytrip.Votes.Helpers;
using Mytrip.Mvc;

namespace Mytrip.Votes
{
   public static class Export
   {
       public static string AccordionVotes()
       {
           VotesSetting artset = new VotesSetting();
           if (artset.unlockVotes())
           {
               TagBuilder form = new TagBuilder("form");
               form.MergeAttribute("action", "/Votes/View");
               form.MergeAttribute("method", "post");
               form.MergeAttribute("onclick", "Sys.Mvc.AsyncForm.handleClick(this, new Sys.UI.DomEvent(event));");
               form.MergeAttribute("onsubmit", "Sys.Mvc.AsyncForm.handleSubmit(this, new Sys.UI.DomEvent(event), { insertionMode: Sys.Mvc.InsertionMode.replace, updateTargetId:\"message\" });");
               form.InnerHtml = VotesHelpers._AccordionVote();
               return GeneralMethods.Accordion2("accvotes",artset.NameVotesPage(), form.ToString());
           }
           else
               return string.Empty;
       }
       public static string SettingVotes()
       {
           TagBuilder li = new TagBuilder("li");
           TagBuilder a = new TagBuilder("a");
           a.MergeAttribute("href", "/Votes/VotesSetting");
           a.InnerHtml = VotesLanguage.votessetting;
           li.InnerHtml = a.ToString();
           return li.ToString();

       }
       public static string ManagerVotes()
       {
           TagBuilder li_votes = new TagBuilder("li");
           TagBuilder a_archive = new TagBuilder("a");
           a_archive.MergeAttribute("href", "/Votes");
           a_archive.InnerHtml = VotesLanguage.votes_manager;
           li_votes.InnerHtml = a_archive.ToString();
           return li_votes.ToString();
       }
    }
}

//************************************************************ 
// Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich
// To learn more about mytrip.mvc.Entyty visit 
// http://starterkitmytripmvc.codeplex.com/
// mytripmvc@gmail.com
// license: Microsoft Public License (Ms-PL) 
// ***********************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mtm.Votes.Models;
using mtm.Core.Models;
using mtm.Votes;
using mtm.Votes.Repository;
using mtm.Votes.Helpers;
using mtm.Votes.Repository.DataEntities;
using mtm.Core;
using System.Xml.Linq;
using mtm.Core.Settings;

namespace mtm.Votes.Controllers
{
    public class VotesController : Controller
    {
        #region Properties
        VotesRepository _vr;
        public VotesRepository vr
        {
            get
            {
                if (_vr == null)
                    _vr = new VotesRepository();
                return _vr;
            }
        }
        public string culture
        {
            get
            { return Session["culture"].ToString(); }
            set
            { Session["culture"] = value; }
        }
        #endregion

        /// <summary>
        /// *****************************
        ///  URL: /Votes/Index/count/
        ///  *****************************
        /// </summary>
        /// <param name="id">number of questions</param>
        /// <returns></returns>
        [RoleAdminAndEditor]
        public ActionResult Index()
        {
            VotesModel model = new VotesModel();
            model.QtAnswers = new SelectList(new string[] { "2", "3", "4", "5", "6", "7", "8", "9", "10" }, "3");
            model.Answers1 = null;
            model.Answers2= null;
            model.Answers3= null;
            model.Answers4= "null";
            model.Answers5= "null";
            model.Answers6= "null";
            model.Answers7= "null";
            model.Answers8= "null";
            model.Answers9= "null";
            model.Answers10 = "null";
            model.CloseDate = DateTime.Today.AddYears(1).ToString("yyyy-MM-dd");
            model.Theme = ThemeSetting.theme();
            string[] a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>",
                             VotesLanguage.votes_manager};
            model.bread = a;
            return View(model);
        }
        // *****************************
        // URL: /Votes/Index/VotesModel/
        // ******** Add Vote  ********
        [HttpPost]
        [RoleAdminAndEditor]
        public ActionResult Index(VotesModel model)
        {
            if (ModelState.IsValid)
                {
                    int questionId = vr.CreateQuestion(model);
                    string[] Answers = 
                    { 
                        model.Answers1, 
                        model.Answers2,
                        model.Answers3, 
                        model.Answers4, 
                        model.Answers5,
                        model.Answers6, 
                        model.Answers7, 
                        model.Answers8, 
                        model.Answers9, 
                        model.Answers10 
                    };
                    foreach (var a in Answers)
                    {
                        if(a!=null&&a.Length>0&&a!="null")
                        vr.CreateAnswer(questionId, a);
                    }
                    return RedirectToAction("Index");
                }

                model.QtAnswers = new SelectList(new string[] { "2", "3", "4", "5", "6", "7", "8", "9", "10" }, model.Count);
            string[] _a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>",
                             VotesLanguage.votes_manager};
            model.bread = _a;
            return View(model);
        }
        // ****************************
        // URL: /Votes/Delete/id/
        // ******  Delete Vote  ******
        [RoleAdminAndEditor]
        public ActionResult Delete(int id)
        {
            vr.DeleteQuestion(id);
            return RedirectToAction("Index");
        }
        // **************************
        // URL: /Votes/OnOff/id/
        // ******  OnOff Vote  ******
        [RoleAdminAndEditor]
        public ActionResult OnOff(int id)
        {
            vr.OnOffQuestion(id);
            return RedirectToAction("Index");
        }
        // *****************************
        // URL: /Votes/View/chosenAnswer/voteButton/
        // ********  Vote View  ********
        [HttpPost]
        public ActionResult View(int chosenAnswer, string voteButton)
        {
            if (Request.IsAjaxRequest())
            {
                mytrip_votesquestion q = vr.GetActiveQuestion(culture);
                if (q != null && voteButton != VotesLanguage.results && (HttpContext.Request.Cookies["mtVotes" + q.QuestionId] == null
                   || HttpContext.Request.Cookies["mtVotes" + q.QuestionId].Value != "votes" + q.CreateDate.ToLongTimeString()))
                {
                    HttpCookie cookie = new HttpCookie("mtVotes" + q.QuestionId, "votes" + q.CreateDate.ToLongTimeString());
                    cookie.Expires = DateTime.Now.AddYears(1);
                    Response.Cookies.Add(cookie);
                    if (voteButton == VotesLanguage.vote)
                        q = vr.AddVote(chosenAnswer);
                }
                return Content(VotesHelpers.BuildResult(q));
            }
            return View("Votes");
        }
        // *****************************
        // URL: /Votes/Edit/QuestionId/
        // ******** Edit Vote  ********
        [RoleAdminAndEditor]
        public ActionResult Edit(int id)
        {
            var q = vr.GetQuestion(id);
            VotesModel model = new VotesModel();
            model.QuestionId = id;
            model.Question = q.Question;
            model.Answers1 = "null";
            model.Answers2 = "null";
            model.Answers3 = "null";
            model.Answers4 = "null";
            model.Answers5 = "null";
            model.Answers6 = "null";
            model.Answers7 = "null";
            model.Answers8 = "null";
            model.Answers9 = "null";
            model.Answers10 = "null";
            int _count = 1;
            foreach (var x in q.mytrip_votesanswer.OrderBy(s => s.AnswerId))
            { 
            if(_count==1)
                model.Answers1 = x.Answer;
            else if (_count == 2)
                model.Answers2 = x.Answer;
            else if (_count == 3)
                model.Answers3 = x.Answer;
            else if (_count == 4)
                model.Answers4 = x.Answer;
            else if (_count == 5)
                model.Answers5 = x.Answer;
            else if (_count == 6)
                model.Answers6 = x.Answer;
            else if (_count == 7)
                model.Answers7 = x.Answer;
            else if (_count == 8)
                model.Answers8 = x.Answer;
            else if (_count == 9)
                model.Answers9 = x.Answer;
            else if (_count == 10)
                model.Answers10 = x.Answer;
            _count++;
            }
            model.Count = (_count-1).ToString();
            model.QtAnswers = new SelectList(new string[] { "2", "3", "4", "5", "6", "7", "8", "9", "10" }, model.Count);
            model.AllCulture = q.AllCulture;
            model.OnlyForRegisterUser = q.OnlyForRegisterUser;
            model.Active = q.Active;
            model.CloseDate = q.CloseDate.ToString("yyyy-MM-dd");
            string[] a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>",
                             "<a href='/Votes'>" + VotesLanguage.votes_manager + "</a>",
                             VotesLanguage.edit_vote};
            model.bread = a;
            return View(model);
        }
        // *****************************
        // URL: /Votes/Edit/VotesModel/
        // ******** Edit Vote  ********
        [HttpPost]
        public ActionResult Edit(VotesModel model)
        { if (ModelState.IsValid)
                    {
                        string _answer =
                        model.Answers1 + "]" +
                        "[" + model.Answers2 + "]" +
                        "[" + model.Answers3 + "]" +
                        "[" + model.Answers4 + "]" +
                        "[" +  model.Answers5 + "]" +
                        "[" + model.Answers6 + "]" +
                        "[" + model.Answers7 + "]" +
                        "[" + model.Answers8 + "]" +
                        "[" + model.Answers9 + "]" +
                        "[" + model.Answers10 + "]";
                        _answer = _answer.Replace("[null]", "").Replace("]", "");
                        string[] Answers = _answer.Split('[');
                    
                        vr.EditQuestion(model, Answers);
                        return RedirectToAction("Index");
                    }
                
                model.QtAnswers = new SelectList(new string[] { "2", "3", "4", "5", "6", "7", "8", "9", "10" }, model.Count);
                string[] a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>",
                             "<a href='/Votes'>" + VotesLanguage.votes_manager + "</a>",
                             VotesLanguage.edit_vote};
                model.bread = a;
                return View(model);
        }
        // ****************************
        // URL: /Votes/Setting/
        // ******* Votes Setting  ******
        [RoleAdminAndEditor]
        public ActionResult Setting()
        {
            VotesSettingModel model = new VotesSettingModel();
            model.nameVotes_ru_ru = ModuleSetting.NameVotesPage("ru-ru");
            model.nameVotes_en_us = ModuleSetting.NameVotesPage("en-us");
            model.partialAccordion = ModuleSetting.partialAccordion();
            model.partialNoAccordion = ModuleSetting.partialNoAccordion();
            if (!LocalisationSetting.unlockAllCulture() && LocalisationSetting.defaultCulture() == "ru-ru")
                model.view_en_us = "none";
            if (!LocalisationSetting.unlockAllCulture() && LocalisationSetting.defaultCulture() == "en-us")
                model.view_ru_ru = "none";
            model.unlockVotes = ModuleSetting.unlockVotes();
            string[] a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>",
                             VotesLanguage.votessetting};
            model.bread = a;
            return View(model);
        }
        // *********************************
        // URL: /Votes/Setting/VotesSettingModel/
        // ********* Votes Setting  *********
        [RoleAdminAndEditor]
        [HttpPost]
        public ActionResult Setting(VotesSettingModel model)
        {
            if (ModelState.IsValid)
            {
                #region Сохранение данных в mtm.Config.xml
                string _absolutDirectory = GeneralMethods.MytripConfigurationDirectory();
                XDocument _doc = XDocument.Load(_absolutDirectory);
                var votes = _doc.Root.Elements("mtm.Votes").Elements("add");
                votes.FirstOrDefault(x => x.Attribute("name").Value == "unlockVotes")
                    .SetAttributeValue("value", model.unlockVotes.ToString());
                votes.FirstOrDefault(x => x.Attribute("name").Value == "partialAccordion")
                    .SetAttributeValue("value", model.partialAccordion);
                votes.FirstOrDefault(x => x.Attribute("name").Value == "partialNoAccordion")
                    .SetAttributeValue("value", model.partialNoAccordion);
                var votesname = votes.FirstOrDefault(x => x.Attribute("name").Value == "nameVotes").Elements("add");
                votesname.FirstOrDefault(x => x.Attribute("value").Value == "ru-ru")
                    .SetAttributeValue("name", model.nameVotes_ru_ru);
                votesname.FirstOrDefault(x => x.Attribute("value").Value == "en-us")
                    .SetAttributeValue("name", model.nameVotes_en_us);
                _doc.Save(_absolutDirectory);
                #endregion

                #region Очистка кеша
                GeneralMethods.MytripCacheRemove("vs_unlockvotes");
                GeneralMethods.MytripCacheRemove("vs_namevotes", "ru-ru");
                GeneralMethods.MytripCacheRemove("vs_namevotes", "en-us");
                GeneralMethods.MytripCacheRemove("vs_partialaccordion");
                GeneralMethods.MytripCacheRemove("vs_partialnoaccordion");
                #endregion

                return RedirectToAction("ControlPanel", "Core");
            } if (!LocalisationSetting.unlockAllCulture() && LocalisationSetting.defaultCulture() == "ru-ru")
                model.view_en_us = "none";
            if (!LocalisationSetting.unlockAllCulture() && LocalisationSetting.defaultCulture() == "en-us")
                model.view_ru_ru = "none";
            string[] a = { "<a href='/Core/ControlPanel'>" + CoreLanguage.control_panel + "</a>",
                             VotesLanguage.votessetting};
            model.bread = a;
            return View(model);
        }
    }
}
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
        public ActionResult Index(int id = 3)
        {
            VotesModel model = new VotesModel();
            model.Count = id;
            model.QtAnswers = new SelectList(new int[] { 2, 3, 4, 5, 6, 7, 8, 9, 10 }, id);
            model.Answers = new string[id];
            model.CloseDate = DateTime.Today.AddYears(1).ToString("yyyy-MM-dd");
            model.Theme = ThemeSetting.theme();
            return View(model);
        }
        // *****************************
        // URL: /Votes/Index/VotesModel/
        // ******** Add Vote  ********
        [HttpPost]
        [RoleAdminAndEditor]
        public ActionResult Index(VotesModel model)
        {
            if (model.Answers.Length != model.Count)
            {
                string[] newans = new string[model.Count];
                for (int i = 0; i <= model.Answers.Length; i++)
                {
                    if (newans.Length <= i || model.Answers.Length <= i)
                        break;
                    newans[i] = model.Answers[i];
                }
                model.Answers = newans;
            }
            else
            {
                for (int i = 0; i < model.Answers.Length; i++)
                {
                    if (string.IsNullOrEmpty(model.Answers[i]) || model.Answers[i].Length == 0)
                        ModelState.AddModelError("Answers_" + i + "_", VotesLanguage.answer_can_not_be_empty);
                }
                if (string.IsNullOrEmpty(model.Question) || model.Question.Length <= 3)
                    ModelState.AddModelError("Question", VotesLanguage.question_lenght);
                if (ModelState.IsValid)
                {
                    int questionId = vr.CreateQuestion(model.Question, model.Active, model.OnlyForRegisterUser, model.AllCulture, culture, DateTime.Parse(model.CloseDate));
                    foreach (var a in model.Answers)
                    {
                        vr.CreateAnswer(questionId, a);
                    }
                    return RedirectToAction("Index");
                }
            }
            model.QtAnswers = new SelectList(new int[] { 2, 3, 4, 5, 6, 7, 8, 9, 10 }, model.Count);
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
            int count = q.mytrip_votesanswer.Count;
            VotesModel model = new VotesModel();
            model.QuestionId = id;
            model.Question = q.Question;
            model.Count = count;
            model.QtAnswers = new SelectList(new int[] { 2, 3, 4, 5, 6, 7, 8, 9, 10 }, count);
            model.Answers = new string[count];
            for (int i = 0; i <= model.Answers.Length - 1; i++)
            {
                model.Answers[i] = q.mytrip_votesanswer.ToList()[i].Answer;
            }
            model.AllCulture = q.AllCulture;
            model.OnlyForRegisterUser = q.OnlyForRegisterUser;
            model.Active = q.Active;
            model.CloseDate = q.CloseDate.ToString("yyyy-MM-dd");
            return View(model);
        }
        // *****************************
        // URL: /Votes/Edit/VotesModel/
        // ******** Edit Vote  ********
        [HttpPost]
        public ActionResult Edit(VotesModel model)
        {
            try
            {
                if (model.Answers.Length != model.Count)
                {
                    string[] newans = new string[model.Count];
                    for (int i = 0; i <= model.Answers.Length; i++)
                    {
                        if (newans.Length <= i || model.Answers.Length <= i)
                            break;
                        newans[i] = model.Answers[i];
                    }
                    model.Answers = newans;
                }
                else
                {
                    for (int i = 0; i < model.Answers.Length; i++)
                    {
                        if (string.IsNullOrEmpty(model.Answers[i]) || model.Answers[i].Length == 0)
                            ModelState.AddModelError("Answers_" + i + "_", VotesLanguage.answer_can_not_be_empty);
                    }
                    if (string.IsNullOrEmpty(model.Question) || model.Question.Length <= 3)
                        ModelState.AddModelError("Question", VotesLanguage.question_lenght);
                    if (ModelState.IsValid)
                    {
                        vr.EditQuestion(model.QuestionId, model.Question, model.Active, model.OnlyForRegisterUser, model.AllCulture
                            , DateTime.Parse(model.CloseDate), model.Answers);
                        return RedirectToAction("Index");
                    }
                }
                model.QtAnswers = new SelectList(new int[] { 2, 3, 4, 5, 6, 7, 8, 9, 10 }, model.Count);
                return View(model);
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
        // ****************************
        // URL: /Votes/Setting/
        // ******* Votes Setting  ******
        [RoleAdminAndEditor]
        public ActionResult Setting()
        {
            VotesSettingModel model = new VotesSettingModel();
            model.nameVotes = ModuleSetting.NameVotesPage();
            model.unlockVotes = ModuleSetting.unlockVotes();
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
                var votesname = votes.FirstOrDefault(x => x.Attribute("name").Value == "nameVotes").Elements("add");
                votesname.FirstOrDefault(x => x.Attribute("value").Value == culture.ToLower())
                    .SetAttributeValue("name", model.nameVotes);
                _doc.Save(_absolutDirectory);
                #endregion

                #region Очистка кеша
                GeneralMethods.MytripCacheRemove("vs_unlockvotes");
                GeneralMethods.MytripCacheRemove("vs_namevotes", true);
                #endregion

                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}
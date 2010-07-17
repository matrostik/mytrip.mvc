//************************************************************ 
// Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich
// To learn more about Mytrip.Mvc.Entyty visit 
// http://starterkitmytripmvc.codeplex.com/
// mytripmvc@gmail.com
// license: Microsoft Public License (Ms-PL) 
// ***********************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mytrip.Votes.Models;
using Mytrip.Mvc.Models;
using Mytrip.Votes;
using Mytrip.Votes.Repository;
using Mytrip.Votes.Helpers;
using Mytrip.Votes.Repository.DataEntities;
using Mytrip.Mvc;
using System.Xml.Linq;

namespace Mytrip.Votes.Controllers
{
    [HandleError]
    [Localization]
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
        VotesSetting _vs;
        public VotesSetting vs
        {
            get
            {
                if (_vs == null)
                    _vs = new VotesSetting();
                return _vs;
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

        // *****************************
        // URL: /Votes/Index/count/
        // ******  Votes Manager  ******
        [RoleAdmin]
        public ActionResult Index(int id = 3)
        {
            VotesModel model = new VotesModel();
            model.Count = id;
            model.QtAnswers = new SelectList(new int[] { 2, 3, 4, 5, 6, 7, 8, 9, 10 }, id);
            model.Answers = new string[id];
            model.CloseDate = DateTime.Today.AddYears(1).ToString("yyyy-MM-dd");
            model.Theme = new ThemeSetting().theme();
            return View(model);
        }
        [HttpPost]
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
        [RoleAdmin]
        public ActionResult Delete(int id)
        {
            vr.DeleteQuestion(id);
            return RedirectToAction("Index");
        }
        // **************************
        // URL: /Votes/OnOff/id/
        // ******  OnOff Vote  ******
        [RoleAdmin]
        public ActionResult OnOff(int id)
        {
            vr.OnOffQuestion(id);
            return RedirectToAction("Index");
        }
        // *****************************
        // URL: /Votes/View/chosenAnswer/
        // ********  Vote View  ********
        [HttpPost]
        public ActionResult View(int chosenAnswer, string voteButton)
        {
            if (Request.IsAjaxRequest())
            {
                mytrip_votesquestion q;
                if (voteButton == VotesLanguage.vote)
                    q = vr.AddVote(chosenAnswer);
                else
                    q = vr.GetActiveQuestion(culture);
                return Content(VotesHelpers.BuildResult(q));
            }
            return View("Votes");
        }
        // *****************************
        // URL: /Votes/View/QuestionId/
        // ******** Edit Vote  ********
        [RoleAdmin]
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
        // URL: /Votes/View/chosenAnswer/
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
                            , DateTime.Parse(model.CloseDate),model.Answers);
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
        [RoleAdmin]
        public ActionResult VotesSetting()
        {
            VotesSettingModel model = new VotesSettingModel();
            model.nameVotes = vs.NameVotesPage();
            model.unlockVotes = vs.unlockVotes();
            return View(model);
        }
        [RoleAdmin]
        [HttpPost]
        public ActionResult VotesSetting(VotesSettingModel model)
        {
            if (ModelState.IsValid)
            {
            string _absolutDirectory = GeneralMethods.xmlAbsoluteDirectory("MytripConfiguration");
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var article = _doc.Root.Elements("Mytrip.Votes").Elements("add");
            article.FirstOrDefault(x => x.Attribute("name").Value == "unlockVotes")
                .SetAttributeValue("value", model.unlockVotes.ToString());
            var artpage = article.FirstOrDefault(x => x.Attribute("name").Value == "nameVotes").Elements("add");
            artpage.FirstOrDefault(x => x.Attribute("value").Value == culture.ToLower())
                .SetAttributeValue("name", model.nameVotes);
            _doc.Save(_absolutDirectory);
            return RedirectToAction("Index", "Home");  }
            return View(model);
        }
    }
}

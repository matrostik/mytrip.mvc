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
using Mytrip.Mvc.Models;
using System.Configuration;
using System.Web.Configuration;
using Mytrip.Mvc.Repository;
using MySql.Data.MySqlClient;
using Mytrip.Mvc.StartUpSettings;
using System.Net;
using System.IO;
using System.Text;
using System.Reflection;

namespace Mytrip.Mvc.Controllers
{
    /// <summary>
    /// Home Controller
    /// </summary>
    [HandleError]
    [Localization]
    public class HomeController : Controller
    {
        #region Properties
        ICoreRepository _coreRepo;
        public ICoreRepository coreRepo
        {
            get
            {
                if (_coreRepo == null)
                    _coreRepo = new ICoreRepository();
                return _coreRepo;
            }
        }
        UsersSetting _userset;
        public UsersSetting userset
        {
            get
            {
                if (_userset == null)
                    _userset = new UsersSetting();
                return _userset;
            }
        }
        CoreSetting _corset;
        public CoreSetting corset
        {
            get
            {
                if (_corset == null)
                    _corset = new CoreSetting();
                return _corset;
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
        /// URL: /Home/Index
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult Index()
        {
            HomeModel model = new HomeModel();
            bool develop = corset.Development();
            if (develop)
                model.title = CoreLanguage.license_agreement;
            else
                model.title = corset.NameHomePage();
            model.developer = develop;
            return View(model);
        }
        /// <summary>
        /// URL: /Home/About
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult About()
        {
            AboutModel model = new AboutModel();
            model.body = coreRepo.aboutRepo.GetAbout(Session["culture"].ToString());
            model.approvedemail = userset.unlockSendEmail();
            model.title = corset.NameAboutPage();
            if (User.Identity.IsAuthenticated)
            {
                model.name = User.Identity.Name;
                model.email = coreRepo.membershipRepo.mtGetUserEmail(User.Identity.Name);
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult About(AboutModel model)
        {
            coreRepo.emailRepo.SendEmail(coreRepo.emailRepo.from_email(), (model.email + " " + model.name), model.messege);
            model.body = coreRepo.aboutRepo.GetAbout(Session["culture"].ToString());
            model.approvedemail = userset.unlockSendEmail();
            if (User.Identity.IsAuthenticated)
            {
                model.name = User.Identity.Name;
                model.email = coreRepo.membershipRepo.mtGetUserEmail(User.Identity.Name);
            }
            return View(model);
        }
        public ActionResult Profile(string id)
        {
            ProfileUsersModel model = new ProfileUsersModel();
            model.UserName = id;
            model.Email = coreRepo.membershipRepo.mtGetUserEmail(id);
            return View(model);
        }
    }
}

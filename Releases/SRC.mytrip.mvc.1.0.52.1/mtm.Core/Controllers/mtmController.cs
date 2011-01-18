/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about mytrip.mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System;
using System.Web;
using System.Web.Mvc;
using mtm.Core.Settings;
using mtm.Core.Repository;
using System.Drawing;
using mtm.Core.Files;
using System.ComponentModel;
using System.IO;

namespace mtm.Core.Controllers
{
    /// <summary>Методы контроллеров не отдающие представление 
    /// но необходимые для работы локализации, смены тем,
    /// аккардионов
    /// </summary>
    public class mtmController : Controller
    {
        StatisticRepository _StatisticRepository;
        public StatisticRepository statisticRepo
        {
            get
            {
                if (_StatisticRepository == null)
                    _StatisticRepository = new StatisticRepository();
                return _StatisticRepository;
            }
        }

        #region Аккардион
        // **********************************************
        // Аккардион
        // **********************************************

        /// <summary>POST: /mtm/Accardion
        /// Запись в coocies и сессию информации о действиях пользователя
        /// с акардионами
        /// </summary>
        /// <param name="id">название аккардиона над которым производилось действие</param>
        [HttpPost]
        public void Accardion(string id)
        {
            if (Request.IsAjaxRequest())
            {
                string accardion = string.Empty;
                if (Session["accardion"] == null && HttpContext.Request.Cookies["mtAccardion"] == null)
                    accardion = id;
                else if (HttpContext.Request.Cookies["mtAccardion"] != null)
                {
                    accardion = HttpContext.Request.Cookies["mtAccardion"].Value;
                    if (accardion != null && !accardion.Contains(id))
                        accardion += "|" + id;
                    else if (accardion != null && accardion.Contains(id))
                    {
                        accardion = accardion.Replace(id, "").Replace("||", "|");
                    }
                }
                else if (Session["accardion"] != null)
                {
                    accardion = Session["accardion"].ToString();
                    if (accardion != null && !accardion.Contains(id))
                        accardion += "|" + id;
                    else if (accardion != null && accardion.Contains(id))
                    {
                        accardion = accardion.Replace(id, "").Replace("||", "|");
                    }
                }
                Session["accardion"] = accardion;
                HttpCookie cookie = new HttpCookie("mtAccardion", accardion);
                cookie.Expires = DateTime.Now.AddYears(1);
                Response.Cookies.Add(cookie);
            }

        }

        /// <summary>POST: /mtm/AccardionCookies
        /// Отдает список закрытых аккардионов
        /// </summary>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult AccardionCookies()
        {
            string _acc = (HttpContext.Request.Cookies["mtAccardion"] != null)
                ? HttpContext.Request.Cookies["mtAccardion"].Value
                : ((Session["accardion"] != null)
                ? Session["accardion"].ToString()
                : string.Empty);
            if (!String.IsNullOrEmpty(_acc))
                _acc = _acc.Replace("_", "").Replace("||", "|");
            return Content(_acc);
        }

        //****************** E N D **********************
        #endregion

        #region Локализация
        // **********************************************
        // Локализация
        // **********************************************

        /// <summary>GET: /mtm/Language
        /// Смена языка и возвращение пользователя на туже страницу
        /// </summary>
        /// <param name="id">выбранная культура</param>
        /// <param name="id2">url для возврата пользователя</param>
        /// <returns>ActionResult</returns>
        public ActionResult Language(string id, string id2)
        {
            Session["culture"] = id;
            HttpCookie cookie = new HttpCookie("mtCulture", id);
            cookie.Expires = DateTime.Now.AddYears(1);
            Response.Cookies.Add(cookie);
            id2 = id2.Replace("(x)", "/");
            return Redirect(id2);
        }

        //****************** E N D **********************
        #endregion

        #region Тема оформления, текущий браузер и статистика
        // **********************************************
        // Тема оформления и текущий браузер
        // **********************************************

        /// <summary>GET: /mtm/Theme
        /// Смена темы оформления и возвращение пользователя на туже страницу
        /// </summary>
        /// <param name="id">тема</param>
        /// <param name="id2">url для возврата пользователя</param>
        /// <returns>ActionResult</returns>
        public ActionResult Theme(string id, string id2)
        {
            if (ThemeSetting.unlockWrap())
            {
                string absolutDirectory = HttpContext.Server.MapPath("/Theme/" + id + "/wrap");
                DirectoryInfo _absolutDirectory = new DirectoryInfo(absolutDirectory);
                if (!_absolutDirectory.Exists)
                    GeneralMethods.RestartApplication();
            }
            else if (!ThemeSetting.unlockWrap() && ThemeSetting._unlockWrap())
            {
                string absolutDirectory = HttpContext.Server.MapPath("/Theme/" + id + "/wrap");
                DirectoryInfo _absolutDirectory = new DirectoryInfo(absolutDirectory);
                if (_absolutDirectory.Exists)
                    GeneralMethods.RestartApplication();
            }
            Session["theme"] = id;
            HttpCookie cookie = new HttpCookie("mtTheme", id);
            cookie.Expires = DateTime.Now.AddYears(1);
            Response.Cookies.Add(cookie);
            id2 = id2.Replace("(x)", "/");            
            return Redirect(id2);
        }

        /// <summary>POST: /mtm/Theme
        /// Текущая тема оформления
        /// </summary>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult Theme()
        {
            return Content(ThemeSetting.theme());
        }

        /// <summary>POST: /mtm/Brouse
        /// Текущий браузер
        /// </summary>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult Browser()
        {
            string browser= HttpContext.Request.Browser.Browser; 
            string version = HttpContext.Request.Browser.Version;        
            return Content(string.Concat(browser,"_",version));
        }
        [HttpPost]
        public void Statistic(string path,string refer)
        {

            statisticRepo.CreateStatistic(path.Replace("(y)", "/").Replace("(z)", "&"), refer.Replace("(y)", "/").Replace("(z)", "&")); 
        }
        //****************** E N D **********************
        #endregion
        public FileContentResult Captcha(int id, int id2, string id3)
        {
            CaptchaRepository captchaRepo = new CaptchaRepository();
            string solution = string.Empty;
            Bitmap _image = captchaRepo.mtGenerateImage(id, id2, id3, out solution);
            byte[] bytes = (byte[])TypeDescriptor.GetConverter(_image).ConvertTo(_image, typeof(byte[]));
            Session["antibotimage"] = solution;
            return File(bytes, "image/jpeg");
        }

        public FileContentResult FileIcon(string id)
        {
            byte[] _image = FilesIcon.ResourceManager.GetObject(id) as byte[];
            return File(_image, "image/png");
        }
        public FileContentResult Smile(string id)
        {
            byte[] _image = SmilesCollection.ResourceManager.GetObject(id) as byte[];
            return File(_image, "image/gif");
        }

    }
}

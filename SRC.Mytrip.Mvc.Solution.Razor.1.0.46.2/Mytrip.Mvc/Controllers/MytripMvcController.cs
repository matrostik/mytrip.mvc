/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about Mytrip.Mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System;
using System.Web;
using System.Web.Mvc;
using Mytrip.Mvc.Settings;

namespace Mytrip.Mvc.Controllers
{
    /// <summary>Методы контроллеров не отдающие представление 
    /// но необходимые для работы локализации, смены тем,
    /// аккардионов
    /// </summary>
    public class MytripMvcController : Controller
    {
        #region Аккардион
        // **********************************************
        // Аккардион
        // **********************************************

        /// <summary>POST: /MytripMvc/Accardion
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
                if (Session["accardion"] == null && HttpContext.Request.Cookies["myTripAccardion"] == null)
                    accardion = id;
                else if (HttpContext.Request.Cookies["myTripAccardion"] != null)
                {
                    accardion = HttpContext.Request.Cookies["myTripAccardion"].Value;
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
                HttpCookie cookie = new HttpCookie("myTripAccardion", accardion);
                cookie.Expires = DateTime.Now.AddYears(1);
                Response.Cookies.Add(cookie);
            }

        }

        /// <summary>POST: /MytripMvc/AccardionCookies
        /// Отдает список закрытых аккардионов
        /// </summary>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult AccardionCookies()
        {
            string _acc = (HttpContext.Request.Cookies["myTripAccardion"] != null)
                ? HttpContext.Request.Cookies["myTripAccardion"].Value
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

        /// <summary>GET: /MytripMvc/Language
        /// Смена языка и возвращение пользователя на туже страницу
        /// </summary>
        /// <param name="id">выбранная культура</param>
        /// <param name="id2">url для возврата пользователя</param>
        /// <returns>ActionResult</returns>
        public ActionResult Language(string id, string id2)
        {
            Session["culture"] = id;
            HttpCookie cookie = new HttpCookie("myTripCulture", id);
            cookie.Expires = DateTime.Now.AddYears(1);
            Response.Cookies.Add(cookie);
            id2 = id2.Replace("(x)", "/");
            return Redirect(id2);
        }

        //****************** E N D **********************
        #endregion

        #region Тема оформления и текущий браузер
        // **********************************************
        // Тема оформления и текущий браузер
        // **********************************************

        /// <summary>GET: /MytripMvc/Theme
        /// Смена темы оформления и возвращение пользователя на туже страницу
        /// </summary>
        /// <param name="id">тема</param>
        /// <param name="id2">url для возврата пользователя</param>
        /// <returns>ActionResult</returns>
        public ActionResult Theme(string id, string id2)
        {
            Session["theme"] = id;
            HttpCookie cookie = new HttpCookie("myTripTheme", id);
            cookie.Expires = DateTime.Now.AddYears(1);
            Response.Cookies.Add(cookie);
            id2 = id2.Replace("(x)", "/");
            return Redirect(id2);
        }

        /// <summary>POST: /MytripMvc/Theme
        /// Текущая тема оформления
        /// </summary>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult Theme()
        {
            return Content(ThemeSetting.theme());
        }

        /// <summary>POST: /MytripMvc/Brouse
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

        //****************** E N D **********************
        #endregion
    }
}

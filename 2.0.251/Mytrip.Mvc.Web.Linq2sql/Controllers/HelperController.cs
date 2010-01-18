using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mytrip.Mvc.Model.Linq2sql;
using System.Configuration;
using Mytrip.Mvc.Web.Linq2sql.Models;

namespace Mytrip.Mvc.Web.Linq2sql.Controllers
{
    [Localization]
    public class HelperController : Controller
    {
        /*  ПОДКЛЮЧЕНИЕ К МОДЕЛИ  */
        /*-----------------------------------------------------*/
        IRepository _db;
        public IRepository _dc
        {
            get
            {
                if (_db == null)
                    _db = new IRepository(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
                return _db;
            }
        }
        /// <summary>
        /// Cвойство для извлечения или сохранения культуры сайта
        /// </summary>
        public string culture
        {
            get
            {
                if (Session["culture"] == null)
            {
                if (_dc.dm_model.Get_models().Count() == 0)
                {
                   Session["culture"] = "en-us";
               }
                else
                {
                    Session["culture"] = _dc.dm_model.Get_model().mt_language.param;
                }
            }
               
                if (_dc.dm_model.Get_language(Session["culture"].ToString()) == null)
                {
                    Session["culture"] = "en-us";
                }
                return Session["culture"].ToString();
            }
            set
            {
                Session["culture"] = value;
                SetCultureToCookie(Session["culture"].ToString());
            }
        }
        /// <summary>
        /// Сохраняем культуру в куки
        /// </summary>
        /// <param name="language">locale</param>
        public void SetCultureToCookie(string language)
        {
            // add site current culture to cookies
            HttpCookie myCookie = new HttpCookie("myTripCulture", language);
            myCookie.Expires = DateTime.Now.AddMonths(12);
            if (Response.Cookies["myCookie"] == null)
                Response.Cookies.Add(myCookie);
            else
            {
                Response.Cookies.Remove("myCookie");
                Response.Cookies.Add(myCookie);
            }
        }

        public void Mytrip_Mvc_Heder(string language_url)
        {
            ////if (Session["culture"] == null)
            ////{
            ////    if (_dc.dm_model.Get_models().Count() == 0)
            ////    {
            ////        Session["culture"] = "en-us";
            ////    }
            ////    else
            ////    {
            ////        Session["culture"] = _dc.dm_model.Get_model().mt_language.param;
            ////    }
            ////}
            ////string culture = Session["culture"].ToString();
            ViewData["helper_logon_url"] = language_url;
            ViewData["helper_language_url"] = language_url;
            //рубрики новостей
            ViewData["helper_news_category"] = _dc.dm_artycle_category.Get_category_news(culture);
            //количество рубрик новостей
            ViewData["helper_news_category_count"] = _dc.dm_artycle_category.Get_category_news(culture).Count();
            //рубрики статей недобавленные в меню как отдельный пункт
            ViewData["helper_artycle_category_no_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_false(culture);
            //количество рубрики статей недобавленные в меню как отдельный пункт
            ViewData["helper_artycle_category_no_menu_count"] = _dc.dm_artycle_category.Get_categories_addmenu_false(culture).Count();

            //блоги
            ViewData["helper_blog"] = _dc.dm_artycle_category.Get_blogs(culture);
            //количество блогов
            ViewData["helper_blog_count"] = _dc.dm_artycle_category.Get_blogs(culture).Count();

            //статьи для меню
            ViewData["helper_artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true(culture);
            //доменное имя
            ViewData["abstract_model_domain"] = _dc.dm_model.Get_model().DomainName;
            //bool включена captcha
            ViewData["abstract_captcha"] = _dc.dm_model.Get_model().Captcha_approved;
            //языковая панель
            ViewData["abstract_lang_panel"] = _dc.dm_model.Get_model().Language_approved;
            //список языков
            ViewData["abstract_language"] = _dc.dm_model.Get_languages();
            //количество комментов для активации блога
            ViewData["abstract_model_comment"] = _dc.dm_model.Get_model().CountComment;
            //подтверждение по email
            ViewData["abstract_email_approved"] = _dc.dm_model.Get_model().Email_approved;
            //bool разрешены блоги или нет
            ViewData["abstract_model_blog"] = _dc.dm_model.Get_model().Blog;
            //bool разрешены новости или нет
            ViewData["abstract_model_news"] = _dc.dm_model.Get_model().News;
            //bool разрешены статьи или нет
            ViewData["abstract_model_artycle"] = _dc.dm_model.Get_model().Artycle;




        }
        public void Mytrip_Mvc_Heder(string logon_url, string language_url)
        {
            //////if (Session["culture"] == null)
            //////{
            //////    if (_dc.dm_model.Get_models().Count() == 0)
            //////    {
            //////        Session["culture"] = "en-us";
            //////    }
            //////    else
            //////    {
            //////        Session["culture"] = _dc.dm_model.Get_model().mt_language.param;
            //////    }
            //////}
            //////string culture = Session["culture"].ToString();
            ViewData["helper_logon_url"] = logon_url;
            ViewData["helper_language_url"] = language_url;
            //рубрики новостей
            ViewData["helper_news_category"] = _dc.dm_artycle_category.Get_category_news(culture);
            //количество рубрик новостей
            ViewData["helper_news_category_count"] = _dc.dm_artycle_category.Get_category_news(culture).Count();
            //рубрики статей недобавленные в меню как отдельный пункт
            ViewData["helper_artycle_category_no_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_false(culture);
            //количество рубрики статей недобавленные в меню как отдельный пункт
            ViewData["helper_artycle_category_no_menu_count"] = _dc.dm_artycle_category.Get_categories_addmenu_false(culture).Count();
            
            //блоги
            ViewData["helper_blog"] = _dc.dm_artycle_category.Get_blogs(culture);
            //количество блогов
            ViewData["helper_blog_count"] = _dc.dm_artycle_category.Get_blogs(culture).Count();
           
            //статьи для меню
            ViewData["helper_artycle_category_menu"] = _dc.dm_artycle_category.Get_categories_addmenu_true(culture);
                //доменное имя
                ViewData["abstract_model_domain"] = _dc.dm_model.Get_model().DomainName;
                //bool включена captcha
                ViewData["abstract_captcha"] = _dc.dm_model.Get_model().Captcha_approved;
                //языковая панель
                ViewData["abstract_lang_panel"] = _dc.dm_model.Get_model().Language_approved;
                //список языков
                ViewData["abstract_language"] = _dc.dm_model.Get_languages();
                //количество комментов для активации блога
                ViewData["abstract_model_comment"] = _dc.dm_model.Get_model().CountComment;
                //подтверждение по email
                ViewData["abstract_email_approved"] = _dc.dm_model.Get_model().Email_approved;
                //bool разрешены блоги или нет
                ViewData["abstract_model_blog"] = _dc.dm_model.Get_model().Blog;
                //bool разрешены новости или нет
                ViewData["abstract_model_news"] = _dc.dm_model.Get_model().News;
                //bool разрешены статьи или нет
                ViewData["abstract_model_artycle"] = _dc.dm_model.Get_model().Artycle;

          
           
            
        }
        public void Mytrip_Mvc_Right_Column()
        {
            ////string culture = Session["culture"].ToString();
            //теги
            ViewData["helper_teg"] = _dc.dm_artycle.Get_tegs(culture);
            var teg = _dc.dm_artycle.Get_tegs(culture);
            int teg_count = 0;
            string teg_name = " ";
            foreach (mt_artycle_in_teg x in teg)
            {
                if (x.mt_artycle_teg.Title != teg_name)
                {
                    teg_name = x.mt_artycle_teg.Title;
                    teg_count++;
                }
            }
            ViewData["helper_teg_count"] = teg_count;
            //меню пользователя
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                //все комментарии пользователя
                ViewData["helper_comment_user"] = _dc.dm_artycle_comment.Get_сomments_start_user();
                //количество блогов пользователя
                ViewData["helper_blog_user_count"] = _dc.dm_artycle_category.Get_blogs_user(culture).Count();
                //все блдги пользователя
                ViewData["helper_blog_user"] = _dc.dm_artycle_category.Get_blogs_user(culture);
            }
        }
        public void Mytrip_Mvc_Menu_Active(string menu_url)
        {
            ViewData["helper_menu_url"] = menu_url;
        }
        public void Mytrip_Mvc_Back_Url(string back_url)
        {
            ViewData["helper_back_url"] = back_url;
        }
        public void Mytrip_Mvc_Category_Status(int status)
        {
            ViewData["helper_category_status"] = status;
        }
        public void Mytrip_Mvc_Pager_Site(string pager_url, int pager_int, int pager_cat,
            int pager_page, string pager_path)
        {
            #region пейджинг страницы
            ViewData["helper_pager_url"] = pager_url;
            ViewData["helper_pager_int"] = pager_int;
            ViewData["helper_pager_cat"] = pager_cat;
            ViewData["helper_pager_page"] = pager_page;
            ViewData["helper_pager_path"] = pager_path;
            #endregion
        }
        public void Mytrip_Mvc_Move_Content()
        {
           /* string culture = Session["culture"].ToString();
            if (_dc.dm_model.Get_model().Move_article == true)
            {
                if (_dc.dm_model.Get_model().Move_news == true)
                {
                    if (_dc.dm_model.Get_model().Move_blog == true)
                    {
                        //Все и новости и статьи и посты
                        ViewData["helper_move_content"] = _dc.dm_movecontent.Get_all_move_content(culture);
                    }
                    else
                    {
                        //тока новости и статьи
                        ViewData["helper_move_content"] = _dc.dm_movecontent.Get_article_news_move_content(culture);
                    }
                }
                else
                {
                    if (_dc.dm_model.Get_model().Move_blog == true)
                    {
                        //тока посты и статьи
                        ViewData["helper_move_content"] = _dc.dm_movecontent.Get_article_post_move_content(culture);
                    }
                    else
                    {
                        //тока статьи
                        ViewData["helper_move_content"] = _dc.dm_movecontent.Get_article_move_content(culture);
                    }
                }
            }
            else
            {
                if (_dc.dm_model.Get_model().Move_news == true)
                {
                    if (_dc.dm_model.Get_model().Move_blog == true)
                    {
                        //новости и посты
                        ViewData["helper_move_content"] = _dc.dm_movecontent.Get_news_post_move_content(culture);
                    }
                    else
                    {
                        //тока новости
                        ViewData["helper_move_content"] = _dc.dm_movecontent.Get_news_move_content(culture);
                    }
                }
                else
                {
                    if (_dc.dm_model.Get_model().Move_blog == true)
                    {
                        //тока посты 
                        ViewData["helper_move_content"] = _dc.dm_movecontent.Get_post_move_content(culture);
                    }
                    else
                    {
                        //ничего
                        ViewData["helper_move_content"] = _dc.dm_movecontent.Get_no_move_content();
                    }
                }
            } */       
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Mvc;
using Mytrip.Mvc.Language;

namespace Mytrip.Mvc.Web.Linq2sql.HtmlHelpers
{
    public static class MenuHelper
    {
        public static string Menu(this HtmlHelper html, string urla,bool captcha)
        {
            StringBuilder result = new StringBuilder();
           

           
            if (urla == "logon")
            {
                TagBuilder div_gr_5 = new TagBuilder("li");
                TagBuilder a_5 = new TagBuilder("a");
                a_5.MergeAttribute("href", "/B/A");
                a_5.MergeAttribute("style", "color: #2E2633; background-image: url(/content/images/4.png); border: 1px solid #E5C365;");
                a_5.InnerHtml = Mytrip_Mvc_Language.menu_logon;
                div_gr_5.InnerHtml = a_5.ToString();
                result.AppendLine(div_gr_5.ToString());
            } if (urla == "registr")
            {

                if (captcha == false)
                {
                    TagBuilder div_gr_6 = new TagBuilder("li");
                    TagBuilder a_6 = new TagBuilder("a");
                    a_6.MergeAttribute("href", "/B/B");
                    a_6.MergeAttribute("style", "color: #2E2633; background-image: url(/content/images/4.png); border: 1px solid #E5C365;");
                    a_6.InnerHtml = Mytrip_Mvc_Language.menu_register;
                    div_gr_6.InnerHtml = a_6.ToString();
                    result.AppendLine(div_gr_6.ToString());
                }
                else
                {
                    TagBuilder div_gr_6 = new TagBuilder("li");
                    TagBuilder a_6 = new TagBuilder("a");
                    a_6.MergeAttribute("href", "/B/H");
                    a_6.MergeAttribute("style", "color: #2E2633; background-image: url(/content/images/4.png); border: 1px solid #E5C365;");
                    a_6.InnerHtml = Mytrip_Mvc_Language.menu_register;
                    div_gr_6.InnerHtml = a_6.ToString();
                    result.AppendLine(div_gr_6.ToString());
                }
            }
            if (urla == "tegs")
            {
                TagBuilder div_gr_7 = new TagBuilder("li");
                TagBuilder a_7 = new TagBuilder("a");
                a_7.MergeAttribute("style", "color: #2E2633; background-image: url(/content/images/4.png); border: 1px solid #E5C365;");
                a_7.InnerHtml = Mytrip_Mvc_Language.menu_tegs;
                div_gr_7.InnerHtml = a_7.ToString();
                result.AppendLine(div_gr_7.ToString());

            } if (urla == "search")
            {
                TagBuilder div_gr_7 = new TagBuilder("li");
                TagBuilder a_7 = new TagBuilder("a");
                a_7.MergeAttribute("style", "color: #2E2633; background-image: url(/content/images/4.png); border: 1px solid #E5C365;");
                a_7.InnerHtml = Mytrip_Mvc_Language.menu_search;
                div_gr_7.InnerHtml = a_7.ToString();
                result.AppendLine(div_gr_7.ToString());

            } if (urla == "thema_blog")
            {
                TagBuilder div_gr_8 = new TagBuilder("li");
                TagBuilder a_8 = new TagBuilder("a");
                a_8.MergeAttribute("style", "color: #2E2633; background-image: url(/content/images/4.png); border: 1px solid #E5C365;");
                a_8.InnerHtml = Mytrip_Mvc_Language.menu_create_blog_theme;
                div_gr_8.InnerHtml = a_8.ToString();
                result.AppendLine(div_gr_8.ToString());
            } if (urla == "thema_news")
            {
                TagBuilder div_gr_8 = new TagBuilder("li");
                TagBuilder a_8 = new TagBuilder("a");
                a_8.MergeAttribute("style", "color: #2E2633; background-image: url(/content/images/4.png); border: 1px solid #E5C365;");
                a_8.InnerHtml = Mytrip_Mvc_Language.menu_create_subheading_news;
                div_gr_8.InnerHtml = a_8.ToString();
                result.AppendLine(div_gr_8.ToString());
            }
            if (urla == "thema_article")
            {
                TagBuilder div_gr_8 = new TagBuilder("li");
                TagBuilder a_8 = new TagBuilder("a");
                a_8.MergeAttribute("style", "color: #2E2633; background-image: url(/content/images/4.png); border: 1px solid #E5C365;");
                a_8.InnerHtml = Mytrip_Mvc_Language.menu_create_subheading_article;
                div_gr_8.InnerHtml = a_8.ToString();
                result.AppendLine(div_gr_8.ToString());
            }
            if (urla == "rub_artycle")
            {
                TagBuilder div_gr_9 = new TagBuilder("li");
                TagBuilder a_9 = new TagBuilder("a");
                a_9.MergeAttribute("style", "color: #2E2633; background-image: url(/content/images/4.png); border: 1px solid #E5C365;");
                a_9.InnerHtml = Mytrip_Mvc_Language.menu_create_heading_article;
                div_gr_9.InnerHtml = a_9.ToString();
                result.AppendLine(div_gr_9.ToString());
            }
            if (urla == "rub_news")
            {
                TagBuilder div_gr_9 = new TagBuilder("li");
                TagBuilder a_9 = new TagBuilder("a");
                a_9.MergeAttribute("style", "color: #2E2633; background-image: url(/content/images/4.png); border: 1px solid #E5C365;");
                a_9.InnerHtml = Mytrip_Mvc_Language.menu_create_heading_news;
                div_gr_9.InnerHtml = a_9.ToString();
                result.AppendLine(div_gr_9.ToString());
            }
            if (urla == "edit_rub_artycle")
            {
                TagBuilder div_gr_10 = new TagBuilder("li");
                TagBuilder a_10 = new TagBuilder("a");
                a_10.MergeAttribute("style", "color: #2E2633; background-image: url(/content/images/4.png); border: 1px solid #E5C365;");
                a_10.InnerHtml = Mytrip_Mvc_Language.menu_edit_heading_article;
                div_gr_10.InnerHtml = a_10.ToString();
                result.AppendLine(div_gr_10.ToString());
            }
            if (urla == "edit_rub_news")
            {
                TagBuilder div_gr_10 = new TagBuilder("li");
                TagBuilder a_10 = new TagBuilder("a");
                a_10.MergeAttribute("style", "color: #2E2633; background-image: url(/content/images/4.png); border: 1px solid #E5C365;");
                a_10.InnerHtml = Mytrip_Mvc_Language.menu_edit_heading_news;
                div_gr_10.InnerHtml = a_10.ToString();
                result.AppendLine(div_gr_10.ToString());
            }
            if (urla == "edit_thema_article")
            {
                TagBuilder div_gr_11 = new TagBuilder("li");
                TagBuilder a_11 = new TagBuilder("a");
                a_11.MergeAttribute("style", "color: #2E2633; background-image: url(/content/images/4.png); border: 1px solid #E5C365;");
                a_11.InnerHtml = Mytrip_Mvc_Language.menu_edit_subheading_article;
                div_gr_11.InnerHtml = a_11.ToString();
                result.AppendLine(div_gr_11.ToString());
            }
            if (urla == "edit_thema_news")
            {
                TagBuilder div_gr_11 = new TagBuilder("li");
                TagBuilder a_11 = new TagBuilder("a");
                a_11.MergeAttribute("style", "color: #2E2633; background-image: url(/content/images/4.png); border: 1px solid #E5C365;");
                a_11.InnerHtml = Mytrip_Mvc_Language.menu_edit_subheading_news;
                div_gr_11.InnerHtml = a_11.ToString();
                result.AppendLine(div_gr_11.ToString());
            }
            if (urla == "edit_thema_blog")
            {
                TagBuilder div_gr_11 = new TagBuilder("li");
                TagBuilder a_11 = new TagBuilder("a");
                a_11.MergeAttribute("style", "color: #2E2633; background-image: url(/content/images/4.png); border: 1px solid #E5C365;");
                a_11.InnerHtml = Mytrip_Mvc_Language.menu_edit_heding_blog;
                div_gr_11.InnerHtml = a_11.ToString();
                result.AppendLine(div_gr_11.ToString());
            }
            if (urla == "edit_blog")
            {
                TagBuilder div_gr_12 = new TagBuilder("li");
                TagBuilder a_12 = new TagBuilder("a");
                a_12.MergeAttribute("style", "color: #2E2633; background-image: url(/content/images/4.png); border: 1px solid #E5C365;");
                a_12.InnerHtml = Mytrip_Mvc_Language.menu_edit_blog;
                div_gr_12.InnerHtml = a_12.ToString();
                result.AppendLine(div_gr_12.ToString());
            } if (urla == "create_artycle")
            {
                TagBuilder div_gr_12 = new TagBuilder("li");
                TagBuilder a_12 = new TagBuilder("a");
                a_12.MergeAttribute("style", "color: #2E2633; background-image: url(/content/images/4.png); border: 1px solid #E5C365;");
                a_12.InnerHtml = Mytrip_Mvc_Language.menu_write_article;
                div_gr_12.InnerHtml = a_12.ToString();
                result.AppendLine(div_gr_12.ToString());
            } if (urla == "edit_artycle")
            {
                TagBuilder div_gr_13 = new TagBuilder("li");
                TagBuilder a_13 = new TagBuilder("a");
                a_13.MergeAttribute("style", "color: #2E2633; background-image: url(/content/images/4.png); border: 1px solid #E5C365;");
                a_13.InnerHtml = Mytrip_Mvc_Language.menu_edit_article;
                div_gr_13.InnerHtml = a_13.ToString();
                result.AppendLine(div_gr_13.ToString());
            } if (urla == "create_post")
            {
                TagBuilder div_gr_14 = new TagBuilder("li");
                TagBuilder a_14 = new TagBuilder("a");
                a_14.MergeAttribute("style", "color: #2E2633; background-image: url(/content/images/4.png); border: 1px solid #E5C365;");
                a_14.InnerHtml = Mytrip_Mvc_Language.menu_write_post;
                div_gr_14.InnerHtml = a_14.ToString();
                result.AppendLine(div_gr_14.ToString());
            } if (urla == "edit_post")
            {
                TagBuilder div_gr_15 = new TagBuilder("li");
                TagBuilder a_15 = new TagBuilder("a");
                a_15.MergeAttribute("style", "color: #2E2633; background-image: url(/content/images/4.png); border: 1px solid #E5C365;");
                a_15.InnerHtml = Mytrip_Mvc_Language.menu_edit_post;
                div_gr_15.InnerHtml = a_15.ToString();
                result.AppendLine(div_gr_15.ToString());
            } if (urla == "create_news")
            {
                TagBuilder div_gr_16 = new TagBuilder("li");
                TagBuilder a_16 = new TagBuilder("a");
                a_16.MergeAttribute("style", "color: #2E2633; background-image: url(/content/images/4.png); border: 1px solid #E5C365;");
                a_16.InnerHtml = Mytrip_Mvc_Language.menu_write_news;
                div_gr_16.InnerHtml = a_16.ToString();
                result.AppendLine(div_gr_16.ToString());
            } if (urla == "edit_news")
            {
                TagBuilder div_gr_17 = new TagBuilder("li");
                TagBuilder a_17 = new TagBuilder("a");
                a_17.MergeAttribute("style", "color: #2E2633; background-image: url(/content/images/4.png); border: 1px solid #E5C365;");
                a_17.InnerHtml = Mytrip_Mvc_Language.menu_edit_news;
                div_gr_17.InnerHtml = a_17.ToString();
                result.AppendLine(div_gr_17.ToString());
            } if (urla == "edit_comm")
            {
                TagBuilder div_gr_18 = new TagBuilder("li");
                TagBuilder a_18 = new TagBuilder("a");
                a_18.MergeAttribute("style", "color: #2E2633; background-image: url(/content/images/4.png); border: 1px solid #E5C365;");
                a_18.InnerHtml = Mytrip_Mvc_Language.menu_edit_comment;
                div_gr_18.InnerHtml = a_18.ToString();
                result.AppendLine(div_gr_18.ToString());
            } if (urla == "file")
            {
                TagBuilder div_gr_19 = new TagBuilder("li");
                TagBuilder a_19 = new TagBuilder("a");
                a_19.MergeAttribute("style", "color: #2E2633; background-image: url(/content/images/4.png); border: 1px solid #E5C365;");
                a_19.InnerHtml = Mytrip_Mvc_Language.menu_file_manager;
                div_gr_19.InnerHtml = a_19.ToString();
                result.AppendLine(div_gr_19.ToString());
            } if (urla == "user")
            {
                TagBuilder div_gr_20 = new TagBuilder("li");
                TagBuilder a_20 = new TagBuilder("a");
                a_20.MergeAttribute("href", "/A/D/0/1/25/Users");
                a_20.MergeAttribute("style", "color: #2E2633; background-image: url(/content/images/4.png); border: 1px solid #E5C365;");
                a_20.InnerHtml = Mytrip_Mvc_Language.menu_management_users;
                div_gr_20.InnerHtml = a_20.ToString();
                result.AppendLine(div_gr_20.ToString());
            } if (urla == "site_setting")
            {
                TagBuilder div_gr_21 = new TagBuilder("li");
                TagBuilder a_21 = new TagBuilder("a");
                a_21.MergeAttribute("style", "color: #2E2633; background-image: url(/content/images/4.png); border: 1px solid #E5C365;");
                a_21.InnerHtml = Mytrip_Mvc_Language.menu_site_adjustment;
                div_gr_21.InnerHtml = a_21.ToString();
                result.AppendLine(div_gr_21.ToString());
            }
            if (urla == "change_password")
            {
                TagBuilder div_gr_22 = new TagBuilder("li");
                TagBuilder a_22 = new TagBuilder("a");
                a_22.MergeAttribute("style", "color: #2E2633; background-image: url(/content/images/4.png); border: 1px solid #E5C365;");
                a_22.InnerHtml = Mytrip_Mvc_Language.menu_change_password;
                div_gr_22.InnerHtml = a_22.ToString();
                result.AppendLine(div_gr_22.ToString());
            }
            return result.ToString();

        }
    }
}

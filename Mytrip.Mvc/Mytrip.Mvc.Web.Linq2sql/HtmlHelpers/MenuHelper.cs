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
        public static string Menu(this HtmlHelper html, string urla, bool news, bool artycles, bool blogs, bool captcha)
        {
            StringBuilder result = new StringBuilder();

            if (urla != "home")
            {
                TagBuilder div_gr_1 = new TagBuilder("div");
                div_gr_1.AddCssClass("menu_gr");
                TagBuilder a_1 = new TagBuilder("a");
                a_1.MergeAttribute("href", "/A/A");
                a_1.InnerHtml = Mytrip_Mvc_Language_1.menu_home;
                div_gr_1.InnerHtml = a_1.ToString();
                result.AppendLine(div_gr_1.ToString());
            }
            else
            {
                TagBuilder div_gr_1 = new TagBuilder("div");
                div_gr_1.AddCssClass("menu_gr1");
                TagBuilder a_1 = new TagBuilder("a");
                a_1.MergeAttribute("href", "/A/A");
                a_1.InnerHtml = Mytrip_Mvc_Language_1.menu_home;
                div_gr_1.InnerHtml = a_1.ToString();
                result.AppendLine(div_gr_1.ToString());
            }
            if (news == true)
            {

                if (urla != "news")
                {
                    TagBuilder div_gr_2 = new TagBuilder("div");
                    div_gr_2.AddCssClass("menu_gr");
                    TagBuilder a_2 = new TagBuilder("a");
                    a_2.MergeAttribute("href", "/C/A/1/10/News");
                    a_2.InnerHtml = Mytrip_Mvc_Language_1.menu_news;
                    div_gr_2.InnerHtml = a_2.ToString();
                    result.AppendLine(div_gr_2.ToString());
                }
                else
                {
                    TagBuilder div_gr_2 = new TagBuilder("div");
                    div_gr_2.AddCssClass("menu_gr1");
                    TagBuilder a_2 = new TagBuilder("a");
                    a_2.MergeAttribute("href", "/C/A/1/10/News");
                    a_2.InnerHtml = Mytrip_Mvc_Language_1.menu_news;
                    div_gr_2.InnerHtml = a_2.ToString();
                    result.AppendLine(div_gr_2.ToString());
                }
            }
            if (artycles == true)
            {

                if (urla != "artycles")
                {
                    TagBuilder div_gr_3 = new TagBuilder("div");
                    div_gr_3.AddCssClass("menu_gr");
                    TagBuilder a_3 = new TagBuilder("a");
                    a_3.MergeAttribute("href", "/C/A/1/10/Articles");
                    a_3.InnerHtml = Mytrip_Mvc_Language_1.menu_artycles;
                    div_gr_3.InnerHtml = a_3.ToString();
                    result.AppendLine(div_gr_3.ToString());
                }
                else
                {
                    TagBuilder div_gr_3 = new TagBuilder("div");
                    div_gr_3.AddCssClass("menu_gr1");
                    TagBuilder a_3 = new TagBuilder("a");
                    a_3.MergeAttribute("href", "/C/A/1/10/Articles");
                    a_3.InnerHtml = Mytrip_Mvc_Language_1.menu_artycles;
                    div_gr_3.InnerHtml = a_3.ToString();
                    result.AppendLine(div_gr_3.ToString());
                }
            }
            if (blogs == true)
            {
                if (urla != "blogs")
                {
                    TagBuilder div_gr_4 = new TagBuilder("div");
                    div_gr_4.AddCssClass("menu_gr");
                    TagBuilder a_4 = new TagBuilder("a");
                    a_4.MergeAttribute("href", "/C/A/1/10/Blogs");
                    a_4.InnerHtml = Mytrip_Mvc_Language_1.menu_blogs;
                    div_gr_4.InnerHtml = a_4.ToString();
                    result.AppendLine(div_gr_4.ToString());
                }
                else
                {
                    TagBuilder div_gr_4 = new TagBuilder("div");
                    div_gr_4.AddCssClass("menu_gr1");
                    TagBuilder a_4 = new TagBuilder("a");
                    a_4.MergeAttribute("href", "/C/A/1/10/Blogs");
                    a_4.InnerHtml = Mytrip_Mvc_Language_1.menu_blogs;
                    div_gr_4.InnerHtml = a_4.ToString();
                    result.AppendLine(div_gr_4.ToString());
                }
            }
            if (urla == "logon")
            {
                TagBuilder div_gr_5 = new TagBuilder("div");
                div_gr_5.AddCssClass("menu_gr1");
                TagBuilder a_5 = new TagBuilder("a");
                a_5.MergeAttribute("href", "/B/A");
                a_5.InnerHtml = Mytrip_Mvc_Language_1.menu_logon;
                div_gr_5.InnerHtml = a_5.ToString();
                result.AppendLine(div_gr_5.ToString());
            } if (urla == "registr")
            {

                if (captcha == false)
                {
                    TagBuilder div_gr_6 = new TagBuilder("div");
                    div_gr_6.AddCssClass("menu_gr1");
                    TagBuilder a_6 = new TagBuilder("a");
                    a_6.MergeAttribute("href", "/B/B");
                    a_6.InnerHtml = Mytrip_Mvc_Language_1.menu_register;
                    div_gr_6.InnerHtml = a_6.ToString();
                    result.AppendLine(div_gr_6.ToString());
                }
                else
                {
                    TagBuilder div_gr_6 = new TagBuilder("div");
                    div_gr_6.AddCssClass("menu_gr1");
                    TagBuilder a_6 = new TagBuilder("a");
                    a_6.MergeAttribute("href", "/B/H");
                    a_6.InnerHtml = Mytrip_Mvc_Language_1.menu_register;
                    div_gr_6.InnerHtml = a_6.ToString();
                    result.AppendLine(div_gr_6.ToString());
                }
            }
            if (urla == "tegs")
            {
                TagBuilder div_gr_7 = new TagBuilder("div");
                div_gr_7.AddCssClass("menu_gr1");
                TagBuilder a_7 = new TagBuilder("a");
                a_7.InnerHtml = Mytrip_Mvc_Language_1.menu_tegs;
                div_gr_7.InnerHtml = a_7.ToString();
                result.AppendLine(div_gr_7.ToString());

            } if (urla == "search")
            {
                TagBuilder div_gr_7 = new TagBuilder("div");
                div_gr_7.AddCssClass("menu_gr1");
                TagBuilder a_7 = new TagBuilder("a");
                a_7.InnerHtml = Mytrip_Mvc_Language_1.menu_search;
                div_gr_7.InnerHtml = a_7.ToString();
                result.AppendLine(div_gr_7.ToString());

            } if (urla == "thema_blog")
            {
                TagBuilder div_gr_8 = new TagBuilder("div");
                div_gr_8.AddCssClass("menu_gr1");
                TagBuilder a_8 = new TagBuilder("a");
                a_8.InnerHtml = Mytrip_Mvc_Language_1.menu_create_blog_theme;
                div_gr_8.InnerHtml = a_8.ToString();
                result.AppendLine(div_gr_8.ToString());
            } if (urla == "thema_news")
            {
                TagBuilder div_gr_8 = new TagBuilder("div");
                div_gr_8.AddCssClass("menu_gr1");
                TagBuilder a_8 = new TagBuilder("a");
                a_8.InnerHtml = Mytrip_Mvc_Language_2.menu_create_subheading_news;
                div_gr_8.InnerHtml = a_8.ToString();
                result.AppendLine(div_gr_8.ToString());
            }
            if (urla == "thema_article")
            {
                TagBuilder div_gr_8 = new TagBuilder("div");
                div_gr_8.AddCssClass("menu_gr1");
                TagBuilder a_8 = new TagBuilder("a");
                a_8.InnerHtml = Mytrip_Mvc_Language_2.menu_create_subheading_article;
                div_gr_8.InnerHtml = a_8.ToString();
                result.AppendLine(div_gr_8.ToString());
            }
            if (urla == "rub_artycle")
            {
                TagBuilder div_gr_9 = new TagBuilder("div");
                div_gr_9.AddCssClass("menu_gr1");
                TagBuilder a_9 = new TagBuilder("a");
                a_9.InnerHtml = Mytrip_Mvc_Language_2.menu_create_heading_article;
                div_gr_9.InnerHtml = a_9.ToString();
                result.AppendLine(div_gr_9.ToString());
            }
            if (urla == "rub_news")
            {
                TagBuilder div_gr_9 = new TagBuilder("div");
                div_gr_9.AddCssClass("menu_gr1");
                TagBuilder a_9 = new TagBuilder("a");
                a_9.InnerHtml = Mytrip_Mvc_Language_2.menu_create_heading_news;
                div_gr_9.InnerHtml = a_9.ToString();
                result.AppendLine(div_gr_9.ToString());
            }
            if (urla == "edit_rub_artycle")
            {
                TagBuilder div_gr_10 = new TagBuilder("div");
                div_gr_10.AddCssClass("menu_gr1");
                TagBuilder a_10 = new TagBuilder("a");
                a_10.InnerHtml = Mytrip_Mvc_Language_2.menu_edit_heading_article;
                div_gr_10.InnerHtml = a_10.ToString();
                result.AppendLine(div_gr_10.ToString());
            }
            if (urla == "edit_rub_news")
            {
                TagBuilder div_gr_10 = new TagBuilder("div");
                div_gr_10.AddCssClass("menu_gr1");
                TagBuilder a_10 = new TagBuilder("a");
                a_10.InnerHtml = Mytrip_Mvc_Language_2.menu_edit_heading_news;
                div_gr_10.InnerHtml = a_10.ToString();
                result.AppendLine(div_gr_10.ToString());
            }
            if (urla == "edit_thema_article")
            {
                TagBuilder div_gr_11 = new TagBuilder("div");
                div_gr_11.AddCssClass("menu_gr1");
                TagBuilder a_11 = new TagBuilder("a");
                a_11.InnerHtml = Mytrip_Mvc_Language_2.menu_edit_subheading_article;
                div_gr_11.InnerHtml = a_11.ToString();
                result.AppendLine(div_gr_11.ToString());
            }
            if (urla == "edit_thema_news")
            {
                TagBuilder div_gr_11 = new TagBuilder("div");
                div_gr_11.AddCssClass("menu_gr1");
                TagBuilder a_11 = new TagBuilder("a");
                a_11.InnerHtml = Mytrip_Mvc_Language_2.menu_edit_subheading_news;
                div_gr_11.InnerHtml = a_11.ToString();
                result.AppendLine(div_gr_11.ToString());
            }
            if (urla == "edit_thema_blog")
            {
                TagBuilder div_gr_11 = new TagBuilder("div");
                div_gr_11.AddCssClass("menu_gr1");
                TagBuilder a_11 = new TagBuilder("a");
                a_11.InnerHtml = Mytrip_Mvc_Language_2.menu_edit_heding_blog;
                div_gr_11.InnerHtml = a_11.ToString();
                result.AppendLine(div_gr_11.ToString());
            }
            if (urla == "edit_blog")
            {
                TagBuilder div_gr_12 = new TagBuilder("div");
                div_gr_12.AddCssClass("menu_gr1");
                TagBuilder a_12 = new TagBuilder("a");
                a_12.InnerHtml = Mytrip_Mvc_Language_1.menu_edit_blog;
                div_gr_12.InnerHtml = a_12.ToString();
                result.AppendLine(div_gr_12.ToString());
            } if (urla == "create_artycle")
            {
                TagBuilder div_gr_12 = new TagBuilder("div");
                div_gr_12.AddCssClass("menu_gr1");
                TagBuilder a_12 = new TagBuilder("a");
                a_12.InnerHtml = Mytrip_Mvc_Language_1.menu_write_article;
                div_gr_12.InnerHtml = a_12.ToString();
                result.AppendLine(div_gr_12.ToString());
            } if (urla == "edit_artycle")
            {
                TagBuilder div_gr_13 = new TagBuilder("div");
                div_gr_13.AddCssClass("menu_gr1");
                TagBuilder a_13 = new TagBuilder("a");
                a_13.InnerHtml = Mytrip_Mvc_Language_1.menu_edit_article;
                div_gr_13.InnerHtml = a_13.ToString();
                result.AppendLine(div_gr_13.ToString());
            } if (urla == "create_post")
            {
                TagBuilder div_gr_14 = new TagBuilder("div");
                div_gr_14.AddCssClass("menu_gr1");
                TagBuilder a_14 = new TagBuilder("a");
                a_14.InnerHtml = Mytrip_Mvc_Language_1.menu_write_post;
                div_gr_14.InnerHtml = a_14.ToString();
                result.AppendLine(div_gr_14.ToString());
            } if (urla == "edit_post")
            {
                TagBuilder div_gr_15 = new TagBuilder("div");
                div_gr_15.AddCssClass("menu_gr1");
                TagBuilder a_15 = new TagBuilder("a");
                a_15.InnerHtml = Mytrip_Mvc_Language_1.menu_edit_post;
                div_gr_15.InnerHtml = a_15.ToString();
                result.AppendLine(div_gr_15.ToString());
            } if (urla == "create_news")
            {
                TagBuilder div_gr_16 = new TagBuilder("div");
                div_gr_16.AddCssClass("menu_gr1");
                TagBuilder a_16 = new TagBuilder("a");
                a_16.InnerHtml = Mytrip_Mvc_Language_1.menu_write_news;
                div_gr_16.InnerHtml = a_16.ToString();
                result.AppendLine(div_gr_16.ToString());
            } if (urla == "edit_news")
            {
                TagBuilder div_gr_17 = new TagBuilder("div");
                div_gr_17.AddCssClass("menu_gr1");
                TagBuilder a_17 = new TagBuilder("a");
                a_17.InnerHtml = Mytrip_Mvc_Language_1.menu_edit_news;
                div_gr_17.InnerHtml = a_17.ToString();
                result.AppendLine(div_gr_17.ToString());
            } if (urla == "edit_comm")
            {
                TagBuilder div_gr_18 = new TagBuilder("div");
                div_gr_18.AddCssClass("menu_gr1");
                TagBuilder a_18 = new TagBuilder("a");
                a_18.InnerHtml = Mytrip_Mvc_Language_1.menu_edit_comment;
                div_gr_18.InnerHtml = a_18.ToString();
                result.AppendLine(div_gr_18.ToString());
            } if (urla == "file")
            {
                TagBuilder div_gr_19 = new TagBuilder("div");
                div_gr_19.AddCssClass("menu_gr1");
                TagBuilder a_19 = new TagBuilder("a");
                a_19.InnerHtml = Mytrip_Mvc_Language_1.menu_file_manager;
                div_gr_19.InnerHtml = a_19.ToString();
                result.AppendLine(div_gr_19.ToString());
            } if (urla == "user")
            {
                TagBuilder div_gr_20 = new TagBuilder("div");
                div_gr_20.AddCssClass("menu_gr1");
                TagBuilder a_20 = new TagBuilder("a");
                a_20.MergeAttribute("href", "/A/D/0/1/25/Users");
                a_20.InnerHtml = Mytrip_Mvc_Language_1.menu_management_users;
                div_gr_20.InnerHtml = a_20.ToString();
                result.AppendLine(div_gr_20.ToString());
            } if (urla == "site_setting")
            {
                TagBuilder div_gr_21 = new TagBuilder("div");
                div_gr_21.AddCssClass("menu_gr1");
                TagBuilder a_21 = new TagBuilder("a");
                a_21.MergeAttribute("href", "/A/C");
                a_21.InnerHtml = Mytrip_Mvc_Language_1.menu_site_adjustment;
                div_gr_21.InnerHtml = a_21.ToString();
                result.AppendLine(div_gr_21.ToString());
            }
            if (urla == "change_password")
            {
                TagBuilder div_gr_22 = new TagBuilder("div");
                div_gr_22.AddCssClass("menu_gr1");
                TagBuilder a_22 = new TagBuilder("a");
                a_22.InnerHtml = Mytrip_Mvc_Language_1.menu_change_password;
                div_gr_22.InnerHtml = a_22.ToString();
                result.AppendLine(div_gr_22.ToString());
            }
            return result.ToString();

        }
    }
}

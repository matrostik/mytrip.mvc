using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using Mytrip.Mvc.Language;

namespace Mytrip.Mvc.Web.Linq2sql.HtmlHelpers
{
    public static class AdminHelper
    {
        public static string EditeAndDeliteArtycle(this HtmlHelper html, bool blog, bool news, int a, int b, string addedby, string addedbycategory)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder img_1 = new TagBuilder("img");
            TagBuilder img_2 = new TagBuilder("img");
            img_1.MergeAttribute("src", "/content/images/edit.png");
            img_2.MergeAttribute("src", "/content/images/delete.png");
            img_1.MergeAttribute("alt", Mytrip_Mvc_Language.edit);
            img_2.MergeAttribute("alt", Mytrip_Mvc_Language.delete);
            img_1.MergeAttribute("style", "border-width: 0px;");
            img_2.MergeAttribute("style", "border-width: 0px;");
            if (blog == true)
            {
                if (HttpContext.Current.User.IsInRole("blogger"))
                {
                    if (addedby == HttpContext.Current.User.Identity.Name)
                    {
                        TagBuilder a_1 = new TagBuilder("a");
                        TagBuilder a_2 = new TagBuilder("a");
                        a_1.MergeAttribute("href", "/C/ZK/" + a + "/Edit_post");
                        a_2.MergeAttribute("href", "/C/ZM/" + a);
                        a_2.MergeAttribute("onclick", "return confirm ('"+Mytrip_Mvc_Language.delete_post+"');");
                        a_1.InnerHtml = img_1.ToString();
                        a_2.InnerHtml = img_2.ToString();
                        result.AppendLine(a_1.ToString());
                        result.AppendLine(a_2.ToString());

                    } if (HttpContext.Current.User.IsInRole("chief_editor"))
                    {
                        if (addedby != HttpContext.Current.User.Identity.Name)
                        {
                            TagBuilder a_1 = new TagBuilder("a");
                            TagBuilder a_2 = new TagBuilder("a");
                            a_1.MergeAttribute("href", "/C/ZK/" + a + "/Edit_post");
                            a_2.MergeAttribute("href", "/C/ZM/" + a);
                            a_2.MergeAttribute("onclick", "return confirm ('" + Mytrip_Mvc_Language.delete_post + "');");
                            a_1.InnerHtml = img_1.ToString();
                            a_2.InnerHtml = img_2.ToString();
                            result.AppendLine(a_1.ToString());
                            result.AppendLine(a_2.ToString());

                        }
                    }
                }
            }
            else
            {
                if (news == false)
                {
                    if (HttpContext.Current.User.IsInRole("artycle_editor"))
                    {
                        if (addedby == HttpContext.Current.User.Identity.Name)
                        {

                            TagBuilder a_1 = new TagBuilder("a");
                            TagBuilder a_2 = new TagBuilder("a");
                            a_1.MergeAttribute("href", "/C/ZK/" + a + "/Edit_article");
                            a_2.MergeAttribute("href", "/C/ZL/" + a);
                            a_2.MergeAttribute("onclick", "return confirm ('"+Mytrip_Mvc_Language.delete_article+"');");
                            a_1.InnerHtml = img_1.ToString();
                            a_2.InnerHtml = img_2.ToString();
                            result.AppendLine(a_1.ToString());
                            result.AppendLine(a_2.ToString());

                        }
                    }
                    if (HttpContext.Current.User.IsInRole("chief_editor"))
                    {
                        TagBuilder a_1 = new TagBuilder("a");
                        TagBuilder a_2 = new TagBuilder("a");
                        a_1.MergeAttribute("href", "/C/ZK/" + a + "/Edit_article");
                        a_2.MergeAttribute("href", "/C/ZL/" + a);
                        a_2.MergeAttribute("onclick", "return confirm ('" + Mytrip_Mvc_Language.delete_article + "');");
                        a_1.InnerHtml = img_1.ToString();
                        a_2.InnerHtml = img_2.ToString();
                        result.AppendLine(a_1.ToString());
                        result.AppendLine(a_2.ToString());
                    }
                }
                else
                {
                    if (HttpContext.Current.User.IsInRole("artycle_editor"))
                    {
                        if (addedbycategory == HttpContext.Current.User.Identity.Name)
                        {
                            TagBuilder a_1 = new TagBuilder("a");
                            TagBuilder a_2 = new TagBuilder("a");
                            a_1.MergeAttribute("href", "/C/ZK/" + a + "/Edit_news");
                            a_2.MergeAttribute("href", "/C/ZL/" + a);
                            a_2.MergeAttribute("onclick", "return confirm ('"+Mytrip_Mvc_Language.delete_news+"');");
                            a_1.InnerHtml = img_1.ToString();
                            a_2.InnerHtml = img_2.ToString();
                            result.AppendLine(a_1.ToString());
                            result.AppendLine(a_2.ToString());
                        }
                    } if (HttpContext.Current.User.IsInRole("chief_editor"))
                    {
                        TagBuilder a_1 = new TagBuilder("a");
                        TagBuilder a_2 = new TagBuilder("a");
                        a_1.MergeAttribute("href", "/C/ZK/" + a + "/Edit_news");
                        a_2.MergeAttribute("href", "/C/ZL/" + a);
                        a_2.MergeAttribute("onclick", "return confirm ('" + Mytrip_Mvc_Language.delete_news + "');");
                        a_1.InnerHtml = img_1.ToString();
                        a_2.InnerHtml = img_2.ToString();
                        result.AppendLine(a_1.ToString());
                        result.AppendLine(a_2.ToString());
                    }
                }

            }
            return result.ToString();
        }
        public static string CreateCategory(this HtmlHelper html, bool news)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder img = new TagBuilder("img");
            img.MergeAttribute("src", "/content/images/create.png");           
            img.MergeAttribute("alt", Mytrip_Mvc_Language.create);           
            img.MergeAttribute("style", "border-width: 0px;");
            string b;
            if (HttpContext.Current.User.IsInRole("artycle_editor"))
            {
                TagBuilder a = new TagBuilder("a");
                if (news == false)
                {
                    a.MergeAttribute("href", "/C/ZA/0/Create_article_heding");
                    b = Mytrip_Mvc_Language.article_heading;
                }
                else
                {
                    a.MergeAttribute("href", "/C/ZA/0/Create_news_heding");
                    b = Mytrip_Mvc_Language.news_heading;
                }
                a.InnerHtml = img.ToString();
                result.AppendLine(b);
                result.AppendLine(a.ToString());
               
            } if (HttpContext.Current.User.IsInRole("chief_editor"))
            {
                TagBuilder a = new TagBuilder("a");
                if (news == false)
                {
                    a.MergeAttribute("href", "/C/ZA/0/Create_article_heding");
                    b = Mytrip_Mvc_Language.article_heading;
                }
                else
                {
                    a.MergeAttribute("href", "/C/ZA/0/Create_news_heding");
                    b = Mytrip_Mvc_Language.news_heading;
                }
                a.InnerHtml = img.ToString();
                result.AppendLine(b);
                result.AppendLine(a.ToString());
            }
            string br = "<br/>";
            result.AppendLine(br);
            return result.ToString();
        }
        public static string CreateCategory(this HtmlHelper html, bool news, bool blog, int categoryid)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder img = new TagBuilder("img");
            img.MergeAttribute("src", "/content/images/create.png");
            img.MergeAttribute("alt", Mytrip_Mvc_Language.create);
            img.MergeAttribute("style", "border-width: 0px;");
            string b;
            if (categoryid == 0)
            {
                if (blog == false)
                {
                    if (HttpContext.Current.User.IsInRole("artycle_editor"))
                    {
                        TagBuilder a = new TagBuilder("a");
                        if (news == false)
                        {
                            a.MergeAttribute("href", "/C/ZA/0/Create_article_heding");
                            b = Mytrip_Mvc_Language.article_heading;
                        }
                        else
                        {
                            a.MergeAttribute("href", "/C/ZA/0/Create_news_heding");
                            b = Mytrip_Mvc_Language.news_heading;
                        }
                        a.InnerHtml = img.ToString();
                        result.AppendLine(b);
                        result.AppendLine(a.ToString());

                    } if (HttpContext.Current.User.IsInRole("chief_editor"))
                    {
                        TagBuilder a = new TagBuilder("a");
                        if (news == false)
                        {
                            a.MergeAttribute("href", "/C/ZA/0/Create_article_heding");
                            b = Mytrip_Mvc_Language.article_heading;
                        }
                        else
                        {
                            a.MergeAttribute("href", "/C/ZA/0/Create_news_heding");
                            b = Mytrip_Mvc_Language.news_heading;
                        }
                        a.InnerHtml = img.ToString();
                        result.AppendLine(b);
                        result.AppendLine(a.ToString());
                    }
                }
            }            
            return result.ToString();
        }
        public static string CreateReCategory(this HtmlHelper html, bool news, bool blog, int c, int categoryid, string addedby)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder img = new TagBuilder("img");
            img.MergeAttribute("src", "/content/images/create.png");
            img.MergeAttribute("alt", Mytrip_Mvc_Language.create);
            img.MergeAttribute("style", "border-width: 0px;");
            string b;
            if (categoryid == 0)
            {
                if (blog == false)
                {
                    if (HttpContext.Current.User.IsInRole("artycle_editor"))
                    {
                        TagBuilder a = new TagBuilder("a");
                        if (news == false)
                        {
                            a.MergeAttribute("href", "/C/ZA/" + c + "/Create_article_subheding");                        
                        }
                        else
                        {
                            a.MergeAttribute("href", "/C/ZA/" + c + "/Create_news_subheding");
                        }
                        b = Mytrip_Mvc_Language.article_subheading;  
                        a.InnerHtml = img.ToString();
                        result.AppendLine(b);
                        result.AppendLine(a.ToString());

                    } if (HttpContext.Current.User.IsInRole("chief_editor"))
                    {
                        TagBuilder a = new TagBuilder("a");
                        if (news == false)
                        {
                            a.MergeAttribute("href", "/C/ZA/" + c + "/Create_article_subheding");
                        }
                        else
                        {
                            a.MergeAttribute("href", "/C/ZA/" + c + "/Create_news_subheding");

                        }
                        b = Mytrip_Mvc_Language.article_subheading;  
                        a.InnerHtml = img.ToString();
                        result.AppendLine(b);
                        result.AppendLine(a.ToString());
                    }
                }
                else
                {
                    if (HttpContext.Current.User.IsInRole("blogger"))
                    {
                        if (addedby == HttpContext.Current.User.Identity.Name)
                        {
                            TagBuilder a = new TagBuilder("a");
                            a.MergeAttribute("href", "/C/ZA/" + c + "/Create_blog_heading");
                            b = Mytrip_Mvc_Language.blog_heading;
                            a.InnerHtml = img.ToString();
                            result.AppendLine(b);
                            result.AppendLine(a.ToString());
                        
                        }
                    }
                }
            }
            string br = "<br/>";
            result.AppendLine(br);
            return result.ToString();
        }
        public static string CreateArtycle(this HtmlHelper html, int c, bool news)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder img = new TagBuilder("img");
            img.MergeAttribute("src", "/content/images/create.png");
            img.MergeAttribute("alt", Mytrip_Mvc_Language.create);
            img.MergeAttribute("style", "border-width: 0px;");
            string b;
            if (HttpContext.Current.User.IsInRole("artycle_editor"))
            {
                 if (c > 0)
                {
                    TagBuilder a = new TagBuilder("a");
                    if (news == false)
                    {
                        a.MergeAttribute("href", "/C/ZJ/0/Create_article");
                        b = Mytrip_Mvc_Language.article;
                    }
                    else
                    {
                        a.MergeAttribute("href", "/C/ZJ/0/Create_news");
                        b = Mytrip_Mvc_Language.news;
                    }
                    a.InnerHtml = img.ToString();
                    result.AppendLine(b);
                    result.AppendLine(a.ToString());
                }
            } if (HttpContext.Current.User.IsInRole("chief_editor"))
            {
                 if (c > 0)
                {
                    TagBuilder a = new TagBuilder("a");
                    if (news == false)
                    {
                        a.MergeAttribute("href", "/C/ZJ/0/Create_article");
                        b = Mytrip_Mvc_Language.article;
                    }
                    else
                    {
                        a.MergeAttribute("href", "/C/ZJ/0/Create_news");
                        b = Mytrip_Mvc_Language.news;
                    }
                    a.InnerHtml = img.ToString();
                    result.AppendLine(b);
                    result.AppendLine(a.ToString());
                   
                       
                    
                }
            }
            string br = "<br/>";
            result.AppendLine(br);
            return result.ToString();
        }
        public static string CreateArtycle(this HtmlHelper html, int c, bool news, bool blog, string addedby)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder img = new TagBuilder("img");
            img.MergeAttribute("src", "/content/images/create.png");
            img.MergeAttribute("alt", Mytrip_Mvc_Language.create);
            img.MergeAttribute("style", "border-width: 0px;");
            string b;
            if (blog == false)
            {
                if (HttpContext.Current.User.IsInRole("artycle_editor"))
                {
                    TagBuilder a = new TagBuilder("a");
                    if (news == false)
                    {
                        a.MergeAttribute("href", "/C/ZJ/" + c + "/Create_article");
                        b = Mytrip_Mvc_Language.article;
                    }
                    else
                    {
                        a.MergeAttribute("href", "/C/ZJ/" + c + "/Create_news");
                        b = Mytrip_Mvc_Language.news;
                    }
                    a.InnerHtml = img.ToString();
                    result.AppendLine(b);
                    result.AppendLine(a.ToString());

                } if (HttpContext.Current.User.IsInRole("chief_editor"))
                {
                    TagBuilder a = new TagBuilder("a");
                    if (news == false)
                    {
                        a.MergeAttribute("href", "/C/ZJ/" + c + "/Create_article");
                        b = Mytrip_Mvc_Language.article;
                    }
                    else
                    {
                        a.MergeAttribute("href", "/C/ZJ/" + c + "/Create_news");
                        b = Mytrip_Mvc_Language.news;
                    }
                    a.InnerHtml = img.ToString();
                    result.AppendLine(b);
                    result.AppendLine(a.ToString());

                }
            }
            else {
                if (HttpContext.Current.User.IsInRole("blogger"))
                {
                    if (addedby == HttpContext.Current.User.Identity.Name)
                    {
                        TagBuilder a = new TagBuilder("a");
                        a.MergeAttribute("href", "/C/ZJ/" + c + "/Create_post");
                        b = Mytrip_Mvc_Language.post;
                        a.InnerHtml = img.ToString();
                        result.AppendLine(b);
                        result.AppendLine(a.ToString());
                    }
                }
            
            }
            string br = "<br/>";
            result.AppendLine(br);
            return result.ToString();
        }
        public static string EditeAndDeliteCategory(this HtmlHelper html, bool blog, bool news, int a, int categoryid, string addedby, string addedbycategory)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder img_1 = new TagBuilder("img");
            TagBuilder img_2 = new TagBuilder("img");
            img_1.MergeAttribute("src", "/content/images/edit.png");
            img_2.MergeAttribute("src", "/content/images/delete.png");
            img_1.MergeAttribute("alt", Mytrip_Mvc_Language.edit);
            img_2.MergeAttribute("alt", Mytrip_Mvc_Language.delete);
            img_1.MergeAttribute("style", "border-width: 0px;");
            img_2.MergeAttribute("style", "border-width: 0px;");
            if (blog == true)
            {
                if (HttpContext.Current.User.IsInRole("blogger"))
                {
                    if (addedby == HttpContext.Current.User.Identity.Name)
                    {
                        TagBuilder a_1 = new TagBuilder("a");
                        TagBuilder a_2 = new TagBuilder("a");
                        if (categoryid != 0)
                        {
                            a_1.MergeAttribute("href", "/C/ZB/" + a + "/Edit_blog_heding");
                        }
                        else { a_1.MergeAttribute("href", "/C/ZB/" + a + "/Edit_blog"); }
                        a_2.MergeAttribute("href", "/C/ZI/" + a);
                        a_2.MergeAttribute("onclick", "return confirm ('"+Mytrip_Mvc_Language.delete_blog+"');");
                        a_1.InnerHtml = img_1.ToString();
                        a_2.InnerHtml = img_2.ToString();
                        result.AppendLine(a_1.ToString());
                        result.AppendLine(a_2.ToString());
               
                    }
                } if (HttpContext.Current.User.IsInRole("chief_editor"))
                    {
                        if (addedby != HttpContext.Current.User.Identity.Name)
                        {
                            TagBuilder a_1 = new TagBuilder("a");
                            TagBuilder a_2 = new TagBuilder("a");
                            if (categoryid != 0)
                            {
                                a_1.MergeAttribute("href", "/C/ZB/" + a + "/Edit_blog_heding");
                            }
                            else { a_1.MergeAttribute("href", "/C/ZB/" + a + "/Edit_blog"); }
                            a_2.MergeAttribute("href", "/C/ZI/" + a);
                            a_2.MergeAttribute("onclick", "return confirm ('" + Mytrip_Mvc_Language.delete_blog + "');");
                            a_1.InnerHtml = img_1.ToString();
                            a_2.InnerHtml = img_2.ToString();
                            result.AppendLine(a_1.ToString());
                            result.AppendLine(a_2.ToString());

                        }
                    }
               
            }
            else
            {
                if (news == true)
                {
                    if (HttpContext.Current.User.IsInRole("artycle_editor"))
                    {
                        if (addedby == HttpContext.Current.User.Identity.Name)
                        {

                            TagBuilder a_1 = new TagBuilder("a");
                            TagBuilder a_2 = new TagBuilder("a");
                            if (categoryid == 0)
                            {
                                a_1.MergeAttribute("href", "/C/ZB/" + a + "/Edit_news_heding");
                            }
                            else
                            {
                                a_1.MergeAttribute("href", "/C/ZB/" + a + "/Edit_news_subheding");
                                string b = Mytrip_Mvc_Language.article_subheading;
                                result.AppendLine(b);
                            }
                            a_2.MergeAttribute("href", "/C/ZH/" + a);
                            a_2.MergeAttribute("onclick", "return confirm ('" + Mytrip_Mvc_Language.delete_heading + "');");
                            a_1.InnerHtml = img_1.ToString();
                            a_2.InnerHtml = img_2.ToString();
                            result.AppendLine(a_1.ToString());
                            result.AppendLine(a_2.ToString());

                        }
                        if (addedby != HttpContext.Current.User.Identity.Name)
                        {
                            if (addedbycategory == HttpContext.Current.User.Identity.Name)
                            {
                                if (categoryid != 0)
                                {
                                    TagBuilder a_1 = new TagBuilder("a");
                                    TagBuilder a_2 = new TagBuilder("a");
                                    a_1.MergeAttribute("href", "/C/ZB/" + a + "/Edit_news_subheding");
                                    string b = Mytrip_Mvc_Language.article_subheading;
                                    result.AppendLine(b);
                                    a_2.MergeAttribute("href", "/C/ZH/" + a);
                                    a_2.MergeAttribute("onclick", "return confirm ('" + Mytrip_Mvc_Language.delete_heading + "');");
                                    a_1.InnerHtml = img_1.ToString();
                                    a_2.InnerHtml = img_2.ToString();
                                    result.AppendLine(a_1.ToString());
                                    result.AppendLine(a_2.ToString());

                                }
                            }
                        }
                    }
                    if (HttpContext.Current.User.IsInRole("chief_editor"))
                    {
                        TagBuilder a_1 = new TagBuilder("a");
                        TagBuilder a_2 = new TagBuilder("a");
                        if (categoryid == 0)
                        {
                            a_1.MergeAttribute("href", "/C/ZB/" + a + "/Edit_news_heding");
                        }
                        else
                        {
                            a_1.MergeAttribute("href", "/C/ZB/" + a + "/Edit_news_subheding");
                            string b = Mytrip_Mvc_Language.article_subheading;
                            result.AppendLine(b);
                        }
                        a_2.MergeAttribute("href", "/C/ZH/" + a);
                        a_2.MergeAttribute("onclick", "return confirm ('" + Mytrip_Mvc_Language.delete_heading + "');");
                        a_1.InnerHtml = img_1.ToString();
                        a_2.InnerHtml = img_2.ToString();
                        result.AppendLine(a_1.ToString());
                        result.AppendLine(a_2.ToString());
                    }
                }
                else
                {
                    if (HttpContext.Current.User.IsInRole("artycle_editor"))
                    {
                        if (addedby == HttpContext.Current.User.Identity.Name)
                        {

                            TagBuilder a_1 = new TagBuilder("a");
                            TagBuilder a_2 = new TagBuilder("a");
                            if (categoryid == 0)
                            {
                                a_1.MergeAttribute("href", "/C/ZB/" + a + "/Edit_article_heding");
                            }
                            else
                            {
                                a_1.MergeAttribute("href", "/C/ZB/" + a + "/Edit_article_subheding");
                                string b = Mytrip_Mvc_Language.article_subheading;
                                result.AppendLine(b);
                            }
                            a_2.MergeAttribute("href", "/C/ZH/" + a);
                            a_2.MergeAttribute("onclick", "return confirm ('" + Mytrip_Mvc_Language.delete_heading + "');");
                            a_1.InnerHtml = img_1.ToString();
                            a_2.InnerHtml = img_2.ToString();
                            result.AppendLine(a_1.ToString());
                            result.AppendLine(a_2.ToString());

                        }
                        if (addedby != HttpContext.Current.User.Identity.Name)
                        {
                            if (addedbycategory == HttpContext.Current.User.Identity.Name)
                            {
                                if (categoryid != 0)
                                {
                                    TagBuilder a_1 = new TagBuilder("a");
                                    TagBuilder a_2 = new TagBuilder("a");
                                    a_1.MergeAttribute("href", "/C/ZB/" + a + "/Edit_article_subheding");
                                    string b = Mytrip_Mvc_Language.article_subheading;
                                    result.AppendLine(b);
                                    a_2.MergeAttribute("href", "/C/ZH/" + a);
                                    a_2.MergeAttribute("onclick", "return confirm ('" + Mytrip_Mvc_Language.delete_heading + "');");
                                    a_1.InnerHtml = img_1.ToString();
                                    a_2.InnerHtml = img_2.ToString();
                                    result.AppendLine(a_1.ToString());
                                    result.AppendLine(a_2.ToString());

                                }
                            }
                        }
                    }
                    if (HttpContext.Current.User.IsInRole("chief_editor"))
                    {
                        TagBuilder a_1 = new TagBuilder("a");
                        TagBuilder a_2 = new TagBuilder("a");
                        if (categoryid == 0)
                        {
                            a_1.MergeAttribute("href", "/C/ZB/" + a + "/Edit_article_heding");
                        }
                        else
                        {
                            a_1.MergeAttribute("href", "/C/ZB/" + a + "/Edit_article_subheding");
                            string b = Mytrip_Mvc_Language.article_subheading;
                            result.AppendLine(b);
                        }
                        a_2.MergeAttribute("href", "/C/ZH/" + a);
                        a_2.MergeAttribute("onclick", "return confirm ('" + Mytrip_Mvc_Language.delete_heading + "');");
                        a_1.InnerHtml = img_1.ToString();
                        a_2.InnerHtml = img_2.ToString();
                        result.AppendLine(a_1.ToString());
                        result.AppendLine(a_2.ToString());
                    }
                }               

            }
            string br = "<br/>";
            result.AppendLine(br);
            return result.ToString();
        }
         public static string EditeAndDeliteCategory(this HtmlHelper html, bool blog,bool news, int a, int categoryid, string addedby)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder img_1 = new TagBuilder("img");
            TagBuilder img_2 = new TagBuilder("img");
            img_1.MergeAttribute("src", "/content/images/edit.png");
            img_2.MergeAttribute("src", "/content/images/delete.png");
            img_1.MergeAttribute("alt", Mytrip_Mvc_Language.edit);
            img_2.MergeAttribute("alt", Mytrip_Mvc_Language.delete);
            img_1.MergeAttribute("style", "border-width: 0px;");
            img_2.MergeAttribute("style", "border-width: 0px;");
            if (blog == true)
            {
                if (HttpContext.Current.User.IsInRole("blogger"))
                {
                    if (addedby == HttpContext.Current.User.Identity.Name)
                    {
                        TagBuilder a_1 = new TagBuilder("a");
                        TagBuilder a_2 = new TagBuilder("a");
                        if (categoryid != 0)
                        {
                            a_1.MergeAttribute("href", "/C/ZB/" + a + "/Edit_blog_heding");
                        }
                        else { a_1.MergeAttribute("href", "/C/ZB/" + a + "/Edit_blog"); }
                        a_2.MergeAttribute("href", "/C/ZI/" + a);
                        a_2.MergeAttribute("onclick", "return confirm ('"+Mytrip_Mvc_Language.delete_blog+"');");
                        a_1.InnerHtml = img_1.ToString();
                        a_2.InnerHtml = img_2.ToString();
                        result.AppendLine(a_1.ToString());
                        result.AppendLine(a_2.ToString());
               
                    }
                } if (HttpContext.Current.User.IsInRole("chief_editor"))
                    {
                        if (addedby != HttpContext.Current.User.Identity.Name)
                        {
                            TagBuilder a_1 = new TagBuilder("a");
                            TagBuilder a_2 = new TagBuilder("a");
                            if (categoryid != 0)
                            {
                                a_1.MergeAttribute("href", "/C/ZB/" + a + "/Edit_blog_heding");
                            }
                            else { a_1.MergeAttribute("href", "/C/ZB/" + a + "/Edit_blog"); }
                            a_2.MergeAttribute("href", "/C/ZI/" + a);
                            a_2.MergeAttribute("onclick", "return confirm ('" + Mytrip_Mvc_Language.delete_blog + "');");
                            a_1.InnerHtml = img_1.ToString();
                            a_2.InnerHtml = img_2.ToString();
                            result.AppendLine(a_1.ToString());
                            result.AppendLine(a_2.ToString());

                        }
                    }
               
            }
            else
            {
                if (news == true)
                {
                    if (HttpContext.Current.User.IsInRole("artycle_editor"))
                    {
                        if (addedby == HttpContext.Current.User.Identity.Name)
                        {

                            TagBuilder a_1 = new TagBuilder("a");
                            TagBuilder a_2 = new TagBuilder("a");
                            if (categoryid == 0)
                            {
                                a_1.MergeAttribute("href", "/C/ZB/" + a + "/Edit_news_heding");
                            }
                            else
                            {
                                a_1.MergeAttribute("href", "/C/ZB/" + a + "/Edit_news_subheding");
                                string b = Mytrip_Mvc_Language.article_subheading;
                                result.AppendLine(b);
                            }
                            a_2.MergeAttribute("href", "/C/ZH/" + a);
                            a_2.MergeAttribute("onclick", "return confirm ('" + Mytrip_Mvc_Language.delete_heading + "');");
                            a_1.InnerHtml = img_1.ToString();
                            a_2.InnerHtml = img_2.ToString();
                            result.AppendLine(a_1.ToString());
                            result.AppendLine(a_2.ToString());

                        }

                    }
                    if (HttpContext.Current.User.IsInRole("chief_editor"))
                    {
                        TagBuilder a_1 = new TagBuilder("a");
                        TagBuilder a_2 = new TagBuilder("a");
                        if (categoryid == 0)
                        {
                            a_1.MergeAttribute("href", "/C/ZB/" + a + "/Edit_news_heding");
                        }
                        else
                        {
                            a_1.MergeAttribute("href", "/C/ZB/" + a + "/Edit_news_subheding");
                            string b = Mytrip_Mvc_Language.article_subheading;
                            result.AppendLine(b);
                        }
                        a_2.MergeAttribute("href", "/C/ZH/" + a);
                        a_2.MergeAttribute("onclick", "return confirm ('" + Mytrip_Mvc_Language.delete_heading + "');");
                        a_1.InnerHtml = img_1.ToString();
                        a_2.InnerHtml = img_2.ToString();
                        result.AppendLine(a_1.ToString());
                        result.AppendLine(a_2.ToString());
                    }
                }
                else
                {
                    if (HttpContext.Current.User.IsInRole("artycle_editor"))
                    {
                        if (addedby == HttpContext.Current.User.Identity.Name)
                        {

                            TagBuilder a_1 = new TagBuilder("a");
                            TagBuilder a_2 = new TagBuilder("a");
                            if (categoryid == 0)
                            {
                                a_1.MergeAttribute("href", "/C/ZB/" + a + "/Edit_article_heding");
                            }
                            else
                            {
                                a_1.MergeAttribute("href", "/C/ZB/" + a + "/Edit_article_subheding");
                                string b = Mytrip_Mvc_Language.article_subheading;
                                result.AppendLine(b);
                            }
                            a_2.MergeAttribute("href", "/C/ZH/" + a);
                            a_2.MergeAttribute("onclick", "return confirm ('" + Mytrip_Mvc_Language.delete_heading + "');");
                            a_1.InnerHtml = img_1.ToString();
                            a_2.InnerHtml = img_2.ToString();
                            result.AppendLine(a_1.ToString());
                            result.AppendLine(a_2.ToString());

                        }

                    }
                    if (HttpContext.Current.User.IsInRole("chief_editor"))
                    {
                        TagBuilder a_1 = new TagBuilder("a");
                        TagBuilder a_2 = new TagBuilder("a");
                        if (categoryid == 0)
                        {
                            a_1.MergeAttribute("href", "/C/ZB/" + a + "/Edit_article_heding");
                        }
                        else
                        {
                            a_1.MergeAttribute("href", "/C/ZB/" + a + "/Edit_article_subheding");
                            string b = Mytrip_Mvc_Language.article_subheading;
                            result.AppendLine(b);
                        }
                        a_2.MergeAttribute("href", "/C/ZH/" + a);
                        a_2.MergeAttribute("onclick", "return confirm ('" + Mytrip_Mvc_Language.delete_heading + "');");
                        a_1.InnerHtml = img_1.ToString();
                        a_2.InnerHtml = img_2.ToString();
                        result.AppendLine(a_1.ToString());
                        result.AppendLine(a_2.ToString());
                    }
                }              

            }
            string br = "<br/>";
            result.AppendLine(br);
            return result.ToString();
        }
    
    }

}

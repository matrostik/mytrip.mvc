using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

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
            img_1.MergeAttribute("alt", "правка");
            img_2.MergeAttribute("alt", "удалить");
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
                        a_1.MergeAttribute("href", "/C/ZT/" + a);
                        a_2.MergeAttribute("href", "/C/ZM/" + a);
                        a_2.MergeAttribute("onclick", "return confirm ('Вы уверены что хотите удалить пост?');");
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
                            a_1.MergeAttribute("href", "/C/ZT/" + a);
                            a_2.MergeAttribute("href", "/C/ZM/" + a);
                            a_2.MergeAttribute("onclick", "return confirm ('Вы уверены что хотите удалить пост?');");
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
                            a_1.MergeAttribute("href", "/C/ZK/" + a + "/" + b);
                            a_2.MergeAttribute("href", "/C/ZL/" + a);
                            a_2.MergeAttribute("onclick", "return confirm ('Вы уверены что хотите удалить статью?');");
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
                        a_1.MergeAttribute("href", "/C/ZK/" + a + "/" + b);
                        a_2.MergeAttribute("href", "/C/ZL/" + a);
                        a_2.MergeAttribute("onclick", "return confirm ('Вы уверены что хотите удалить статью?');");
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
                            a_1.MergeAttribute("href", "/C/ZX/" + a + "/" + b);
                            a_2.MergeAttribute("href", "/C/ZL/" + a);
                            a_2.MergeAttribute("onclick", "return confirm ('Вы уверены что хотите удалить новость?');");
                            a_1.InnerHtml = img_1.ToString();
                            a_2.InnerHtml = img_2.ToString();
                            result.AppendLine(a_1.ToString());
                            result.AppendLine(a_2.ToString());
                        }
                    } if (HttpContext.Current.User.IsInRole("chief_editor"))
                    {
                        TagBuilder a_1 = new TagBuilder("a");
                        TagBuilder a_2 = new TagBuilder("a");
                        a_1.MergeAttribute("href", "/C/ZX/" + a + "/" + b);
                        a_2.MergeAttribute("href", "/C/ZL/" + a);
                        a_2.MergeAttribute("onclick", "return confirm ('Вы уверены что хотите удалить новость?');");
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
            img.MergeAttribute("alt", "создать");           
            img.MergeAttribute("style", "border-width: 0px;");
            string b;
            if (HttpContext.Current.User.IsInRole("artycle_editor"))
            {
                TagBuilder a = new TagBuilder("a");
                if (news == false)
                {
                    a.MergeAttribute("href", "/C/ZA");
                    b = "рубрика: ";
                }
                else
                {
                    a.MergeAttribute("href", "/C/ZE");
                    b = "рубрика новостей: ";
                }
                a.InnerHtml = img.ToString();
                result.AppendLine(b);
                result.AppendLine(a.ToString());
               
            } if (HttpContext.Current.User.IsInRole("chief_editor"))
            {
                TagBuilder a = new TagBuilder("a");
                if (news == false)
                {
                    a.MergeAttribute("href", "/C/ZA");
                    b = "рубрика: ";
                }
                else
                {
                    a.MergeAttribute("href", "/C/ZE");
                    b = "рубрика новостей: ";
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
            img.MergeAttribute("alt", "создать");
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
                            a.MergeAttribute("href", "/C/ZA");
                            b = "рубрика: ";
                        }
                        else
                        {
                            a.MergeAttribute("href", "/C/ZE");
                            b = "рубрика новостей: ";
                        }
                        a.InnerHtml = img.ToString();
                        result.AppendLine(b);
                        result.AppendLine(a.ToString());

                    } if (HttpContext.Current.User.IsInRole("chief_editor"))
                    {
                        TagBuilder a = new TagBuilder("a");
                        if (news == false)
                        {
                            a.MergeAttribute("href", "/C/ZA");
                            b = "рубрика: ";
                        }
                        else
                        {
                            a.MergeAttribute("href", "/C/ZE");
                            b = "рубрика новостей: ";
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
            img.MergeAttribute("alt", "создать");
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
                            a.MergeAttribute("href", "/C/ZC/"+c);                        
                        }
                        else
                        {
                            a.MergeAttribute("href", "/C/YZ/"+c);
                        }
                        b = "подрубрика: ";  
                        a.InnerHtml = img.ToString();
                        result.AppendLine(b);
                        result.AppendLine(a.ToString());

                    } if (HttpContext.Current.User.IsInRole("chief_editor"))
                    {
                        TagBuilder a = new TagBuilder("a");
                        if (news == false)
                        {
                            a.MergeAttribute("href", "/C/ZC/" + c);
                        }
                        else
                        {
                            a.MergeAttribute("href", "/C/YZ/" + c);

                        }
                        b = "подрубрика: ";  
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
                            a.MergeAttribute("href", "/C/YY/" + c);
                            b = "тема блога: ";
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
            img.MergeAttribute("alt", "создать");
            img.MergeAttribute("style", "border-width: 0px;");
            string b;
            if (HttpContext.Current.User.IsInRole("artycle_editor"))
            {
                 if (c > 0)
                {
                    TagBuilder a = new TagBuilder("a");
                    if (news == false)
                    {
                        a.MergeAttribute("href", "/C/ZJ/0");
                        b = "статья: ";
                    }
                    else
                    {
                        a.MergeAttribute("href", "/C/ZW/0");
                        b = "новость: ";
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
                        a.MergeAttribute("href", "/C/ZJ/0");
                        b = "статья: ";
                    }
                    else
                    {
                        a.MergeAttribute("href", "/C/ZW/0");
                        b = "новость: ";
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
            img.MergeAttribute("alt", "создать");
            img.MergeAttribute("style", "border-width: 0px;");
            string b;
            if (blog == false)
            {
                if (HttpContext.Current.User.IsInRole("artycle_editor"))
                {
                    TagBuilder a = new TagBuilder("a");
                    if (news == false)
                    {
                        a.MergeAttribute("href", "/C/ZJ/" + c);
                        b = "статья: ";
                    }
                    else
                    {
                        a.MergeAttribute("href", "/C/ZW/" + c);
                        b = "новость: ";
                    }
                    a.InnerHtml = img.ToString();
                    result.AppendLine(b);
                    result.AppendLine(a.ToString());

                } if (HttpContext.Current.User.IsInRole("chief_editor"))
                {
                    TagBuilder a = new TagBuilder("a");
                    if (news == false)
                    {
                        a.MergeAttribute("href", "/C/ZJ/" + c);
                        b = "статья: ";
                    }
                    else
                    {
                        a.MergeAttribute("href", "/C/ZW/" + c);
                        b = "новость: ";
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
                        a.MergeAttribute("href", "/C/ZS/" + c);
                        b = "пост: ";
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
        public static string EditeAndDeliteCategory(this HtmlHelper html, bool blog, int a, int categoryid, string addedby, string addedbycategory)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder img_1 = new TagBuilder("img");
            TagBuilder img_2 = new TagBuilder("img");
            img_1.MergeAttribute("src", "/content/images/edit.png");
            img_2.MergeAttribute("src", "/content/images/delete.png");
            img_1.MergeAttribute("alt", "правка");
            img_2.MergeAttribute("alt", "удалить");
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
                        a_1.MergeAttribute("href", "/C/ZG/" + a);
                        a_2.MergeAttribute("href", "/C/ZI/" + a);
                        a_2.MergeAttribute("onclick", "return confirm ('Вы уверены что хотите удалить блог?');");
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
                            a_1.MergeAttribute("href", "/C/ZG/" + a);
                            a_2.MergeAttribute("href", "/C/ZI/" + a);
                            a_2.MergeAttribute("onclick", "return confirm ('Вы уверены что хотите удалить блог?');");
                            a_1.InnerHtml = img_1.ToString();
                            a_2.InnerHtml = img_2.ToString();
                            result.AppendLine(a_1.ToString());
                            result.AppendLine(a_2.ToString());

                        }
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
                                a_1.MergeAttribute("href", "/C/ZB/" + a);
                            }
                            else {
                                a_1.MergeAttribute("href", "/C/ZD/" + a);
                                string b = "подрубрика: ";
                                result.AppendLine(b);
                            }
                            a_2.MergeAttribute("href", "/C/ZH/" + a);
                            a_2.MergeAttribute("onclick", "return confirm ('Вы уверены что хотите удалить рубрику?');");
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
                                    a_1.MergeAttribute("href", "/C/ZD/" + a);
                                    string b = "подрубрика: ";
                                    result.AppendLine(b);
                                    a_2.MergeAttribute("href", "/C/ZH/" + a);
                                    a_2.MergeAttribute("onclick", "return confirm ('Вы уверены что хотите удалить рубрику?');");
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
                            a_1.MergeAttribute("href", "/C/ZB/" + a);
                        }
                        else
                        {
                            a_1.MergeAttribute("href", "/C/ZD/" + a);
                            string b = "подрубрика: ";
                            result.AppendLine(b);
                        }
                        a_2.MergeAttribute("href", "/C/ZH/" + a);
                        a_2.MergeAttribute("onclick", "return confirm ('Вы уверены что хотите удалить рубрику?');");
                        a_1.InnerHtml = img_1.ToString();
                        a_2.InnerHtml = img_2.ToString();
                        result.AppendLine(a_1.ToString());
                        result.AppendLine(a_2.ToString());
                    }               

            }
            string br = "<br/>";
            result.AppendLine(br);
            return result.ToString();
        }
         public static string EditeAndDeliteCategory(this HtmlHelper html, bool blog, int a, int categoryid, string addedby)
        {
            StringBuilder result = new StringBuilder();
            TagBuilder img_1 = new TagBuilder("img");
            TagBuilder img_2 = new TagBuilder("img");
            img_1.MergeAttribute("src", "/content/images/edit.png");
            img_2.MergeAttribute("src", "/content/images/delete.png");
            img_1.MergeAttribute("alt", "правка");
            img_2.MergeAttribute("alt", "удалить");
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
                        a_1.MergeAttribute("href", "/C/ZG/" + a);
                        a_2.MergeAttribute("href", "/C/ZI/" + a);
                        a_2.MergeAttribute("onclick", "return confirm ('Вы уверены что хотите удалить блог?');");
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
                            a_1.MergeAttribute("href", "/C/ZG/" + a);
                            a_2.MergeAttribute("href", "/C/ZI/" + a);
                            a_2.MergeAttribute("onclick", "return confirm ('Вы уверены что хотите удалить блог?');");
                            a_1.InnerHtml = img_1.ToString();
                            a_2.InnerHtml = img_2.ToString();
                            result.AppendLine(a_1.ToString());
                            result.AppendLine(a_2.ToString());

                        }
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
                                a_1.MergeAttribute("href", "/C/ZB/" + a);
                            }
                            else {
                                a_1.MergeAttribute("href", "/C/ZD/" + a);
                                string b = "подрубрика: ";
                                result.AppendLine(b);
                            }
                            a_2.MergeAttribute("href", "/C/ZH/" + a);
                            a_2.MergeAttribute("onclick", "return confirm ('Вы уверены что хотите удалить рубрику?');");
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
                            a_1.MergeAttribute("href", "/C/ZB/" + a);
                        }
                        else
                        {
                            a_1.MergeAttribute("href", "/C/ZD/" + a);
                            string b = "подрубрика: ";
                            result.AppendLine(b);
                        }
                        a_2.MergeAttribute("href", "/C/ZH/" + a);
                        a_2.MergeAttribute("onclick", "return confirm ('Вы уверены что хотите удалить рубрику?');");
                        a_1.InnerHtml = img_1.ToString();
                        a_2.InnerHtml = img_2.ToString();
                        result.AppendLine(a_1.ToString());
                        result.AppendLine(a_2.ToString());
                    }               

            }
            string br = "<br/>";
            result.AppendLine(br);
            return result.ToString();
        }
    
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Mytrip.Articles.Helpers
{
   public static class ArticleFileHelper
   {
       public static string MytripAddTextArea(this HtmlHelper html, int id, string value, string name, string extension)
       {
           string _value = string.Empty;
           if (extension != ".ico" && extension != ".png" && extension != ".jpg" && extension != ".gif")
           {
               _value = "<a href='" + value + "'><img src='/content/images/download.png' alt='download' style='border:0px; width:20px'/></a>" +
                   "  <a href='" + value + "'>" + name + "</a>";
           }
           else
           {
               _value = "<img src='" + value + "' alt='" + name + "' style='border:0px;'/>";
           }

           StringBuilder result = new StringBuilder();
           TagBuilder input = new TagBuilder("input");
           input.MergeAttribute("id", "image" + id);
           input.MergeAttribute("type", "image");
           input.MergeAttribute("src", "/content/images/addtextarea.png");
           input.MergeAttribute("style", "border:0px; width:20px");
           input.MergeAttribute("value", _value);
           input.MergeAttribute("onclick", "window.close();");
           result.AppendLine(input.ToString());
           return result.ToString();
       }
       public static string MytripAddTextArea(this HtmlHelper html, int id, string value, string name)
       {
           string _value = "<img src='" + value + "' alt='" + name + "' style='border:0px;'/>";

           StringBuilder result = new StringBuilder();
           TagBuilder input = new TagBuilder("input");
           input.MergeAttribute("id", "image" + id);
           input.MergeAttribute("type", "image");
           input.MergeAttribute("src", value);
           input.MergeAttribute("style", "border:0px");
           input.MergeAttribute("value", _value);
           input.MergeAttribute("onclick", "window.close();");
           result.AppendLine(input.ToString());
           return result.ToString();
       }
       public static string MytripAddTextAreaScript(this HtmlHelper html, int id, string area)
       {
           StringBuilder result = new StringBuilder();
           TagBuilder script = new TagBuilder("script");
           script.MergeAttribute("type", "text/javascript");
           StringBuilder _result = new StringBuilder();
           string scriptFirst = "$(document).ready(function(){";
           _result.AppendLine(scriptFirst);
           int _id = 0;
           while (id > _id)
           {
               _id++;
               string _script = "$('#image" +
                   _id + "').click(function(){var htmlText=$('#image" +
                   _id + "').val(); var openerID = '#" +
                   area + "'; var $parent = $(window.opener); $parent[0].jHtmlArea_API[openerID][0].pasteHTML(htmlText);});";
               _result.AppendLine(_script);
           }
           string scriptLast = "});";
           _result.AppendLine(scriptLast);
           script.InnerHtml = _result.ToString();
           result.AppendLine(script.ToString());
           return result.ToString();

       }
       public static string MytripAddTextAreaScript(this HtmlHelper html)
       {
           StringBuilder result = new StringBuilder();
           TagBuilder script = new TagBuilder("script");
           script.MergeAttribute("type", "text/javascript");
           StringBuilder _result = new StringBuilder();
           string script1 = "var jHtmlArea_API = new Object();";
           string script2 = "$(document).ready(function(){";
           string script3 = "$('#article').htmlarea({";
           string script4 = "toolbar: [['html'],['bold', 'italic', 'underline', 'strikethrough', 'forecolor', 'subscript', 'superscript'],";
           string script5 = " ['increasefontsize', 'decreasefontsize'],";
           string script5_1 = "['orderedlist', 'unorderedlist', 'horizontalrule'],['indent', 'outdent'],";
           string script6 = "['justifyleft', 'justifycenter', 'justifyright'],['link', 'unlink', {";
           string script7 = "css: 'image',text: 'Image Gallery',action: function (btn) {jHtmlArea_API['#article'] = $(this);";
           string script8 = "var url = '/ArticleFile/Index/()Content()Articles()" + HttpContext.Current.User.Identity.Name + "/article';";
           if (HttpContext.Current.User.IsInRole(ArticlesSetting.roleChiefEditor))
               script8 = "var url = '/ArticleFile/Index/()Content()Articles/article';";
           string script9 = "var gallery = window.open(url, 'gallery', 'width=800,height=600,menubar=0,location=0,resizable=0,scrollbars=1,status=0');";
           string script10 = "gallery.focus();";
           string script11 = "}},{css: 'smile',text: 'Smiles', action: function (btn) {jHtmlArea_API['#article'] = $(this); var url = '/ArticleFile/Smile/article';";
           string script12 = "var gallery = window.open(url, 'gallery', 'width=300,height=300,menubar=0,location=0,resizable=0,scrollbars=1,status=0');";
           //script10
           string script13 = "}}],['p', 'h1', 'h2', 'h3', 'h4', 'h5', 'h6'], ['cut', 'copy', 'paste']]});";
           string script14 = "$('#abstract').htmlarea({";
           //script4
           //script5
           //script6
           string script15 = "css: 'smile',text: 'Smiles', action: function (btn) {jHtmlArea_API['#abstract'] = $(this); var url = '/ArticleFile/Smile/abstract';";
           //script12
           //script10
           //script13
           string script16 = "$('#fotoabstract').htmlarea({toolbar: [{ css: 'image',text: 'Image Gallery',action: function (btn) {jHtmlArea_API['#fotoabstract'] = $(this);";
           string script17 = "var url = '/ArticleFile/Index/()Content()Articles()" + HttpContext.Current.User.Identity.Name + "/fotoabstract';";
           if (HttpContext.Current.User.IsInRole(ArticlesSetting.roleChiefEditor))
               script17 = "var url = '/ArticleFile/Index/()Content()Articles/fotoabstract';";
           //script9
           //script10
           string script18 = "}}] });});";
           _result.AppendLine(script1);
           _result.AppendLine(script2);
           _result.AppendLine(script3);
           _result.AppendLine(script4);
           _result.AppendLine(script5);
           _result.AppendLine(script5_1);
           _result.AppendLine(script6);
           _result.AppendLine(script7);
           _result.AppendLine(script8);
           _result.AppendLine(script9);
           _result.AppendLine(script10);
           _result.AppendLine(script11);
           _result.AppendLine(script12);
           _result.AppendLine(script10);
           _result.AppendLine(script13);
           _result.AppendLine(script14);
           _result.AppendLine(script4);
           _result.AppendLine(script5);
           _result.AppendLine(script6);
           _result.AppendLine(script15);
           _result.AppendLine(script12);
           _result.AppendLine(script10);
           _result.AppendLine(script13);
           _result.AppendLine(script16);
           _result.AppendLine(script17);
           _result.AppendLine(script9);
           _result.AppendLine(script10);
           _result.AppendLine(script18);
           script.InnerHtml = _result.ToString();
           result.AppendLine(script.ToString());
           return result.ToString();

       }
      
       public static string MytripDirectory(this HtmlHelper html, string path, string controller, string param)
       {
           string result = string.Empty;
           if (!String.IsNullOrEmpty(path))
           {
               string[] directory = path.Remove(0, 2).Replace("()", "/").Split('/');
               string _path = string.Empty;
               int id = 0;
               foreach (string item in directory)
               {
                   if (item.IndexOf(".") != -1)
                   {
                       result += " / " + item;
                   }
                   else
                   {
                       if (param == "article" | param == "fotoabstract")
                       {
                           if (id != 0 && item != "Content")
                           {
                               if (id == 1 && HttpContext.Current.User.IsInRole(ArticlesSetting.roleChiefEditor))
                               {
                                   _path += "()" + item;
                                   TagBuilder _result = new TagBuilder("a");
                                   _result.MergeAttribute("href", "/" + controller + "/Index/" + _path + "/" + param);
                                   _result.InnerHtml = item;
                                   result += " / " + _result;
                               }
                               if (id == 1 && !HttpContext.Current.User.IsInRole(ArticlesSetting.roleChiefEditor)) { _path += "()" + item; }
                               if (id > 1)
                               {
                                   _path += "()" + item;
                                   TagBuilder _result = new TagBuilder("a");
                                   _result.MergeAttribute("href", "/" + controller + "/Index/" + _path + "/" + param);
                                   _result.InnerHtml = item;
                                   result += " / " + _result;
                               }
                           }
                           else { _path += "()" + item; }
                           id++;
                       }
                   }
               }
           }
           return result;
       } 
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;

namespace Mytrip.Core.Repository
{
   public class EditePageRepository
    {
       public static string[] WritePage(string directory)
       {
           string absolutDirectory = HttpContext.Current.Server.MapPath(directory);
           string[] file_in = File.ReadAllLines(absolutDirectory);
           return file_in;
       }
       public static void CreatePage(string directory,string[] files)
       {
           string absolutDirectory = HttpContext.Current.Server.MapPath(directory);
           File.WriteAllLines(absolutDirectory,files);
       }
       public static void CreatePage(string directory, string files)
       {
           string absolutDirectory = HttpContext.Current.Server.MapPath(directory);
           File.WriteAllText(absolutDirectory, files);
       }
       public static void CreatePage1(string directory,string param)
       {
           string[] files = PageSettings.SiteMaster(param);
           int count=files.Count();
           string[] files1 = new string[count];
           int count1 = 0;
           foreach (string x in files)
           {
               string a = x.Replace("[x_1_x]", "\"");
               a = a.Replace("[x_2_x]", "^");
               files1[count1] = a;
               count1++;
           }
           string absolutDirectory = HttpContext.Current.Server.MapPath(directory);
           File.WriteAllLines(absolutDirectory, files1);
       }
       public static string HtmlDecoding(string a)
       {
           if (a.IndexOf("<") != -1 || a.IndexOf(">") != -1)
           {
               a = a.Replace("<", "[_<_]");
               a = a.Replace(">", "[_>_]");
               a = a.Replace("[_<_]%", "[_<_%_]");
               a = a.Replace("%[_>_]", "[_%_>_]");
               a = a.Replace("[_<_]/", "[_<_/_]");
               a = a.Replace("/[_>_]", "[_/_>_]");
               a = a.Replace("[_<_]=", "<=");
               a = a.Replace("=[_>_]", "=>");
               a = a.Replace("[_<_]script", "[_<_]<span style='color: #800000'>script</span>");
               a = a.Replace("[_<_] script", "[_<_]<span style='color: #800000'>script</span>");
               a = a.Replace("[_<_]h2", "[_<_]<span style='color: #800000'>h2</span>");
               a = a.Replace("[_<_] h2", "[_<_]<span style='color: #800000'>h2</span>");
               a = a.Replace("[_<_]table", "[_<_]<span style='color: #800000'>table</span>");
               a = a.Replace("[_<_] table", "[_<_]<span style='color: #800000'>table</span>");
               a = a.Replace("[_<_]tr", "[_<_]<span style='color: #800000'>tr</span>");
               a = a.Replace("[_<_] tr", "[_<_]<span style='color: #800000'>tr</span>");
               a = a.Replace("[_<_]th", "[_<_]<span style='color: #800000'>th</span>");
               a = a.Replace("[_<_] th", "[_<_]<span style='color: #800000'>th</span>");
               a = a.Replace("[_<_]td", "[_<_]<span style='color: #800000'>td</span>");
               a = a.Replace("[_<_] td", "[_<_]<span style='color: #800000'>td</span>");
               a = a.Replace("[_<_]html", "[_<_]<span style='color: #800000'>html</span>");
               a = a.Replace("[_<_] html", "[_<_]<span style='color: #800000'>html</span>");
               a = a.Replace("[_<_]head", "[_<_]<span style='color: #800000'>head</span>");
               a = a.Replace("[_<_] head", "[_<_]<span style='color: #800000'>head</span>");
               a = a.Replace("[_<_]title", "[_<_]<span style='color: #800000'>title</span>");
               a = a.Replace("[_<_] title", "[_<_]<span style='color: #800000'>title</span>");
               a = a.Replace("[_<_]link", "[_<_]<span style='color: #800000'>link</span>");
               a = a.Replace("[_<_] link", "[_<_]<span style='color: #800000'>link</span>");
               a = a.Replace("[_<_]div", "[_<_]<span style='color: #800000'>div</span>");
               a = a.Replace("[_<_] div", "[_<_]<span style='color: #800000'>div</span>");
               a = a.Replace("[_<_]body", "[_<_]<span style='color: #800000'>body</span>");
               a = a.Replace("[_<_] body", "[_<_]<span style='color: #800000'>body</span>");
               a = a.Replace("[_<_]ul", "[_<_]<span style='color: #800000'>ul</span>");
               a = a.Replace("[_<_] ul", "[_<_]<span style='color: #800000'>ul</span>");
               a = a.Replace(";", "<span style='color: #000000'>;</span>");
               a = a.Replace("[_<_%_]=", "[_<_%_]<span style='color: #0000FF'>=</span>");
               a = a.Replace("[_<_%_] =", "[_<_%_]<span style='color: #0000FF'>=</span>");
               a = a.Replace("[_<_%_]@ Page", "<span style='background-color: #FFFF00;color:#000000;'>&lt;</span><span style='background-color: #FFFF00;color:#000000;'>%</span><span style='color: #0000FF'>@ <span style='color: #800000'>Page</span>");
               a = a.Replace("[_<_%_] @ Page", "<span style='background-color: #FFFF00;color:#000000;'>&lt;</span><span style='background-color: #FFFF00;color:#000000;'>%</span><span style='color: #0000FF'>@ <span style='color: #800000'>Page</span>");
               a = a.Replace("[_<_%_]@ Master", "<span style='background-color: #FFFF00;color:#000000;'>&lt;</span><span style='background-color: #FFFF00;color:#000000;'>%</span><span style='color: #0000FF'>@ <span style='color: #800000'>Master</span>");
               a = a.Replace("[_<_%_] @ Master", "<span style='background-color: #FFFF00;color:#000000;'>&lt;</span><span style='background-color: #FFFF00;color:#000000;'>%</span><span style='color: #0000FF'>@ <span style='color: #800000'>Master</span>");

               a = a.Replace("[_<_]!DOCTYPE html PUBLIC", "[_<_]!<span style='color: #800000'>DOCTYPE</span>&nbsp;<span style='color: red'>html PUBLIC</span>");
               a = a.Replace("[_<_]! DOCTYPE html PUBLIC", "[_<_]!<span style='color: #800000'>DOCTYPE</span>&nbsp;<span style='color: red'>html PUBLIC</span>");

               a = a.Replace("[_<_%_]//", "<span style='background-color: #FFFF00;color:#000000;'>&lt;</span><span style='background-color: #FFFF00;color:#000000;'>%</span><span style='color: #008000'>//");
               a = a.Replace("[_<_%_] //", "<span style='background-color: #FFFF00;color:#000000;'>&lt;</span><span style='background-color: #FFFF00;color:#000000;'>%</span><span style='color: #008000'>//");
               a = a.Replace("[_<_%_]  //", "<span style='background-color: #FFFF00;color:#000000;'>&lt;</span><span style='background-color: #FFFF00;color:#000000;'>%</span><span style='color: #008000'>//");

               a = a.Replace("[_<_%_]", "<span style='background-color: #FFFF00;color:#000000;'>&lt;</span><span style='background-color: #FFFF00;color:#000000;'>%</span><span style='color: #A31515'>");
               a = a.Replace("[_%_>_]", "</span><span style='background-color: #FFFF00;color:#000000;'>%</span><span style='background-color: #FFFF00;color:#000000;'>&gt;</span>");
               a = a.Replace("[_<_]", "<span style='color: #0000FF'>&lt;</span><span style='color: #0000FF'>");
               a = a.Replace("[_>_]", "</span><span style='color: #0000FF'>&gt;</span>");
               a = a.Replace("[_<_/_]", "<span style='color: #0000FF'>&lt;&frasl;</span><span style='color: #800000'>");
               a = a.Replace("[_/_>_]", "</span><span style='color: #0000FF'>&frasl;&gt;</span>");
               //HTML
               a = a.Replace("Language=", "<span style='color: red'>Language</span>=");
               a = a.Replace("MasterPageFile=", "<span style='color: red'>MasterPageFile</span>=");
               a = a.Replace("Inherits=", "<span style='color: red'>Inherits</span>=");
               a = a.Replace("src=", "<span style='color: red'>src</span>=");
               a = a.Replace("href=", "<span style='color: red'>href</span>=");
               a = a.Replace("type=", "<span style='color: red'>type</span>=");
               a = a.Replace("class=", "<span style='color: red'>class</span>=");
               a = a.Replace("Title=", "<span style='color: red'>Title</span>=");
               a = a.Replace("title=", "<span style='color: red'>title</span>=");
               a = a.Replace("xmlns=", "<span style='color: red'>xmlns</span>=");
               a = a.Replace("rel=", "<span style='color: red'>rel</span>=");
               a = a.Replace("id=", "<span style='color: red'>id</span>=");
               a = a.Replace("ContentPlaceHolderID=", "<span style='color: red'>ContentPlaceHolderID</span>=");
               a = a.Replace("ID=", "<span style='color: red'>ID</span>=");
               a = a.Replace("runat=", "<span style='color: red'>runat</span>=");
               //C#
               a = a.Replace("Html.PageTitle(", "<span style='color: #000000'>Html.PageTitle(</span>");
               a = a.Replace("Html.CssLink(", "<span style='color: #000000'>Html.CssLink(</span>");
               a = a.Replace("Html.Logo(", "<span style='color: #000000'>Html.Logo(</span>");
               a = a.Replace("Html.LanguageMenu(", "<span style='color: #000000'>Html.LanguageMenu(</span>");
               a = a.Replace("Html.ThemeMenu(", "<span style='color: #000000'>Html.ThemeMenu(</span>");
               a = a.Replace("Html.LogonMenu(", "<span style='color: #000000'>Html.LogonMenu(</span>");
               a = a.Replace("Html.MytripMenu(", "<span style='color: #000000'>Html.MytripMenu(</span>");
               a = a.Replace(")", "<span style='color: #000000'>)</span>");
               a = a.Replace(",", "<span style='color: #000000'>,</span>");
               a = a.Replace("Html.MytripImageLink(", "<span style='color: #000000'>Html.MytripImageLink(</span>");
               a = a.Replace("Url.Action(", "<span style='color: #000000'>Url.Action(</span>");
               a = a.Replace("{", "<span style='color: #000000'>{</span>");
               a = a.Replace("}", "<span style='color: #000000'>}</span>");
               a = a.Replace("CoreLanguage.file_manager", "<span style='color: #5991AF'>CoreLanguage</span><span style='color: #000000'>.file_manager</span>");
               a = a.Replace("asp:ContentPlaceHolder", "<span style='color: #800000'>asp</span><span style='color: #0000FF'>:</span><span style='color: #800000'>ContentPlaceHolder</span>");
               a = a.Replace("asp:Content", "<span style='color: #800000'>asp</span><span style='color: #0000FF'>:</span><span style='color: #800000'>Content</span>");
               }
           return a;
       }
    }
}

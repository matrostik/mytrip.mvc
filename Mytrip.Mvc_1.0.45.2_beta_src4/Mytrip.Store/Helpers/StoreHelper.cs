using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Mytrip.Store.Repository.DataEntities;
using Mytrip.Store.Models;
using Mytrip.Mvc;
using System.Web;

namespace Mytrip.Store.Helpers
{
   public static class StoreHelper
    {
       public static string Department(this HtmlHelper html, IQueryable<mytrip_storedepartment> department, int take)
       {
           StoreSettings store = new StoreSettings(); 
           int   column = store.columnDepartment();
           int _count = department.Count();
           if (column > _count)
               column = _count;
           int _count2 = 0;
           int _line = 1;
           if (_count > column)
           {
               Math.DivRem(_count, column, out _count2);
               _line = (int)Math.Ceiling((double)_count / column);
           }
           int count = 1;
           int tr = 0;
           int width = 100;
           if (column > 0)
               width = 100 / column;
           int style = store.styleDepartment();
           StringBuilder result = new StringBuilder();
           TagBuilder table = new TagBuilder("table");
           int _line2 = 0;
           string finaltr = string.Empty;
           string start = string.Empty;
           string end = string.Empty;
           string styletable = string.Empty;
           foreach (var article in department)
           {
               int countproduct = article.mytrip_storeproduct.Count();
               string subdepartment = string.Empty;
               foreach (var item in article.mytrip_storedepartment1)
               {int _countproduct=item.mytrip_storeproduct.Count();
                   subdepartment += "<li><a href=\"/Store/Index/1/10/" + item.DepartmentId + "/0/1/" + item.Path + "\" >" +
                       item.Title + " ("+_countproduct+")</a></li>";
                   countproduct += _countproduct;
               }
               string _subdepartment = string.Empty;
               if (!String.IsNullOrEmpty(subdepartment))
               {
                   _subdepartment = "<ul>" + subdepartment + "</ul>";
               }

               string _content ="<a href=\"/Store/Index/1/10/" + article.DepartmentId + "/0/1/" + article.Path + "\">"+GeneralMethods.ImageForAbstract(article.Image,store.widthImgDepartment())+"</a>";
               _content += "<b><a href=\"/Store/Index/1/10/" + article.DepartmentId + "/0/1/" + article.Path + "\" class=\"hometitle\" >" +
                   article.Title + " (" + countproduct + ")</a></b><br/>" + article.Body + _subdepartment;

               int tr2 = 0;
               int _line3 = 0;
               result.AppendLine(GeneralMethods.StyleTable(column, style, tr, width, _content,
                   count, _count2, _line, _line2, out tr2, out _line3, out finaltr, out start, out end, out styletable));
               tr = tr2;
               _line2 = _line3;
               count++;
           }
           if (tr > 0 && tr % 2 != 0)
               result.AppendLine(finaltr);
           table.AddCssClass(styletable);
           table.InnerHtml = result.ToString(); 
           string _CategoryName = string.Empty;
           if (column > 0)
               return _CategoryName + start + table.ToString() + end;
           else
               return string.Empty;
       }
       public static string TitleDepartment(this HtmlHelper html, TitleDepartmentModel x)
       { StoreSettings store = new StoreSettings();
       string a = string.Empty;
       string b = string.Empty;
       string c = "<table style=\"padding:0;border:0;width:100%;\">";
       string producer = "<h2 class=\"title\">" +StoreLanguage.Producers +"</h2>";
       string _store = "<h2 class=\"title\">" +store.nameStore() +"</h2>";
       if (x.count >= 0||x._search)
       {
           producer = "<h2 class=\"title\"><a href=\"/Store/Index/1/10/0/0/1/Producer\" >" +
                   StoreLanguage.Producers +
                   "</a></h2>";
           _store = "<h2 class=\"title\"><a href=\"/Store/Index/1/10/0/0/1/Department\" >" +
                 store.nameStore() +
                 "</a></h2>";
       }
       if (x.producer)
       {
           c += "<tr><td style=\"padding:0;border:0;\">" +
                   producer +
                   "</td><td style=\"padding:0;border:0;\"><h2 class=\"adminlink\"><a href=\"/Store/Index/1/10/0/0/1/Department\" >" +
                   store.nameStore() + "</a></h2></td></tr>";
       }
       else
       {
           c += "<tr><td style=\"padding:0;border:0;\">" +
                 _store +
                 "</td><td style=\"padding:0;border:0;\"><h2 class=\"adminlink\"><a href=\"/Store/Index/1/10/0/0/1/Producer\" >" +
                 StoreLanguage.Producers +
                 "</a></h2></td></tr>";
       }
       if (x.count >= 0)
       {
           b = " (" + x.count + ")";

           if (x.subDepartmentId > 0)
           {
               a = "<a href=\"/Store/Index/1/10/" + x.subDepartmentId + "/0/1/" + x.subDepartmentPath + "\" >" +
                   x.subDepartmentTitle + " (" + x.subcount + ")</a> / ";
           }
           c += "<tr><td style=\"padding:0;border:0;vertical-align: top;\">" +
           "<h2 class=\"title\">" + a + x.title + b + "</h2>" + x.body +
           "</td><td style=\"padding:0;border:0;\">" +GeneralMethods.ImageForAbstract(x.img, store.widthImgDepartment()) + "</td></tr>";
       }
       c += "</table>";
       if (x._search) {
           if (x.id > 0 && x.ProducerId > 0)
           {
               c += "<table style=\"padding:0;border:0;width:100%;\"><tr><td style=\"padding:0;border:0;vertical-align: top;\">";
               b = " (" + x.departmentcount + ")";

               if (x.subDepartmentId > 0)
               {
                   a = "<a href=\"/Store/Index/1/10/" + x.subDepartmentId + "/0/1/" + x.subDepartmentPath + "\" >" +
                       x.subDepartmentTitle + " (" + x.subcount + ")</a> / ";
               }
               c += "<h2 class=\"title\">" + a +
                   "<a href=\"/Store/Index/1/10/" + x.id + "/0/1/" + x.path + "\" >" + x.title + b + "</a></h2>" + x.body +
           "</td><td style=\"padding:0;border:0;padding-right:10px;\">" +GeneralMethods.ImageForAbstract(x.img, store.widthImgDepartment()) + "</td>";
              
               
               c += "<td style=\"padding:0;border:0;vertical-align: top;padding-left:10px;\">" +
              "<h2 class=\"title\"><a href=\"/Store/Index/1/10/0" + x.ProducerId + "/1/" + x.ProducerPath + "\" >" + x.ProducerTitle
              + " (" + x.producercount + ")" + "</a></h2>" + x.ProducerBody +
                          "</td><td style=\"padding:0;border:0;\">" +GeneralMethods.ImageForAbstract(x.ProducerImg, store.widthImgDepartment());
               c += "</td></tr></table>";
           }
           string search = string.Empty;
           if (x.search.Length >0)
               search =" "+ StoreLanguage._for+" \"" + x.search + "\"";
           c += "<table style=\"padding:0;border:0;width:100%;\"><tr><td style=\"padding:0;border:0;\">";
           c += "<h3 class=\"title\">"+StoreLanguage.Search+search+", "+ StoreLanguage.found+" " + x.totalsearch +" "+ StoreLanguage.results+"</h3>";
           c += "</td></tr></table>";
       
       }
           return c;
       
       }
       public static string Product(this HtmlHelper html, IQueryable<mytrip_storeproduct> department, int take,int subdepartment,
           bool producer, bool DepartmentAndProducer, bool DepartmentAndProducer2)
       {
           StoreSettings store = new StoreSettings();
           int column = store.columnProduct();
           int _count = department.Count();
           if (column > _count)
               column = _count;
           int _count2 = 0;
           int _line = 1;
           if (_count > column)
           {
               Math.DivRem(_count, column, out _count2);
               _line = (int)Math.Ceiling((double)_count / column);
           }
           int count = 1;
           int tr = 0;
           int width = 100;
           if (column > 0)
               width = 100 / column;
           int style = store.styleProduct();
           StringBuilder result = new StringBuilder();
           TagBuilder table = new TagBuilder("table");
           int _line2 = 0;
           string finaltr = string.Empty;
           string start = string.Empty;
           string end = string.Empty;
           string styletable = string.Empty;
           foreach (var article in department)
           {
               string departmentlink = string.Empty;
               if (!producer&&!DepartmentAndProducer)
               {
                       departmentlink = StoreLanguage.Producer + " " + "<a href=\"/Store/Index/1/10/0/" + article.ProducerId + "/1/" + article.mytrip_storeproducer.Path + "\" >" +
                       article.mytrip_storeproducer.Title + "</a><br/>";
                  
               }
               if (producer||DepartmentAndProducer2)
               {
                   if (article.mytrip_storedepartment.SubDepartmentId == 0)
                   {
                       departmentlink += StoreLanguage.department + " " + "<a href=\"/Store/Index/1/10/" + article.DepartmentId + "/0/1/" + article.mytrip_storedepartment.Path + "\" >" +
                       article.mytrip_storedepartment.Title + "</a><br/>";
                   }
                   else 
                   {
                       departmentlink += StoreLanguage.department + " " + "<a href=\"/Store/Index/1/10/" + article.mytrip_storedepartment.SubDepartmentId + "/0/1/" + article.mytrip_storedepartment.mytrip_storedepartment2.Path + "\" >" +
                           article.mytrip_storedepartment.mytrip_storedepartment2.Title + "</a>";
                       departmentlink += "<br/>" + StoreLanguage.subdepartment + " " + "<a href=\"/Store/Index/1/10/" + article.DepartmentId + "/0/1/" + article.mytrip_storedepartment.Path + "\" >" +
                       article.mytrip_storedepartment.Title + "</a>";
                   }
               }
               if (!producer && subdepartment == 0 && article.mytrip_storedepartment.SubDepartmentId != 0 && !DepartmentAndProducer2)
               {
                   departmentlink += StoreLanguage.subdepartment + " " + "<a href=\"/Store/Index/1/10/" + article.DepartmentId + "/0/1/" + article.mytrip_storedepartment.Path + "\" >" +
                       article.mytrip_storedepartment.Title + "</a>";
               }


               string prise = string.Empty;
               if (article.ViewPrice)
                   prise = StoreLanguage.Prise + " <b>" + article.Price + "</b>";
               string votes = string.Empty;
               if(article.ViewVotes)
                   votes = GeneralMethods.CoreRating(true, false, (double)article.TotalVotes, -1) + "</b><br/>";

               string _content = "<a href=\"/Store/View/" + article.ProductId + "/" + article.Path + "\">" + GeneralMethods.ImageForAbstract(article.Image, store.widthImgDepartment()) + "</a>";
               _content += "<b><a href=\"/Store/View/" + article.ProductId + "/" + article.Path + "\" class=\"hometitle\" >" +
                   article.Title + "</a></b><br/>" + article.Abstract + "<br/>" + votes + prise + "<br/>" + String.Format("{0:dd MMMM yyyy}", article.CreationDate) + "<br/>" + departmentlink;
               int tr2 = 0;
               int _line3 = 0;
               result.AppendLine(GeneralMethods.StyleTable(column, style, tr, width, _content,
                   count, _count2, _line, _line2, out tr2, out _line3, out finaltr, out start, out end, out styletable));
               tr = tr2;
               _line2 = _line3;
                count++;
           }
           if (tr > 0 && tr % 2 != 0)
               result.AppendLine(finaltr);
           table.AddCssClass(styletable);
           table.InnerHtml = result.ToString(); 
           string _CategoryName = string.Empty;
           if (column > 0)
               return _CategoryName + start + table.ToString() + end;
           else
               return string.Empty;
       }
       public static string Producer(this HtmlHelper html, IQueryable<mytrip_storeproducer> department, int take)
       {
           StoreSettings store = new StoreSettings();
           int column = store.columnDepartment();
           int _count = department.Count();
           if (column > _count)
               column = _count;
           int _count2 = 0;
           int _line = 1;
           if (_count > column)
           {
               Math.DivRem(_count, column, out _count2);
               _line = (int)Math.Ceiling((double)_count / column);
           }
           int count = 1;
           int tr = 0;
           int width = 100;
           if (column > 0)
               width = 100 / column;
           int style = store.styleDepartment();
           StringBuilder result = new StringBuilder();
           TagBuilder table = new TagBuilder("table");
           int _line2 = 0;
           string finaltr = string.Empty;
           string start = string.Empty;
           string end = string.Empty;
           string styletable = string.Empty;
           foreach (var article in department)
           {
               int countproduct = article.mytrip_storeproduct.Count();

               string _content = "<a href=\"/Store/Index/1/10/0/" + article.ProducerId + "/1/" + article.Path + "\">" + GeneralMethods.ImageForAbstract(article.Image, store.widthImgDepartment()) + "</a>";
               _content += "<b><a href=\"/Store/Index/1/10/0/" + article.ProducerId + "/1/" + article.Path + "\" class=\"hometitle\" >" +
                   article.Title + " (" + countproduct + ")</a></b><br/>" + article.Body;
               int tr2 = 0;
               int _line3 = 0;
               result.AppendLine(GeneralMethods.StyleTable(column, style, tr, width, _content,
                   count, _count2, _line, _line2, out tr2, out _line3, out finaltr, out start, out end, out styletable));
               tr = tr2;
               _line2 = _line3;
               count++;
               
               count++;
           }
           if (tr > 0 && tr % 2 != 0)
               result.AppendLine(finaltr);
           table.AddCssClass(styletable);
           table.InnerHtml = result.ToString(); 
           string _CategoryName = string.Empty;
           if (column > 0)
               return _CategoryName + start + table.ToString() + end;
           else
               return string.Empty;
       }
       public static string Sorting(this HtmlHelper html)
       {
           string[] urlpath = GeneralMethods.UrlDictionary(HttpContext.Current.Request.Path);
           string Controller = urlpath[1];
           string Action = urlpath[2];
           string Page = urlpath[3];
           string TotalSize = urlpath[4];
           string Department = urlpath[5];
           string Producer = urlpath[6];
           string Sorting = urlpath[7];
           string Path = urlpath[8];
           string SmallPrice = "x";
           string BigPrice = "x";
           if (urlpath.Length >= 11)
           {
               SmallPrice = urlpath[9];
               BigPrice = urlpath[10];
           }
           ThemeSetting theme=new ThemeSetting();
           string b="/"+Controller+"/"+Action+"/"+1+"/"+TotalSize+"/"+Department+"/"+Producer+"/";
           string a="/"+Path;
           if(SmallPrice!="x"&&BigPrice!="x")
               a+="/"+SmallPrice+"/"+BigPrice;
           string up = "<img src=\"/Theme/" + theme.theme() + "/images/uparrow_blue.png\" style=\"width:20px;\" />";
           string down = "<img src=\"/Theme/" + theme.theme() + "/images/downarrow_blue.png\" style=\"width:20px;\" />";
           StringBuilder result = new StringBuilder();
           result.AppendLine("<div class=\"div_border\" style=\"border-left:0;border-right:0;border-top:0px;margin-top:5px;margin-bottom:5px;\"></div>");
           result.AppendLine("<table style=\"padding:0;border:0;\"><tr><td style=\"padding:0;border:0;\">");
           result.AppendLine(StoreLanguage.Sorted+" </td><td style=\"padding:0;border:0;\">");
           result.AppendLine(StoreLanguage.date+" </td><td style=\"padding:0;border:0;\"><a href=\"" + b + 5 + a + "\">" + up + "</a> ");
           result.AppendLine("<a href=\"" + b + 1 + a + "\">" + down + "</a> </td><td style=\"padding:0;border:0;\">");
           result.AppendLine(StoreLanguage.prise_+" </td><td style=\"padding:0;border:0;\"><a href=\"" + b + 6 + a + "\">" + up + "</a> ");
           result.AppendLine("<a href=\"" + b + 2 + a + "\">" + down + "</a> </td><td style=\"padding:0;border:0;\">");
           result.AppendLine(StoreLanguage.votes+" </td><td style=\"padding:0;border:0;\"><a href=\"" + b + 7 + a + "\">" + up + "</a> ");
           result.AppendLine("<a href=\"" + b + 3 + a + "\">" + down + "</a> </td><td style=\"padding:0;border:0;\">");
           result.AppendLine(StoreLanguage.title+" </td><td style=\"padding:0;border:0;\"><a href=\"" + b + 8 + a + "\">" + up + "</a> ");
           result.AppendLine("<a href=\"" + b + 4 + a + "\">" + down + "</a></td></tr></table>");
           return result.ToString();
       }
    }
}

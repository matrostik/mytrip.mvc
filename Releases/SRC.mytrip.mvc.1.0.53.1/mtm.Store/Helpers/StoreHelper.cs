using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using mtm.Store.Repository.DataEntities;
using mtm.Store.Models;
using mtm.Core;
using System.Web;
using mtm.Core.Settings;
using mtm.Core.Repository;
using mtm.Store.Repository;
using mtm.Core.Helpers;
using System.IO;

namespace mtm.Store.Helpers
{
    /// <summary>Хелпер для магазина
    /// </summary>
    public static class StoreHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="directory"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static string GetImage(int id,string title, string directory, int width)
        {
            string name = "_" + id + ".";
            string absolutDirectory = HttpContext.Current.Server.MapPath(directory);
            string img = "";
            DirectoryInfo _absolutDirectory2 = new DirectoryInfo(absolutDirectory);
            if (!_absolutDirectory2.Exists)
                _absolutDirectory2.Create();
            FileInfo[] result = _absolutDirectory2.GetFiles();
            foreach (var x in result)
            {
                if (x.Name.Contains(name))
                {
                    img = "<img src='" + directory + "/" + x.Name + "'class='imgabstract' title='"+title+"' alt='"+title+"' style='width:" + width + "px;'/>";
                    break;
                }
            }
            return img;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static string GetImageProduct(int id, int width)
        {
            string name = "product.";
            string absolutDirectory = HttpContext.Current.Server.MapPath("/Content/Store/Product/" + id);
            string _directory = "";
            DirectoryInfo _absolutDirectory2 = new DirectoryInfo(absolutDirectory);
            if (!_absolutDirectory2.Exists)
                _absolutDirectory2.Create();
            FileInfo[] result = _absolutDirectory2.GetFiles();
            foreach (var x in result)
            {
                if (x.Name.Contains(name))
                {
                    _directory = "<span class='imgproduct'><img src='/Content/Store/Product/" + id + "/" + x.Name + "'class='imgabstract' style='width:" + width + "px; float:left;'/></span>";
                    break;
                }
            }
            return _directory;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetImageProduct(int id)
        {
            string name = "product.";
            string absolutDirectory = HttpContext.Current.Server.MapPath("/Content/Store/Product/" + id);
            string _directory = "";
            DirectoryInfo _absolutDirectory2 = new DirectoryInfo(absolutDirectory);
            if (!_absolutDirectory2.Exists)
                _absolutDirectory2.Create();
            FileInfo[] result = _absolutDirectory2.GetFiles();
            foreach (var x in result)
            {
                if (x.Name.Contains(name))
                {
                    _directory = "<span id='zoomimg' class='imgproduct'><img src='/Content/Store/Product/" + id + "/" + x.Name + "' alt='"+x.Name+"' width='" + ModuleSetting.widthImgProduct()+ "'/><br/>"+StoreLanguage.enlarge+"</span>"; 
                    break;
                }
            }
            return _directory;
        } 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static HtmlString StoreSorting(this HtmlHelper html)
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
            string b = "/" + Controller + "/" + Action + "/" + 1 + "/" + TotalSize + "/" + Department + "/" + Producer + "/";
            string a = "/" + Path;
            if (SmallPrice != "x" && BigPrice != "x")
                a += "/" + SmallPrice + "/" + BigPrice;
            string up = "<img src=\"/Theme/" + ThemeSetting.theme() + "/images/uparrow_blue.png\" style=\"width:14px;\" alt='up' />";
            string down = "<img src=\"/Theme/" + ThemeSetting.theme() + "/images/downarrow_blue.png\" style=\"width:14px;\" alt='down' />";
            StringBuilder result = new StringBuilder();
            result.AppendLine("<table class=\"noborders\"><tr><td>");
            result.AppendLine(StoreLanguage.Sorted + " </td><td>");
            result.AppendLine(StoreLanguage.date + " </td><td><a href=\"" + b + 5 + a + "\">" + up + "</a> ");
            result.AppendLine("<a href=\"" + b + 1 + a + "\">" + down + "</a> </td><td>");
            result.AppendLine(StoreLanguage.prise_ + " </td><td><a href=\"" + b + 6 + a + "\">" + up + "</a> ");
            result.AppendLine("<a href=\"" + b + 2 + a + "\">" + down + "</a> </td><td>");
            result.AppendLine(StoreLanguage.votes + " </td><td><a href=\"" + b + 7 + a + "\">" + up + "</a> ");
            result.AppendLine("<a href=\"" + b + 3 + a + "\">" + down + "</a> </td><td>");
            result.AppendLine(StoreLanguage.title + " </td><td><a href=\"" + b + 8 + a + "\">" + up + "</a> ");
            result.AppendLine("<a href=\"" + b + 4 + a + "\">" + down + "</a></td></tr></table>");
            return new HtmlString(result.ToString());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="count"></param>
        /// <param name="subid"></param>
        /// <param name="search"></param>
        /// <param name="producer"></param>
        /// <returns></returns>
        public static string StoreAddCategoryAndProduct(int id, int count, int subid, bool search, bool producer)
        {
            if (!search && (MytripUser.UserInRole(ModuleSetting.roleChiefStoreManager()) || MytripUser.UserInRole(ModuleSetting.roleStoreManager())))
            {
                TagBuilder img = new TagBuilder("img");
                img.MergeAttribute("src", "/Theme/" + ThemeSetting.theme() + "/images/add.png");
                img.MergeAttribute("style", "height:14px;");
                if (producer)
                    return "<div class=\"right\"><a href=\"/Store/EditorCategory/0/CreateProducer\">" + StoreLanguage.createProducer + " " + img + "</a></div>";
                else
                {
                    if (subid == 0)
                        subid = id;
                    string a = string.Empty;
                    if (count >= 0)
                    {
                        a = "<br/><a href=\"/Store/EditorCategory/" + subid + "/CreateDepartment\">" + StoreLanguage.createSubDepartment2 + " " + img + "</a>";
                        a += "<br/><a href=\"/Store/EditorProduct/" + id + "/0/CreateProduct\">" + StoreLanguage.createProduct + " " + img + "</a>";
                    }
                    return "<div class=\"right\"><a href=\"/Store/EditorCategory/0/CreateDepartment\">" + StoreLanguage.createDepartment + " " + img + "</a>" + a + "</div>";
                }
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <param name="subuser"></param>
        /// <param name="producer"></param>
        /// <param name="prodid"></param>
        /// <returns></returns>
        public static string EditAndDeleteCategory(int id, string user, string subuser, bool producer, int prodid)
        {
            if (MytripUser.UserInRole(ModuleSetting.roleChiefStoreManager()) || (MytripUser.UserInRole(ModuleSetting.roleStoreManager()) && (HttpContext.Current.User.Identity.Name == user || HttpContext.Current.User.Identity.Name == subuser)))
            {
                if (producer)
                    id = prodid;
                string edit = producer ? "/EditProducer" : "/EditDepartment";
                string delete = producer ? "/DeleteProducer" : "/DeleteDepartment";
                return GeneralMethods.ImgInput("/images/edite.png", "/Store/EditorCategory/" + id + edit, "rename", 14) +
                              " " + GeneralMethods.ImgInput("/images/delete.png", "/Store/EditorCategory/" + id + delete, "delete", 14);
            }
            else return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <param name="subuser"></param>
        /// <param name="subuser2"></param>
        /// <param name="prodid"></param>
        /// <returns></returns>
        public static string EditAndDeleteProduct(int id, string user, string subuser, string subuser2, int prodid)
        {
            if (MytripUser.UserInRole(ModuleSetting.roleChiefStoreManager()) || (MytripUser.UserInRole(ModuleSetting.roleStoreManager())
                && (HttpContext.Current.User.Identity.Name == user || HttpContext.Current.User.Identity.Name == subuser || HttpContext.Current.User.Identity.Name == subuser2)))
            {
                string edit = "/" + prodid + "/EditProduct";
                string delete = "/0/DeleteProduct";
                return GeneralMethods.ImgInput("/images/edite.png", "/Store/EditorProduct/" + id + edit, "rename", 14) +
                              " " + GeneralMethods.ImgInput("/images/delete.png", "/Store/EditorProduct/" + id + delete, "delete", 14);
            }
            else return null;
        }
    }
}

/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about Mytrip.Mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System.IO;
using System.Web;

namespace Mytrip.Mvc.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class EditePageRepository
    {
       /// <summary>
       /// 
       /// </summary>
       /// <param name="directory"></param>
       /// <returns></returns>
        public static string[] WritePage(string directory)
       {
           string absolutDirectory = HttpContext.Current.Server.MapPath(directory);
           string[] file_in = File.ReadAllLines(absolutDirectory);
           return file_in;
       }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="directory"></param>
       /// <param name="files"></param>
       public static void CreatePage(string directory, string[] files)
       {
           string absolutDirectory = HttpContext.Current.Server.MapPath(directory);
           File.WriteAllLines(absolutDirectory,files);
       }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="directory"></param>
       /// <param name="files"></param>
        public static void CreatePage(string directory, string files)
       {
           string absolutDirectory = HttpContext.Current.Server.MapPath(directory);
           File.WriteAllText(absolutDirectory, files);
       }
    }
}

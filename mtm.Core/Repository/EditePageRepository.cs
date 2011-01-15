/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about mytrip.mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System.IO;
using System.Web;
using System.Text;

namespace mtm.Core.Repository
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
           string[] file_in = File.ReadAllLines(absolutDirectory, Encoding.ASCII);
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
           File.WriteAllLines(absolutDirectory, files, Encoding.ASCII);
       }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="directory"></param>
       /// <param name="files"></param>
        public static void CreatePage(string directory, string files)
       {
           string absolutDirectory = HttpContext.Current.Server.MapPath(directory);
           File.WriteAllText(absolutDirectory, files, Encoding.ASCII);
       }
    }
    public static class CreateFileRepository
    {
        public static void CreateFile(string directory, byte[] files)
        {
            string absolutDirectory = HttpContext.Current.Server.MapPath(directory);
            File.WriteAllBytes(absolutDirectory, files);
        }
    }
}

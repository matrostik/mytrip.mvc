/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about mytrip.mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System.Linq;
using System.Text;
using System.Xml.Linq;
using mtm.Core.Repository;
using mtm.Core.Settings;

namespace mtm.Core.Install
{
    /// <summary>
    /// Деинсталяция модуля
    /// </summary>
    internal class UninstallModules
    {
        /// <summary>
        /// Деинсталяция модуля
        /// </summary>
        /// <param name="moduleName">Название модуля</param>
        internal void UninstallModule(string moduleName)
        {
            string _moduleName = string.Concat(moduleName, ".Export");

            #region Удаление записей из mtm.Config.xml секции installModules и секции самого модуля
            // **********************************************
            // Удаление записей из mtm.Config.xml 
            // секции installModules, секции namespace  и секции самого модуля
            // **********************************************

            string _absolutDirectory = GeneralMethods.MytripConfigurationDirectory();
            XDocument _doc = XDocument.Load(_absolutDirectory);
            try
            {
                _doc.Root.Elements("installModules").Elements("add").FirstOrDefault(x => x.Attribute("name").Value == moduleName).Remove();
            }
            catch { }
            try
            {
                _doc.Root.Elements(moduleName).Remove();
            }
            catch { }
            _doc.Save(_absolutDirectory);

            //****************** E N D **********************
            #endregion

            #region Удаление записей из /Views/Home/Partial/_profileBig.cshtml
            // **********************************************
            // Удаление записей из /Views/Home/Partial/_profileBig.cshtml
            // **********************************************

            string[] profile = EditePageRepository.WritePage("/Views/Home/Partial/_profileBig.cshtml");
            string a = string.Empty;
            foreach (string x in profile)
            {
                if (!x.Contains(_moduleName))
                    a += string.Concat(x.Trim(),"|");
            }
            profile = a.Replace(",|/*--EndLastActivity--*/", "|/*--EndLastActivity--*/").Split('|');
            EditePageRepository.CreatePage("/Views/Home/Partial/_profileBig.cshtml", profile);

            //****************** E N D **********************
            #endregion

            #region Удаление записей из /Views/Home/Partial/_profileSmall.cshtml
            // **********************************************
            // Удаление записей из /Views/Home/Partial/_profileSmall.cshtml
            // **********************************************

            profile = EditePageRepository.WritePage("/Views/Shared/Partial/_profileSmall.cshtml");
            a = string.Empty;
            foreach (string x in profile)
            {
                if (!x.Contains(_moduleName))
                    a += string.Concat(x.Trim(), "|");
            }
            profile = a.Replace(",|/*--EndProfile--*/", "|/*--EndProfile--*/").Split('|');
            EditePageRepository.CreatePage("/Views/Shared/Partial/_profileSmall.cshtml", profile);

            //****************** E N D **********************
            #endregion

            #region Удаление записей из /Views/Home/Partial/_homePage.cshtml
            // **********************************************
            // Удаление записей из /Views/Home/Partial/_homePage.cshtml
            // **********************************************

            profile = EditePageRepository.WritePage("/Views/Home/Partial/_homePage.cshtml");
            StringBuilder _profile = new StringBuilder();
            foreach (string x in profile)
            {
                if (!x.Contains(_moduleName))
                    _profile.AppendLine(x);
            }
            EditePageRepository.CreatePage("/Views/Home/Partial/_homePage.cshtml", _profile.ToString());

            //****************** E N D **********************
            #endregion

            #region Удаление записей из /Views/Shared/Partial/_menu.cshtml
            // **********************************************
            // Удаление записей из /Views/Shared/Partial/_menu.cshtml
            // **********************************************

            profile = EditePageRepository.WritePage("/Views/Shared/Partial/_menu.cshtml");
            a = string.Empty;
            foreach (string x in profile)
            {
                if (!x.Contains(_moduleName))
                    a += string.Concat(x.Trim(), "|");
            }
            profile = a.Replace(",|/*--EndMenu--*/", "|/*--EndMenu--*/").Split('|');
            EditePageRepository.CreatePage("/Views/Shared/Partial/_menu.cshtml", profile);

            //****************** E N D **********************
            #endregion
            #region Удаление записей из /Views/Shared/Partial/_menu.cshtml
            // **********************************************
            // Удаление записей из /Views/Shared/Partial/_menu.cshtml
            // **********************************************

            profile = EditePageRepository.WritePage("/Views/Shared/Partial/_sitemap.cshtml");
            a = string.Empty;
            foreach (string x in profile)
            {
                if (!x.Contains(_moduleName))
                    a += string.Concat(x.Trim(), "|");
            }
            profile = a.Replace(",|/*--EndSiteMap--*/", "|/*--EndSiteMap--*/").Split('|');
            EditePageRepository.CreatePage("/Views/Shared/Partial/_sitemap.cshtml", profile);

            //****************** E N D **********************
            #endregion
            #region Удаление записей из /Views/Shared/Partial/_sideBar.cshtml
            // **********************************************
            // Удаление записей из /Views/Shared/Partial/_sideBar.cshtml
            // **********************************************

            profile = EditePageRepository.WritePage("/Views/Shared/Partial/_sideBarProfile.cshtml");
            a = string.Empty;
            foreach (string x in profile)
            {
                if (!x.Contains(_moduleName))
                    a += string.Concat(x.Trim(), "|");
            }
            profile = a.Replace(",|/*--EndProfile--*/", "|/*--EndProfile--*/").Split('|');
            EditePageRepository.CreatePage("/Views/Shared/Partial/_sideBarProfile.cshtml", profile);

            //****************** E N D **********************
            #endregion

            #region Удаление записей из /Views/Shared/_sideBarExport.cshtml
            // **********************************************
            // Удаление записей из /Views/Shared/_sideBarExport.cshtml
            // **********************************************

            profile = EditePageRepository.WritePage("/Views/Shared/Partial/_sideBar.cshtml");
            _profile = new StringBuilder();
            foreach (string x in profile)
            {
                if (!x.Contains(_moduleName.Replace(".","")))
                    _profile.AppendLine(x);
            }
            EditePageRepository.CreatePage("/Views/Shared/Partial/_sideBar.cshtml", _profile.ToString());

            //****************** E N D **********************
            #endregion

            #region Удаление записей из /Views/Shared/Partial/_announce.cshtml
            // **********************************************
            // Удаление записей из /Views/Shared/Partial/_announce.cshtml
            // **********************************************

            profile = EditePageRepository.WritePage("/Views/Shared/Partial/_announce.cshtml");
            _profile = new StringBuilder();
            foreach (string x in profile)
            {
                if (!x.Contains(_moduleName))
                    _profile.AppendLine(x);
            }
            EditePageRepository.CreatePage("/Views/Shared/Partial/_announce.cshtml", _profile.ToString());

            //****************** E N D **********************
            #endregion

            #region Удаление записей из /Views/Core/ControlPanel.cshtml
            // **********************************************
            // Удаление записей из /Views/Core/ControlPanel.cshtml
            // **********************************************

            profile = EditePageRepository.WritePage("/Views/Core/ControlPanel.cshtml");
            _profile = new StringBuilder();
            foreach (string x in profile)
            {
                if (!x.Contains(_moduleName))
                    _profile.AppendLine(x);
            }
            EditePageRepository.CreatePage("/Views/Core/ControlPanel.cshtml", _profile.ToString());

            //****************** E N D **********************
            #endregion


            #region Удаление записей из /Views/Shared/Partial/_rss.cshtml
            // **********************************************
            // Удаление записей из /Views/Shared/Partial/_rss.cshtml
            // **********************************************

            profile = EditePageRepository.WritePage("/Views/Shared/Partial/_rss.cshtml");
            _profile = new StringBuilder();
            foreach (string x in profile)
            {
                if (!x.Contains(_moduleName))
                    _profile.AppendLine(x);
            }
            EditePageRepository.CreatePage("/Views/Shared/Partial/_rss.cshtml", _profile.ToString());

            //****************** E N D **********************
            #endregion
            #region Удаление записей из /Views/Home/Search.cshtml
            // **********************************************
            // Удаление записей из /Views/Home/Search.cshtml
            // **********************************************
            profile = EditePageRepository.WritePage("/Views/Home/Search.cshtml");
            a = string.Empty;
            foreach (string x in profile)
            {
                if (!x.Contains(_moduleName))
                    a += string.Concat(x.Trim(), "|");
            }
            profile = a.Replace(",|/*--EndLastActivity--*/", "|/*--EndLastActivity--*/").Split('|');
            EditePageRepository.CreatePage("/Views/Home/Search.cshtml", profile);

            //****************** E N D **********************
            #endregion
            #region Удаление записей из /Views/Home/Search.cshtml
            // **********************************************
            // Удаление записей из /Views/Home/Search.cshtml
            // **********************************************
            profile = EditePageRepository.WritePage("/Views/Web.config");
            a = string.Empty;
            foreach (string x in profile)
            {
                if (!x.Contains(moduleName))
                    a += string.Concat(x.Trim(), "|");
            }
            profile = a.Split('|');
            EditePageRepository.CreatePage("/Views/Web.config", profile);

            //****************** E N D **********************
            #endregion
        }
    }
}
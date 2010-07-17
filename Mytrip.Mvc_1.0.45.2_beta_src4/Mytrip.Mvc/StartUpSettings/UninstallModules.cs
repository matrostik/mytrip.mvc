using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Mytrip.Mvc.Repository;
using System.Text;

namespace Mytrip.Mvc.StartUpSettings
{
    public class UninstallModules
    {
        public void UninstallModule(string moduleName)
        {
            string _absolutDirectory = GeneralMethods.xmlAbsoluteDirectory("MytripConfiguration");
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var modules = _doc.Root.Elements("installModules");
            try
            {
                modules.Elements("add").FirstOrDefault(x => x.Attribute("name").Value == moduleName).Remove();
            }
            catch
            {
            }
            try
            {
                _doc.Root.Elements(moduleName).Remove();
            }
            catch
            {
            }
            _doc.Save(_absolutDirectory);
            string[] profile = EditePageRepository.WritePage("/Views/Home/ProfileBig.ascx");
            string a = string.Empty;
            foreach (string x in profile)
            {
                if (x.IndexOf(moduleName+".Export") == -1)
                    a += x.Trim() + "|";
            }
            a = a.Replace(",|//EndLastActivity", "|//EndLastActivity");
            profile = a.Split('|');
            EditePageRepository.CreatePage("/Views/Home/ProfileBig.ascx", profile);
            profile = EditePageRepository.WritePage("/Views/Home/ProfileSmall.ascx");
            a = string.Empty;
            foreach (string x in profile)
            {
                if (x.IndexOf(moduleName + ".Export") == -1)
                    a += x.Trim() + "|";
            }
            a = a.Replace(",|//EndProfile", "|//EndProfile");
            profile = a.Split('|');
            EditePageRepository.CreatePage("/Views/Home/ProfileSmall.ascx", profile);
            profile = EditePageRepository.WritePage("/Views/Home/HomePage.ascx");
            StringBuilder _profile = new StringBuilder();
            foreach (string x in profile)
            {
                if (x.IndexOf(moduleName + ".Export") == -1)
                    _profile.AppendLine(x);
            }
            EditePageRepository.CreatePage("/Views/Home/HomePage.ascx", _profile.ToString());
            profile = EditePageRepository.WritePage("/Views/Shared/Menu.ascx");
            a = string.Empty;
            foreach (string x in profile)
            {
                if (x.IndexOf(moduleName + ".Export") == -1)
                    a += x.Trim() + "|";
            }
            a = a.Replace(",|//EndMenu", "|//EndMenu");
            profile = a.Split('|');
            EditePageRepository.CreatePage("/Views/Shared/Menu.ascx", profile);
            profile = EditePageRepository.WritePage("/Views/Shared/SideBar.ascx");
            a = string.Empty;
            foreach (string x in profile)
            {
                if (x.IndexOf(moduleName + ".Export") == -1)
                    a += x.Trim() + "|";
            }
            a = a.Replace(",|//End", "|//End");
            profile = a.Split('|');
            EditePageRepository.CreatePage("/Views/Shared/SideBar.ascx", profile);
            string[] profile6 = EditePageRepository.WritePage("/Views/Shared/SideBarExport.ascx");
            StringBuilder _profile6 = new StringBuilder();
            foreach (string x in profile6)
            {
                if (x.IndexOf(moduleName + ".Export") == -1)
                    _profile6.AppendLine(x);
            }
            EditePageRepository.CreatePage("/Views/Shared/SideBarExport.ascx", _profile6.ToString());
        }
    }
}
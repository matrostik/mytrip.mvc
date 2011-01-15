/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about mytrip.mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;
using mtm.Core.Settings;
using System.Reflection;

namespace mtm.Core.Repository
{
    /// <summary> Методы для установки новых модулей
    /// </summary>
    internal class InstallModuleRepository
    {
        /// <summary>
        /// 
        /// </summary>
        internal void CreateProfileSmall()
        {
            IDictionary<string, string> a = CreateLinkForProfile();
            StringBuilder result = new StringBuilder();
            foreach (var x in a)
            {
                result.AppendLine(x.Value);

            }
            EditePageRepository.CreatePage("/Views/Shared/_profileSmall.cshtml", result.ToString());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="culture"></param>
        internal void CreateProfile(string culture)
        {
            string export = string.Empty;
            string _export = string.Empty;
            int count = 0;
            foreach (var x in ProfilePage(culture))
            {
                if (count == 0)
                    export = x.Key;
                else
                    export += "|" + x.Key;
                count++;
            }
            StringBuilder result = new StringBuilder();
            string[] sidebar = EditePageRepository.WritePage("/Views/Home/_profileBig.cshtml");
            result.AppendLine("@inherits System.Web.Mvc.WebViewPage<mtm.Core.Models.ProfileUsersModel>");
            result.AppendLine("@{");
            result.AppendLine("/* WARNING AUTO GENERATE CODE */");
            result.AppendLine(" ");
            result.AppendLine("/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich");
            result.AppendLine("To learn more about mytrip.mvc visit");
            result.AppendLine("http://mytripmvc.net  http://mytripmvc.codeplex.com");
            result.AppendLine("mytripmvc@gmail.com");
            result.AppendLine("license: Microsoft Public License (Ms-PL) */");
            result.AppendLine(" ");
            result.AppendLine("/* WARNING AUTO GENERATE CODE */");
            result.AppendLine("}");
            result.AppendLine("@Html.LastActivity(Model.Path, new {");
            result.AppendLine("/*--StartLastActivity--*/");
            bool end = false;
            foreach (string x in sidebar)
            {
                if (x.Contains("/*--EndLastActivity--*/"))
                    end = false;
                if (end && x.Contains("="))
                {
                    string _x = x.Remove(0, x.IndexOf("=") + 1);
                    _x = _x.Replace(",", "");
                    _x = _x.Trim();
                    export = export.Replace(_x, "");
                    export = export.Replace("||", "|");
                    _export += _x + "|";
                }
                else if (x.Contains("/*--StartLastActivity--*/"))
                    end = true;
            }
            _export += export;
            string[] _result = _export.Split('|');
            count = 1;
            foreach (string x in _result)
            {
                if (!string.IsNullOrEmpty(x))
                {
                    result.AppendLine("i" + count + "=" + x + ",");
                    count++;
                }
            }
            count = result.Length;
            result = result.Remove(count - 3, 1);
            result.AppendLine("/*--EndLastActivity--*/");
            result.AppendLine("})");
            EditePageRepository.CreatePage("/Views/Home/_profileBig.cshtml", result.ToString());
        }
        internal void CreateRss()
        {
            StringBuilder result = new StringBuilder();
            foreach (var a in RssLink())
            {
                result.AppendLine("@" + a.Value);
            }
            EditePageRepository.CreatePage("/Views/Shared/_rss.cshtml", result.ToString());
        }
        /// <summary>
        /// 
        /// </summary>
        internal void CreateSideBarProfile()
        {
            IDictionary<string, string> a = CreateLinkForSideBarProfile();
            StringBuilder result = new StringBuilder();
            foreach (var x in a)
            {
                result.AppendLine(x.Value);

            }
            EditePageRepository.CreatePage("/Views/Shared/_sideBarProfile.cshtml", result.ToString());
        }

        internal void CreateSearch()
        {
            IDictionary<string, string> a = SearchPage(LocalisationSetting.culture());
            string path = "/Views/Home/Search.cshtml";
            string[] page = EditePageRepository.WritePage(path);
            StringBuilder result = new StringBuilder();
            StringBuilder _result = new StringBuilder();
            int id = 1;
            int count = a.Count();
            string end = ",";
            foreach (var x in a)
            {
                if (id == count)
                    end = "";
                _result.AppendLine("i" + id + "=" + x.Key + end);
                id++;

            }
            bool stop = true;
            foreach (string x in page)
            {
                if (x.Contains("/*--EndLastActivity--*/"))
                    stop = true;
                if (stop)
                {
                    result.AppendLine(x);
                    if (x.Contains("/*--StartLastActivity--*/"))
                    {
                        result.AppendLine(_result.ToString().Trim());
                        stop = false;
                    }
                }
            }

            EditePageRepository.CreatePage("/Views/Home/Search.cshtml", result.ToString());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="culture"></param>
        internal void CreateMenu(string culture)
        {
            string export = string.Empty;
            string _export = string.Empty;
            int count = 0;
            foreach (var x in MenuPage(culture))
            {
                if (count == 0)
                    export = x.Key;
                else
                    export += "|" + x.Key;
                count++;
            }
            StringBuilder result = new StringBuilder();
            string[] sidebar = EditePageRepository.WritePage("/Views/Shared/_menu.cshtml");
            result.AppendLine("@inherits System.Web.Mvc.WebViewPage");
            result.AppendLine("@{");
            result.AppendLine("/* WARNING AUTO GENERATE CODE */");
            result.AppendLine(" ");
            result.AppendLine("/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich");
            result.AppendLine("To learn more about mytrip.mvc visit");
            result.AppendLine("http://mytripmvc.net  http://mytripmvc.codeplex.com");
            result.AppendLine("mytripmvc@gmail.com");
            result.AppendLine("license: Microsoft Public License (Ms-PL) */");
            result.AppendLine(" ");
            result.AppendLine("/* WARNING AUTO GENERATE CODE */");
            result.AppendLine("}");
            result.AppendLine("@Html.MytripMenu(new {");
            result.AppendLine("/*--StartMenu--*/");
            bool end = false;
            foreach (string x in sidebar)
            {
                if (x.IndexOf("/*--EndMenu--*/") != -1)
                    end = false;
                if (end && x.Contains("="))
                {
                    string _x = x.Remove(0, x.IndexOf("=") + 1);
                    _x = _x.Replace(",", "");
                    _x = _x.Trim();
                    export = export.Replace(_x, "");
                    export = export.Replace("||", "|");
                    _export += _x + "|";
                }
                else if (x.IndexOf("/*--StartMenu--*/") != -1)
                    end = true;
            }
            _export += export;
            string[] _result = _export.Split('|');
            count = 1;
            foreach (string x in _result)
            {
                if (!string.IsNullOrEmpty(x))
                {
                    result.AppendLine("i" + count + "=" + x + ",");
                    count++;
                }
            }
            count = result.Length;
            result = result.Remove(count - 3, 1);
            result.AppendLine("/*--EndMenu--*/");
            result.AppendLine("})");
            EditePageRepository.CreatePage("/Views/Shared/_menu.cshtml", result.ToString());
        }
        /// <summary>
        /// 
        /// </summary>
        internal void CreateControlPanel()
        {
            IDictionary<string, string> a = CreateLinkForControlPanel();
            StringBuilder result = new StringBuilder();
            foreach (var x in a)
            {
                result.AppendLine(x.Value);

            }
            EditePageRepository.CreatePage("/Views/Core/ControlPanel.cshtml", result.ToString());
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dll"></param>
        /// <param name="culture"></param>
        internal void CreateDataBase(string dll, string culture)
        {
            string domain = "http://" + HttpContext.Current.Request.ServerVariables["HTTP_HOST"];
            string _dll = dll.Replace(".", "_");
            GeneralMethods.HttpGetParsing(domain + "/" + _dll + "Export/InstallModule", culture);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal IDictionary<string, List<string>> alldllMassiveForUninstall()
        {
            IDictionary<string, List<string>> _result = new Dictionary<string, List<string>>();
            string _absolutDirectory1 = GeneralMethods.MytripConfigurationDirectory();
            string absolutDirectory = HttpContext.Current.Server.MapPath("/bin");
            XDocument _doc = XDocument.Load(_absolutDirectory1);
            try
            {
                int count = 1;
                var core = _doc.Root.Elements("installModules").Elements("add");
                foreach (var x in core)
                {
                    Assembly assembly = Assembly.LoadFrom(Path.Combine(absolutDirectory, x.Attribute("name").Value + ".dll"));
                    string attTitle = ((AssemblyTitleAttribute)assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false)[0]).Title;
                    string attDesc = ((AssemblyDescriptionAttribute)assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false)[0]).Description;
                    string attComp = ((AssemblyCompanyAttribute)assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false)[0]).Company;
                    string attVer = assembly.GetName().Version.ToString();
                    List<string> info = new List<string>(4) { attTitle, attVer, attComp, attDesc };
                    _result.Add(x.Attribute("name").Value, info);
                    count++;
                }
            }
            catch { }
            return _result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal IDictionary<string, List<string>> alldllMassiveForModules()
        {
            string _absolutDirectory1 = GeneralMethods.MytripConfigurationDirectory();
            string absolutDirectory = HttpContext.Current.Server.MapPath("/bin");
            string modules = string.Empty;
            DirectoryInfo _absolutDirectory = new DirectoryInfo(absolutDirectory);
            if (!_absolutDirectory.Exists)
            {
                _absolutDirectory.Create();
            }
            XDocument _doc = XDocument.Load(_absolutDirectory1);
            try
            {
                var core = _doc.Root.Elements("installModules").Elements("add");
                foreach (var x in core)
                {
                    modules += x.Attribute("name").Value;
                }
            }
            catch { }
            FileInfo[] result = _absolutDirectory.GetFiles();
            IDictionary<string, List<string>> _result = new Dictionary<string, List<string>>();
            foreach (FileInfo x in result)
            {
                if (x.Extension == ".dll" && !modules.Contains(x.Name.Replace(".dll", "")))
                {
                    Assembly assembly = Assembly.LoadFrom(x.FullName);
                    string attTitle = ((AssemblyTitleAttribute)assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false)[0]).Title;
                    string attDesc = ((AssemblyDescriptionAttribute)assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false)[0]).Description;
                    string attComp = ((AssemblyCompanyAttribute)assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false)[0]).Company;
                    string product = ((AssemblyProductAttribute)assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false)[0]).Product;
                    if (product.Contains("module") && product.Contains("mytrip.mvc"))
                    {
                        string attVer = assembly.GetName().Version.ToString();
                        List<string> info = new List<string>(4) { attTitle, attVer, attComp, attDesc };
                        _result.Add(x.Name.Replace(".dll", ""), info);
                    }
                }
            }
            return _result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        internal IDictionary<string, string> HomePage(string culture)
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            int count = 1;
            ICoreRepository core = new ICoreRepository();
            var _x = core.corePageRepo.GetPagesForHomePage();
            foreach (var x in _x)
            {
                string _culture = "globe_";
                if (!x.AllCulture)
                    _culture = x.Culture.ToLower() + "_";
                string body = x.PageId + "_" + _culture + x.Title;
                result.Add(string.Concat(count, "_", "mytrip.mvc"), body.Trim());
                count++;
            }
            foreach (var x in alldllMassive())
            {
                string parser = GeneralMethods.HttpGetParsing(x.Value + "/HomePage", culture);
                string name = x.Value.Replace("http://" + HttpContext.Current.Request.ServerVariables["HTTP_HOST"] + "/", "");
                name = name.Replace("Export", "").Replace("_", ".");
                parser = parser.Replace("<?xml version=\"1.0\" encoding=\"utf-8\" ?>", "")
                    .Replace("<root_el>", "").Replace("<first_el>", "")
                    .Replace("</first_el>", "").Replace("</root_el>", "");
                if (parser != null && parser != "")
                {
                    string[] _parser = parser.Split('|');
                    foreach (string q in _parser)
                    {
                        if (q != null && q.Trim().Length > 2)
                        {
                            result.Add(string.Concat(count, "_", name), q.Trim());
                            count++;
                        }
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        internal IDictionary<string, string> MenuPage(string culture)
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            foreach (var x in alldllMassive())
            {
                string parser = GeneralMethods.HttpGetParsing(x.Value + "/MenuPage", culture);
                string name = x.Value.Replace("http://" + HttpContext.Current.Request.ServerVariables["HTTP_HOST"] + "/", "");
                name = name.Replace("Export", "").Replace("_", ".");
                if (parser != null && parser != "")
                {
                    string[] _parser = parser.Split('|');
                    foreach (string q in _parser)
                    {
                        string[] _q = q.Split('_');
                        int count = 1;
                        string _name = string.Empty;
                        string _name2 = string.Empty;
                        foreach (string w in _q)
                        {
                            if (count == 1)
                                _name = string.Concat(name, ".Export.", w, "()");
                            else
                                _name2 = w;
                            count++;
                        }
                        result.Add(_name, _name2);

                    }
                }
            }
            return result;
        }
        internal IDictionary<string, string> AnoncePage(string culture)
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            foreach (var x in alldllMassive())
            {
                string parser = GeneralMethods.HttpGetParsing(x.Value + "/AnoncePage", culture);
                string name = x.Value.Replace("http://" + HttpContext.Current.Request.ServerVariables["HTTP_HOST"] + "/", "");
                name = name.Replace("Export", "").Replace("_", ".");
                if (parser != null && parser != "")
                {
                    string[] _parser = parser.Split('|');
                    foreach (string q in _parser)
                    {
                        string[] _q = q.Split('_');
                        int count = 1;
                        string _name = string.Empty;
                        string _name2 = string.Empty;
                        foreach (string w in _q)
                        {
                            if (count == 1)
                                _name = string.Concat(name, ".Export.", w, "()");
                            else
                                _name2 = w;
                            count++;
                        }
                        result.Add(_name, _name2);

                    }
                }
            }
            return result;
        }
        internal IDictionary<string, string> SearchPage(string culture)
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            foreach (var x in alldllMassive())
            {
                string parser = GeneralMethods.HttpGetParsing(x.Value + "/SearchPage", culture);
                string name = x.Value.Replace("http://" + HttpContext.Current.Request.ServerVariables["HTTP_HOST"] + "/", "");
                name = name.Replace("Export", "").Replace("_", ".");
                if (parser != null && parser != "")
                {
                    string[] _parser = parser.Split('|');
                    foreach (string q in _parser)
                    {
                        string[] _q = q.Split('_');
                        int count = 1;
                        string _name = string.Empty;
                        string _name2 = string.Empty;
                        foreach (string w in _q)
                        {
                            if (count == 1)
                                _name = string.Concat(name, ".Export.", w, "(Model.search)");
                            else
                                _name2 = w;
                            count++;
                        }
                        result.Add(_name, _name2);

                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        internal IDictionary<string, string> SideBarPage(string culture)
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            result.Add("Html.AccordionDonateProject()", CoreLanguage.donate);
            result.Add("Html.AccordionLatestUser()", CoreLanguage.RecentVisitors);
            result.Add("Html.AccordionSearch()", CoreLanguage.search);
            result.Add("Html.Partial(\"_sideBarProfile\")", CoreLanguage.my_profile);
            result.Add("Html.AccordionPage()", CoreLanguage.CorePageForSidebar);
            foreach (var x in alldllMassive())
            {
                string parser = GeneralMethods.HttpGetParsing(x.Value + "/SideBarPage", culture);
                string name = x.Value.Replace("http://" + HttpContext.Current.Request.ServerVariables["HTTP_HOST"] + "/", "");
                name = name.Replace("Export", "").Replace("_", ".");
                if (parser != null && parser != "")
                {
                    string[] _parser = parser.Split('|');
                    foreach (string q in _parser)
                    {
                        string[] _q = q.Split('_');
                        int count = 1;
                        string _name = string.Empty;
                        string _name2 = string.Empty;
                        foreach (string w in _q)
                        {
                            if (count == 1)
                                _name = string.Concat(name, ".Export.", w, "()");
                            else
                                _name2 = w;
                            count++;
                        }
                        result.Add(_name, _name2);
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IDictionary<int, string> alldllMassive()
        {
            IDictionary<int, string> _result = new Dictionary<int, string>();
            string _absolutDirectory1 = GeneralMethods.MytripConfigurationDirectory();
            string domain = string.Concat("http://",HttpContext.Current.Request.ServerVariables["HTTP_HOST"]);
            XDocument _doc = XDocument.Load(_absolutDirectory1);
            try
            {
                int count = 1;
                var core = _doc.Root.Elements("installModules").Elements("add");
                foreach (var x in core)
                {
                    string _x = x.Attribute("name").Value.Replace(".", "_");
                    _x =string.Concat(domain,"/",_x,"Export");
                    _result.Add(count, _x);
                    count++;
                }
            }
            catch { }
            return _result;
        }        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IDictionary<string, string> alldllMassiveForLink()
        {

            string absolutDirectory = HttpContext.Current.Server.MapPath("/bin");
            IDictionary<string, string> _result = new Dictionary<string, string>();
            string _absolutDirectory1 = GeneralMethods.MytripConfigurationDirectory();
            string domain = "http://" + HttpContext.Current.Request.ServerVariables["HTTP_HOST"];
            XDocument _doc = XDocument.Load(_absolutDirectory1);
            try
            {
                var core = _doc.Root.Elements("installModules").Elements("add");
                foreach (var x in core)
                {
                    string _x = absolutDirectory + "/" + x.Attribute("name").Value + ".dll";
                    _result.Add(x.Attribute("name").Value, _x);
                }
            }
            catch { }
            return _result;
        }        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IDictionary<string, string> ProfileLink()
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            int count = 1;
            foreach (var x in alldllMassiveForLink())
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFile(x.Value);
                var a = assembly.GetType(x.Key + ".Export");
                var d = a.GetMethods();
                foreach (var _x in d)
                {
                    if (_x.Name.IndexOf("Profile") != -1)
                    {
                        result.Add(count.ToString(), x.Key + ".Export." + _x.Name + "()");
                        count++;
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IDictionary<string, string> ManagerLink()
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            int count = 1;
            foreach (var x in alldllMassiveForLink())
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFile(x.Value);
                var a = assembly.GetType(x.Key + ".Export");
                var d = a.GetMethods();
                foreach (var _x in d)
                {
                    if (_x.Name.IndexOf("Manager") != -1)
                    {
                        result.Add(count.ToString(), x.Key + ".Export." + _x.Name + "()");
                        count++;
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IDictionary<string, string> SettingLink()
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            int count = 1;
            foreach (var x in alldllMassiveForLink())
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFile(x.Value);
                var a = assembly.GetType(x.Key + ".Export");
                var d = a.GetMethods();
                foreach (var _x in d)
                {
                    if (_x.Name.IndexOf("Setting") != -1)
                    {
                        result.Add(count.ToString(), x.Key + ".Export." + _x.Name + "()");
                        count++;
                    }
                }
            }
            return result;
        }
        private IDictionary<string, string> RssLink()
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            int count = 1;
            foreach (var x in alldllMassiveForLink())
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFile(x.Value);
                var a = assembly.GetType(x.Key + ".Export");
                var d = a.GetMethods();
                foreach (var _x in d)
                {
                    if (_x.Name.IndexOf("RssLink") != -1)
                    {
                        result.Add(count.ToString(), x.Key + ".Export." + _x.Name + "()");
                        count++;
                    }
                }
            }
            return result;
        }        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IDictionary<string, string> CreateLinkForProfile()
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            string[] sidebar = EditePageRepository.WritePage("/Views/Shared/_profileSmall.cshtml");
            int count = 1;
            int StartProfile = 0;
            int EndProfile = 0;
            foreach (string x in sidebar)
            {
                if (x.Contains("/*--StartProfile--*/"))
                {
                    result.Add(count.ToString(), x);
                    StartProfile = count;
                }
                else if (x.Contains("/*--EndProfile--*/"))
                {
                    int _count = 1;
                    IDictionary<string, string> managrtlink = ProfileLink();
                    int _countmanaget = managrtlink.Count();
                    foreach (var manager in managrtlink)
                    {
                        string end = ",";
                        if (_count == _countmanaget)
                            end = string.Empty;
                        result.Add(count.ToString(), "i" + _count + "=" + manager.Value + end);
                        count++;
                        _count++;
                    }
                    result.Add(count.ToString(), x);
                    EndProfile = count;
                }


                else if ((StartProfile == 0 || EndProfile > 0))
                {
                    result.Add(count.ToString(), x);
                }
                count++;
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IDictionary<string, string> CreateLinkForControlPanel()
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            string[] sidebar = EditePageRepository.WritePage("/Views/Core/ControlPanel.cshtml");
            int count = 1;
            int StartManagerControlPanel = 0;
            int EndManagerControlPanel = 0;
            int StartSettingControlPanel = 0;
            int EndSettingControlPanel = 0;
            foreach (string x in sidebar)
            {
                if (x.Contains("/*--StartManagerControlPanel--*/"))
                {
                    result.Add(count.ToString(), x);
                    StartManagerControlPanel = count;
                }
                else if (x.Contains("/*--EndManagerControlPanel--*/"))
                {
                    IDictionary<string, string> managrtlink = ManagerLink();
                    int _countmanaget = managrtlink.Count();
                    foreach (var manager in managrtlink)
                    {
                        result.Add(count.ToString(), "@" + manager.Value);
                        count++;
                    }
                    result.Add(count.ToString(), x);
                    EndManagerControlPanel = count;
                }
                else if (x.Contains("/*--StartSettingControlPanel--*/"))
                {
                    result.Add(count.ToString(), x);
                    StartSettingControlPanel = count;
                }
                else if (x.Contains("/*--EndSettingControlPanel--*/"))
                {
                    IDictionary<string, string> managrtlink = SettingLink();
                    int _countmanaget = managrtlink.Count();
                    foreach (var manager in managrtlink)
                    {
                        result.Add(count.ToString(), "@" + manager.Value);
                        count++;
                    }
                    result.Add(count.ToString(), x);
                    EndSettingControlPanel = count;
                }
                else if ((StartManagerControlPanel == 0 || EndManagerControlPanel > 0)
                    && (StartSettingControlPanel == 0 || EndSettingControlPanel > 0))
                {
                    result.Add(count.ToString(), x);
                }
                count++;
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        private IDictionary<string, string> ProfilePage(string culture)
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            foreach (var x in alldllMassive())
            {
                string parser = GeneralMethods.HttpGetParsing(x.Value + "/ProfilePage", culture);
                string name = x.Value.Replace("http://" + HttpContext.Current.Request.ServerVariables["HTTP_HOST"] + "/", "");
                name = name.Replace("Export", "");
                name = name.Replace("_", ".");
                if (parser != null && parser != "")
                {
                    string[] _parser = parser.Split('|');
                    foreach (string q in _parser)
                    {
                        string[] _q = q.Split('_');
                        int count = 1;
                        string _name = string.Empty;
                        string _name2 = string.Empty;
                        foreach (string w in _q)
                        {
                            if (count == 1)
                                _name = name + ".Export." + w + "(Model.UserName)";
                            else
                                _name2 = w;
                            count++;
                        }
                        result.Add(_name, _name2);

                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IDictionary<string, string> CreateLinkForSideBarProfile()
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            string[] sidebar = EditePageRepository.WritePage("/Views/Shared/_sideBarProfile.cshtml");
            int count = 1;
            int StartProfile = 0;
            int EndProfile = 0;
            foreach (string x in sidebar)
            {
                if (x.Contains("/*--StartProfile--*/"))
                {
                    result.Add(count.ToString(), x);
                    StartProfile = count;
                }
                else if (x.Contains("/*--EndProfile--*/"))
                {
                    int _count = 1;
                    IDictionary<string, string> managrtlink = ProfileLink();
                    int _countmanaget = managrtlink.Count();
                    foreach (var manager in managrtlink)
                    {
                        string end = ",";
                        if (_count == _countmanaget)
                            end = string.Empty;
                        result.Add(count.ToString(), "i" + _count + "=" + manager.Value + end);
                        count++;
                        _count++;
                    }
                    result.Add(count.ToString(), x);
                    EndProfile = count;
                }

                else if (StartProfile == 0 || EndProfile > 0)
                {
                    result.Add(count.ToString(), x);
                }
                count++;
            }
            return result;
        }
        
    }
}
/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about Mytrip.Mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Linq;
using Mytrip.Mvc.Settings;

namespace Mytrip.Mvc.Repository
{
    /// <summary> Методы для установки новых модулей
    /// </summary>
    internal class InstallModuleRepository
    { 
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal IDictionary<int, string> alldllMassive()
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
        internal IDictionary<int, string> alldllMassiveForUninstall()
        {
            IDictionary<int, string> _result = new Dictionary<int, string>();
            string _absolutDirectory1 = GeneralMethods.MytripConfigurationDirectory();
            XDocument _doc = XDocument.Load(_absolutDirectory1);
            try
            {
                int count = 1;
                var core = _doc.Root.Elements("installModules").Elements("add");
                foreach (var x in core)
                {
                    _result.Add(count, x.Attribute("name").Value);
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
        internal IDictionary<string, string> alldllMassiveForLink()
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
        internal string noModuledll()
        {

            string _result = string.Empty;
            string _absolutDirectory = GeneralMethods.MytripConfigurationDirectory();
            XDocument _doc = XDocument.Load(_absolutDirectory);
            try
            {
                var core = _doc.Root.Elements("no.module.dll").Elements("add");
                foreach (var x in core)
                {
                    _result+=string.Concat("|",x.Attribute("name").Value);
                }
            }
            catch { }
            return _result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal IDictionary<string, bool> alldllMassiveForModules()
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
            IDictionary<string, bool> _result = new Dictionary<string, bool>();
            foreach (FileInfo x in result)
            {
                if (x.Extension == ".dll" && !noModuledll().Contains(x.Name) 
                    && !modules.Contains(x.Name.Replace(".dll", "")))
                {
                    bool _x = false;
                    _result.Add(x.Name.Replace(".dll", ""), _x);
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
            foreach (var x in alldllMassive())
            {
                string parser = GeneralMethods.HttpGetParsing(x.Value + "/HomePage", culture);
                string name = x.Value.Replace("http://" + HttpContext.Current.Request.ServerVariables["HTTP_HOST"] + "/", "");
                name = name.Replace("Export", "").Replace("_", ".");
                if (parser != null && parser != "")
                {
                    string[] _parser = parser.Split('|');
                    foreach (string q in _parser)
                    {
                        result.Add(string.Concat(count, "_", name), q);
                        count++;
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
                                _name = string.Concat(name,".Export.",w,"()");
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
        internal IDictionary<string, string> ProfileLink()
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
        internal IDictionary<string, string> ManagerLink()
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
        internal IDictionary<string, string> SettingLink()
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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IDictionary<string, string> CreateLinkForProfile()
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            string[] sidebar = EditePageRepository.WritePage("/Views/Home/_profileSmall.cshtml");
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
        internal void CreateProfileSmall()
        {
            IDictionary<string, string> a = CreateLinkForProfile();
            StringBuilder result = new StringBuilder();
            foreach (var x in a)
            {
                result.AppendLine(x.Value);

            }
            EditePageRepository.CreatePage("/Views/Home/_profileSmall.cshtml", result.ToString());
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
            result.AppendLine("@inherits System.Web.Mvc.WebViewPage<Mytrip.Mvc.Models.ProfileUsersModel>");
            result.AppendLine("@{");
            result.AppendLine("/* WARNING AUTO GENERATE CODE */");
            result.AppendLine(" ");
            result.AppendLine("/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich");
            result.AppendLine("To learn more about Mytrip.Mvc visit");
            result.AppendLine("http://mytripmvc.net  http://mytripmvc.codeplex.com");
            result.AppendLine("mytripmvc@gmail.com");
            result.AppendLine("license: Microsoft Public License (Ms-PL) */");
            result.AppendLine(" ");
            result.AppendLine("/* WARNING AUTO GENERATE CODE */");
            result.AppendLine("}");
            result.AppendLine("@Html.LastActivity(new {");
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        internal IDictionary<string, string> ProfilePage(string culture)
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
        private IDictionary<string, string> CreateLinkForSideBar()
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            string[] sidebar = EditePageRepository.WritePage("/Views/Shared/_sideBar.cshtml");
            int count = 1;
            int StartProfile = 0;
            int EndProfile = 0;
            int StartManagerControlPanel = 0;
            int EndManagerControlPanel = 0;
            int StartSettingControlPanel = 0;
            int EndSettingControlPanel = 0;
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
                else if (x.Contains("/*--StartManagerControlPanel--*/"))
                {
                    result.Add(count.ToString(), x);
                    StartManagerControlPanel = count;
                }
                else if (x.Contains("/*--EndManagerControlPanel--*/"))
                {
                    int _count = 1;
                    IDictionary<string, string> managrtlink = ManagerLink();
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
                    EndManagerControlPanel = count;
                }
                else if (x.Contains("/*--StartSettingControlPanel--*/"))
                {
                    result.Add(count.ToString(), x);
                    StartSettingControlPanel = count;
                }
                else if (x.Contains("/*--EndSettingControlPanel--*/"))
                {
                    int _count = 1;
                    IDictionary<string, string> managrtlink = SettingLink();
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
                    EndSettingControlPanel = count;
                }
                else if ((StartProfile == 0 || EndProfile > 0)
                    && (StartManagerControlPanel == 0 || EndManagerControlPanel > 0)
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
        internal void CreateSideBar()
        {
            IDictionary<string, string> a = CreateLinkForSideBar();
            StringBuilder result = new StringBuilder();
            foreach (var x in a)
            {
                result.AppendLine(x.Value);

            }
            EditePageRepository.CreatePage("/Views/Shared/_sideBar.cshtml", result.ToString());
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
            result.AppendLine("To learn more about Mytrip.Mvc visit");  
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
                        result.Add(count.ToString(), "@"+ manager.Value);
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
            GeneralMethods.HttpGetParsing(domain + "/" + _dll + "Export/CreateDataBaseAndXml", culture);

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Text;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Mytrip.Mvc.Repository
{
    public class HelperRepository
    {

        public string _RssParsing(string url, string culture)
        {
            try
            {
                string retVal = string.Empty;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "GET";
                request.Headers.Add("Accept-Language:" + culture);
                request.Headers.Add(HttpRequestHeader.Cookie, "myTripCulture=" + culture.ToLower());
                request.Headers.Add("Accept-Encoding:gzip, deflate");
                request.Accept = "*/*";
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E; InfoPath.3)";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader streamReader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(response.CharacterSet)))
                {
                    retVal = streamReader.ReadToEnd();
                }
                response.Close();
                return retVal;
            }
            catch { return string.Empty; }
        }
        public IDictionary<int, string> alldllMassive()
        {
            IDictionary<int, string> _result = new Dictionary<int, string>();
            string _absolutDirectory1 = GeneralMethods.xmlAbsoluteDirectory("MytripConfiguration");
            string domain = "http://" + HttpContext.Current.Request.ServerVariables["HTTP_HOST"];
            XDocument _doc = XDocument.Load(_absolutDirectory1);
            try
            {
                int count = 1;
                var core = _doc.Root.Elements("installModules").Elements("add");
                foreach (var x in core)
                {
                    string _x = x.Attribute("name").Value.Replace(".", "_");
                    _x = domain + "/" + _x + "Export";
                    _result.Add(count, _x);
                    count++;
                }
            }
            catch { }
            return _result;
        }
        public IDictionary<int, string> alldllMassiveForUninstall()
        {
            IDictionary<int, string> _result = new Dictionary<int, string>();
            string _absolutDirectory1 = GeneralMethods.xmlAbsoluteDirectory("MytripConfiguration");
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
        public IDictionary<string, string> alldllMassiveForLink()
        {

            string absolutDirectory = HttpContext.Current.Server.MapPath("/bin");
            IDictionary<string, string> _result = new Dictionary<string, string>();
            string _absolutDirectory1 = GeneralMethods.xmlAbsoluteDirectory("MytripConfiguration");
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
        public IDictionary<string, bool> alldllMassiveForModules()
        {
            string _absolutDirectory1 = GeneralMethods.xmlAbsoluteDirectory("MytripConfiguration");
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
                if (x.Extension == ".dll" && x.Name != "Mytrip.Mvc.dll"
                    && x.Name != "MySql.Data.dll" && x.Name != "MySql.Data.Entity.dll"
                    && x.Name != "System.Web.Mvc.dll" && modules.IndexOf(x.Name.Replace(".dll", "")) == -1)
                {
                    bool _x = false;
                    _result.Add(x.Name.Replace(".dll", ""), _x);
                }
            }
            return _result;
        }
        public IDictionary<string, string> HomePage(string culture)
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            int count = 1;
            foreach (var x in alldllMassive())
            {
                string parser = _RssParsing(x.Value + "/HomePage", culture);
                string name = x.Value.Replace("http://" + HttpContext.Current.Request.ServerVariables["HTTP_HOST"] + "/", "");
                name = name.Replace("Export", "");
                name = name.Replace("_", ".");
                if (parser != null && parser != "")
                {
                    string[] _parser = parser.Split('|');
                    foreach (string q in _parser)
                    {
                        result.Add(count + "_" + name, q);
                        count++;
                    }
                }
            }
            return result;
        }
        public IDictionary<string, string> MenuPage(string culture)
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            foreach (var x in alldllMassive())
            {
                string parser = _RssParsing(x.Value + "/MenuPage", culture);
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
                                _name = name + ".Export." + w + "()";
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
        public IDictionary<string, string> SideBarPage(string culture)
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            foreach (var x in alldllMassive())
            {
                string parser = _RssParsing(x.Value + "/SideBarPage", culture);
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
                                _name = name + ".Export." + w + "()";
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
        public IDictionary<string, string> ProfileLink()
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
        public IDictionary<string, string> ManagerLink()
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
        public IDictionary<string, string> SettingLink()
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
        private IDictionary<string, string> CreateLinkForSideBar()
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            string[] sidebar = EditePageRepository.WritePage("/Views/Shared/SideBar.ascx");
            int count = 1;
            int StartProfile = 0;
            int EndProfile = 0;
            int StartManagerControlPanel = 0;
            int EndManagerControlPanel = 0;
            int StartSettingControlPanel = 0;
            int EndSettingControlPanel = 0;
            foreach (string x in sidebar)
            {
                if (x.IndexOf("StartProfile") != -1)
                {
                    result.Add(count.ToString(), x);
                    StartProfile = count;
                }
                else if (x.IndexOf("EndProfile") != -1)
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
                else if (x.IndexOf("StartManagerControlPanel") != -1)
                {
                    result.Add(count.ToString(), x);
                    StartManagerControlPanel = count;
                }
                else if (x.IndexOf("EndManagerControlPanel") != -1)
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
                else if (x.IndexOf("StartSettingControlPanel") != -1)
                {
                    result.Add(count.ToString(), x);
                    StartSettingControlPanel = count;
                }
                else if (x.IndexOf("EndSettingControlPanel") != -1)
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
        public void CreateSideBar()
        {
            IDictionary<string, string> a = CreateLinkForSideBar();
            StringBuilder result = new StringBuilder();
            foreach (var x in a)
            {
                result.AppendLine(x.Value);

            }
            EditePageRepository.CreatePage("/Views/Shared/SideBar.ascx", result.ToString());
        }
        public void CreateDataBase(string dll, string culture)
        {
            string domain = "http://" + HttpContext.Current.Request.ServerVariables["HTTP_HOST"];
            string _dll = dll.Replace(".", "_");
            _RssParsing(domain + "/" + _dll + "Export/CreateDataBaseAndXml", culture);

        }
        public void CreateSideBarExport(string culture)
        {
            string export = string.Empty;
            int count = 0;
            foreach (var x in SideBarPage(culture))
            {
                if (count == 0)
                    export = x.Key;
                else
                    export += "|" + x.Key;
                count++;
            }
            StringBuilder _result = new StringBuilder();
            string[] sidebar = EditePageRepository.WritePage("/Views/Shared/SideBarExport.ascx");
            foreach (string x in sidebar)
            {
                if (!string.IsNullOrEmpty(x))
                {
                    _result.AppendLine(x);
                    string result = x.Replace("<%", "");
                    result = result.Replace("=", "");
                    result = result.Replace("%>", "");
                    result = result.Trim();
                    export = export.Replace(result, "");
                    export = export.Replace("||", "|");
                }
            }
            string[] _sidebar = export.Split('|');
            foreach (string x in _sidebar)
            {
                if (!string.IsNullOrEmpty(x))
                    _result.AppendLine("<%=" + x + "%>");
            }
            EditePageRepository.CreatePage("/Views/Shared/SideBarExport.ascx", _result.ToString());
        }
        public void CreateMenu(string culture)
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
            string[] sidebar = EditePageRepository.WritePage("/Views/Shared/Menu.ascx");
            result.AppendLine("<%@ Control Language=\"C#\" Inherits=\"System.Web.Mvc.ViewUserControl\" %>");
            result.AppendLine("<%= Html.MytripMenu(new {");
            result.AppendLine("//StartMenu");
            bool end = false;
            foreach (string x in sidebar)
            {
                if (end && x.IndexOf("=") != -1)
                {
                    string _x = x.Remove(0, x.IndexOf("=") + 1);
                    _x = _x.Replace(",", "");
                    _x = _x.Trim();
                    export = export.Replace(_x, "");
                    export = export.Replace("||", "|");
                    _export += _x + "|";
                }
                else if (x.IndexOf("StartMenu") != -1)
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
            result.AppendLine("//EndMenu");
            result.AppendLine("})%>");
            EditePageRepository.CreatePage("/Views/Shared/Menu.ascx", result.ToString());
        }
        private IDictionary<string, string> CreateLinkForProfile()
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            string[] sidebar = EditePageRepository.WritePage("/Views/Home/ProfileSmall.ascx");
            int count = 1;
            int StartProfile = 0;
            int EndProfile = 0;
            foreach (string x in sidebar)
            {
                if (x.IndexOf("StartProfile") != -1)
                {
                    result.Add(count.ToString(), x);
                    StartProfile = count;
                }
                else if (x.IndexOf("EndProfile") != -1)
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
        public void CreateProfileSmall()
        {
            IDictionary<string, string> a = CreateLinkForProfile();
            StringBuilder result = new StringBuilder();
            foreach (var x in a)
            {
                result.AppendLine(x.Value);

            }
            EditePageRepository.CreatePage("/Views/Home/ProfileSmall.ascx", result.ToString());
        }
        public void CreateProfile(string culture)
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
            string[] sidebar = EditePageRepository.WritePage("/Views/Home/ProfileBig.ascx");
            result.AppendLine("<%@ Control Language=\"C#\" Inherits=\"System.Web.Mvc.ViewUserControl<Mytrip.Mvc.Models.ProfileUsersModel>\" %>");
            result.AppendLine("<%=Html.LastActivity(new {");
            result.AppendLine("//StartLastActivity");
            bool end = false;
            foreach (string x in sidebar)
            {
                if (end && x.IndexOf("=") != -1)
                {
                    string _x = x.Remove(0, x.IndexOf("=") + 1);
                    _x = _x.Replace(",", "");
                    _x = _x.Trim();
                    export = export.Replace(_x, "");
                    export = export.Replace("||", "|");
                    _export += _x + "|";
                }
                else if (x.IndexOf("StartMenu") != -1)
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
            result.AppendLine("//EndLastActivity");
            result.AppendLine("})%>");
            EditePageRepository.CreatePage("/Views/Home/ProfileBig.ascx", result.ToString());
        }
        public IDictionary<string, string> ProfilePage(string culture)
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            foreach (var x in alldllMassive())
            {
                string parser = _RssParsing(x.Value + "/ProfilePage", culture);
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
    }
}
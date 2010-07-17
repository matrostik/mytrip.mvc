using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web;
using Mytrip.Mvc.Models;
using System.Xml.Linq;

namespace Mytrip.Mvc
{
    public class UsersSetting
    {
        string _absolutDirectory = GeneralMethods.xmlAbsoluteDirectory("MytripConfiguration");
        public int minRequiredPasswordLength()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements("membership").Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "minRequiredPasswordLength");
            return int.Parse(core.Attribute("value").Value);
        }
        public int maxInvalidPasswordAttempts()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements("membership").Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "maxInvalidPasswordAttempts");
            return int.Parse(core.Attribute("value").Value);
        }        
        public string applicationName()
        { 
            return HttpContext.Current.Request.ServerVariables["HTTP_HOST"];
        }
        public bool requiresUniqueEmail()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements("membership").Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "requiresUniqueEmail");
            return bool.Parse(core.Attribute("value").Value);
        }
        public bool unlockCaptcha()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements("membership").Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "unlockCaptcha");
            return bool.Parse(core.Attribute("value").Value);
        }
        public string roleAdmin()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements("membership").Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "roleAdmin");
            return core.Attribute("value").Value;
        }
        public bool unlockGravatar()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements("profile").Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "unlockGravatar");
            return bool.Parse(core.Attribute("value").Value);
        }
        public bool unlockRegistration()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements("membership").Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "unlockRegistration");
            return bool.Parse(core.Attribute("value").Value);
        }
        public bool unlockVisibleLogon()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements("membership").Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "unlockVisibleLogon");
            return bool.Parse(core.Attribute("value").Value);
        }
        public bool unlockApprovedEmail()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements("membership").Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "unlockApprovedEmail");
            return bool.Parse(core.Attribute("value").Value);
        }
        public bool unlockSendEmail()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements("email").Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "unlockSendEmail");
            return bool.Parse(core.Attribute("value").Value);
        }
        public string roleChiefEditor()
        {
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements("membership").Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "roleChiefEditor");
            return core.Attribute("value").Value;
        }
    }
}

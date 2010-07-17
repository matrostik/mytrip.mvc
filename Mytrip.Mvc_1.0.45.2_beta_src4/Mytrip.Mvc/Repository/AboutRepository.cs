using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Mytrip.Mvc.Repository
{
   public class AboutRepository
   {
       string _absolutDirectory = GeneralMethods.xmlAbsoluteDirectory("About");
       public string GetAbout(string culture)
       {
           XDocument _doc = XDocument.Load(_absolutDirectory);
           var core = _doc.Root.Elements("About").FirstOrDefault(x => x.Attribute("culture").Value == culture.ToLower());
           string result = core.Element("Body").Value;
           result = result.Replace("[_", "<");
           result = result.Replace("_]", ">");
           return result;
       }
       public void EditAbout(string body, string culture)
       {
           body = body.Replace("<","[_");
           body = body.Replace(">","_]");
           XDocument _doc = XDocument.Load(_absolutDirectory);
           var core = _doc.Root.Elements("About").FirstOrDefault(x => x.Attribute("culture").Value == culture.ToLower());
           core.SetElementValue("Body", body);
           _doc.Save(_absolutDirectory);
       }
    }
}

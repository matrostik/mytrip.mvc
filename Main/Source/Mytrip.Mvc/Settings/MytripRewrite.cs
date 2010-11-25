using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Mytrip.Mvc.Settings
{
    public static class MytripRewrite
    {
        public static IDictionary<string, object> GetMytripRewrite()
        {
            IDictionary<string, object> result = new Dictionary<string, object>();
            string _absolutDirectory = GeneralMethods.MytripConfigurationDirectory("MytripUrlRewrite");
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var x = _doc.Root.Elements("add");
            foreach (var item in x)
            {
                string a = item.Attribute("url").Value;
                string b = item.Attribute("rewrite").Value;
                int count=0;
                string _controller = string.Empty;
                string _action = string.Empty;
                string _id = string.Empty;
                string _id2 = string.Empty;
                string _id3 = string.Empty;
                string _id4 = string.Empty;
                string _id5 = string.Empty;
                string _id6 = string.Empty;
                string _id7 = string.Empty;
                string _id8 = string.Empty;
                string _id9 = string.Empty;
                string _id10 = string.Empty;
                foreach(var d in b.Split('/'))
                {
                    if (count == 0)
                        _controller = d;
                    else if (count == 1)
                        _action = d;
                    else if (count == 2)
                        _id = d;
                    else if (count == 3)
                        _id2 = d;
                    else if (count == 4)
                        _id3 = d;
                    else if (count == 5)
                        _id4 = d;
                    else if (count == 6)
                        _id5 = d;
                    else if (count == 7)
                        _id6 = d;
                    else if (count == 8)
                        _id7 = d;
                    else if (count == 9)
                        _id8 = d;
                    else if (count == 10)
                        _id9 = d;
                    else if (count == 11)
                        _id10 = d;
                    count++;                
                }
                result.Add(a, new {
                    controller = _controller,
                    action = _action,
                    id = _id,
                    id2 = _id2,
                    id3 = _id3,
                    id4 = _id4,
                    id5 = _id5,
                    id6 = _id6,
                    id7 = _id7,
                    id8 = _id8,
                    id9 = _id9,
                    id10 = _id10
                });
            }
            return result;
        }
        public static string GetMytripRewriteForPath(string url)
        {
            string _url = url.Remove(0, 1);
            string _absolutDirectory = GeneralMethods.MytripConfigurationDirectory("MytripUrlRewrite");
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var q = _doc.Root.Elements("add").FirstOrDefault(x=>x.Attribute("url").Value==_url);
            if (q != null)
                return "/"+q.Attribute("rewrite").Value;
            else
            return url;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mytrip.Mvc.Settings;
using System.Xml.Linq;

namespace Mytrip.Tourism.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class MoneyHelpers
    {
        public static IDictionary<string, string> CultureMoney()
        {
            string _absolutDirectory = GeneralMethods.MytripConfigurationDirectory();
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements("Mytrip.Tourism").Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "Money");
            var _core = core.Elements("add");
            IDictionary<string, string> result = new Dictionary<string, string>();
            foreach (var x in _core)
            {
                result.Add(x.Attribute("key").Value, x.Attribute("name").Value);
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static decimal GetRateMoney(string id)
        {
            string _absolutDirectory = GeneralMethods.MytripConfigurationDirectory();
            XDocument _doc = XDocument.Load(_absolutDirectory);
            //money container
            var money = _doc.Root.Elements("Mytrip.Tourism")
                .Elements("add")
                .FirstOrDefault(x => x.Attribute("name").Value == "Course");
            //one money container
            var onemoney = money.Elements("add")
                .Where(x => x.Attribute("key").Value == ModuleSetting.keyMoney().ToUpper())
                .FirstOrDefault(x => x.Attribute("to").Value == id);
            //проверка и обновление всех курсов
            if (onemoney.Attribute("date").Value != string.Format("{0:dd-MM-yyyy}", DateTime.Now))
            {
                IDictionary<string, decimal> _key = new Dictionary<string, decimal>();
                IDictionary<string, decimal> _to = new Dictionary<string, decimal>();
                foreach (var q in CultureMoney())
                {
                    if (q.Key != "RUB")
                    {
                        string parser = GeneralMethods.HttpGetParsing("http://www.cbr.ru/currency_base/D_print.aspx?date_req=" +
                        string.Format("{0:dd.MM.yyyy}", DateTime.Now), "ru-RU");
                        if (parser.Length > 0)
                        {
                            parser = parser.Replace("</", "").Replace("<", "").Replace("td", "");
                            string[] m_parser = parser.Split('>');
                            int count = 0;
                            string ratexml = string.Empty;
                            foreach (string x in m_parser)
                            {
                                if (count == 7)
                                    ratexml = x;
                                if (x.Contains(q.Key))
                                    count = 1;
                                if (count > 0)
                                    count++;
                            }
                            ratexml = ratexml.Replace(",", ".");
                            //прямая запись
                            var _onemoney = money.Elements("add")
                                           .Where(x => x.Attribute("key").Value == "RUB")
                                           .FirstOrDefault(x => x.Attribute("to").Value == q.Key);
                            _onemoney.SetAttributeValue("date", string.Format("{0:dd-MM-yyyy}", DateTime.Now));
                            _onemoney.SetAttributeValue("rate", ratexml);
                            decimal _rate = 0;
                            if (LocalisationSetting.culture().ToLower() == "ru-ru")
                                decimal.TryParse(ratexml.Replace(".", ","), out _rate);
                            if (LocalisationSetting.culture().ToLower() == "en-us")
                                decimal.TryParse(ratexml, out _rate);
                            //обратная запись
                            _onemoney = money.Elements("add")
                                              .Where(x => x.Attribute("to").Value == "RUB")
                                              .FirstOrDefault(x => x.Attribute("key").Value == q.Key);
                            _onemoney.SetAttributeValue("date", string.Format("{0:dd-MM-yyyy}", DateTime.Now));
                            _onemoney.SetAttributeValue("rate", 1 / _rate);
                            _key.Add(q.Key, _rate);
                            _to.Add(q.Key, 1 / _rate);

                        }
                    }
                }
                foreach (var __key in _key)
                {
                    foreach (var __to in _to)
                    {
                        if (__key.Key != __to.Key)
                        {
                            var __onemoney = money.Elements("add")
                                                        .Where(x => x.Attribute("to").Value == __key.Key)
                                                        .FirstOrDefault(x => x.Attribute("key").Value == __to.Key);
                            __onemoney.SetAttributeValue("date", string.Format("{0:dd-MM-yyyy}", DateTime.Now));
                            __onemoney.SetAttributeValue("rate", __key.Value * __to.Value);
                        }
                    }
                }
                _doc.Save(_absolutDirectory);
            }
            string result = onemoney.Attribute("rate").Value;
            decimal rate = 0;
            if (LocalisationSetting.culture().ToLower() == "ru-ru")
                decimal.TryParse(result.Replace(".", ","), out rate);
            if (LocalisationSetting.culture().ToLower() == "en-us")
                decimal.TryParse(result, out rate);
            return rate;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="price"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string ConvertMoney(string key, decimal price)
        {
            string result = price.ToString("0.00") + " " + ModuleSetting.nameMoney();
            if (key.ToUpper() != ModuleSetting.keyMoney().ToUpper())
            {

                decimal rate = price * GetRateMoney(key.ToUpper());
                decimal procent = (rate / 100) * ModuleSetting.MoneyProcent();
                rate += procent;
                result = rate.ToString("0.00") + " " + ModuleSetting.nameMoney();

            }
            result = result.Replace(",", ".");
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="price"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static decimal ConvertMoneyDecimal(string key, decimal price)
        {
            decimal rate = price;
            if (key.ToUpper() != ModuleSetting.keyMoney().ToUpper())
            {
                rate = price * GetRateMoney(key.ToUpper());
                decimal procent = (rate / 100) * ModuleSetting.MoneyProcent();
                rate += procent;
            }
            return rate;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mytrip.Mvc.Settings;
using System.Xml.Linq;

namespace Mytrip.Store.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public class MoneyHelpers
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static decimal GetRate_EN_RU()
        {
            string _absolutDirectory = GeneralMethods.MytripConfigurationDirectory();
            XDocument _doc = XDocument.Load(_absolutDirectory);
            var core = _doc.Root.Elements("Mytrip.Store").Elements("add").FirstOrDefault(x => x.Attribute("name").Value == "Money");
            var _core = core.Elements("add").FirstOrDefault(x => x.Attribute("value").Value == "ru-ru");
            if (_core.Attribute("date").Value != string.Format("{0:dd-MM-yyyy}", DateTime.Now))
            {
                string parser = GeneralMethods.HttpGetParsing("http://www.cbr.ru/currency_base/D_print.aspx?date_req=" +
                string.Format("{0:dd.MM.yyyy}", DateTime.Now), "ru-RU");
                if (parser.Length > 0)
                {
                    parser = parser.Replace("</", "").Replace("<", "").Replace("td", "");
                    string[] m_parser = parser.Split('>');
                    var _en_us = core.Elements("add").FirstOrDefault(x => x.Attribute("value").Value == "en-us");
                    string en_us = _en_us.Attribute("key").Value;
                    int count = 0;
                    string ratexml = string.Empty;
                    foreach (string x in m_parser)
                    {
                        if (count == 7)
                            ratexml = x;
                        if (x.Contains(en_us))
                            count = 1;
                        if (count > 0)
                            count++;
                    }
                    ratexml = ratexml.Replace(",", ".");
                    _core.SetAttributeValue("date", string.Format("{0:dd-MM-yyyy}", DateTime.Now));
                    _core.SetAttributeValue("rate", ratexml);
                    _doc.Save(_absolutDirectory);
                }            
            }
            string result = _core.Attribute("rate").Value;
            decimal rate = 1;
            decimal.TryParse(result, out rate);
            return rate;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="price"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static string ConvertMoney(decimal price, string culture)
        {
            string result = price.ToString("0.00") + " " + ModuleSetting.nameMoney();
            if (culture.ToLower() != LocalisationSetting.culture().ToLower())
            {
                if (culture.ToLower() == "ru-ru")
                {
                    decimal rate = (price / GetRate_EN_RU());
                    result = rate.ToString("0.00") + " " + ModuleSetting.nameMoney();
                }
                if (culture.ToLower() == "en-us")
                {
                    decimal rate = (price * GetRate_EN_RU());
                    result = rate.ToString("0.00") + " " + ModuleSetting.nameMoney();
                }
            }
            result = result.Replace(",", ".");
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="price"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public static decimal ConvertMoneyDecimal(decimal price, string culture)
        {
            decimal rate = price;
            if (culture.ToLower() != LocalisationSetting.culture().ToLower())
            {
                if (culture.ToLower() == "ru-ru")
                {
                    rate = (price / GetRate_EN_RU());
                }
                if (culture.ToLower() == "en-us")
                {
                   rate = (price * GetRate_EN_RU());
                }
            }
            return rate;
        }

    }
}

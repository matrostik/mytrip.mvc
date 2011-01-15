/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about mytrip.mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System.Web.Mvc;
using System.Web;
using mtm.Core.Settings;
using System;

namespace mtm.Core.Helpers
{
    /// <summary>ХТМЛ Хелпер для отображения Donate 
    /// </summary>
    public static class DonateHelper
    {  
        /// <summary>Акардион для отображения Donate
        /// Настраивается через конфигурацию ядра в mtm.Config.xml "DonateSideBar"
        /// </summary>
        /// <param name="html">HtmlHelper</param>
        /// <returns>возвращает HtmlString</returns>
        public static HtmlString AccordionDonateProject(this HtmlHelper html)
        {
            TagBuilder div_paypal = new TagBuilder("div");
            div_paypal.MergeAttribute("style", "padding: 10px; text-align: center; margin-right: auto; margin-left: auto");
            TagBuilder form_paypal = new TagBuilder("form");
            form_paypal.MergeAttribute("action", "https://www.paypal.com/cgi-bin/webscr");
            form_paypal.MergeAttribute("method", "post");
            TagBuilder input_paypal1 = new TagBuilder("input");
            input_paypal1.MergeAttribute("type", "hidden");
            input_paypal1.MergeAttribute("name", "cmd");
            input_paypal1.MergeAttribute("value", "_s-xclick");
            TagBuilder input_paypal2 = new TagBuilder("input");
            input_paypal2.MergeAttribute("type", "hidden");
            input_paypal2.MergeAttribute("name", "hosted_button_id");
            input_paypal2.MergeAttribute("value", "S26LL8WH92RZG");
            TagBuilder input_paypal3 = new TagBuilder("input");
            input_paypal3.MergeAttribute("type", "image");
            input_paypal3.MergeAttribute("src", "/Theme/" + ThemeSetting.theme() + "/images/PayPal.gif");
            input_paypal3.MergeAttribute("name", "submit");
            input_paypal3.MergeAttribute("alt", "PayPal - The safer, easier way to pay online!");
            input_paypal3.MergeAttribute("style", "border-width: 0px");
            TagBuilder img_paypal = new TagBuilder("img");
            img_paypal.MergeAttribute("alt", "");
            img_paypal.MergeAttribute("src", "/Theme/" + ThemeSetting.theme() + "/images/empty.gif");
            img_paypal.MergeAttribute("style", "border-width: 0px; width: 1px; height: 1px");
            form_paypal.InnerHtml = input_paypal1.ToString() + input_paypal2.ToString() + input_paypal3.ToString() + img_paypal.ToString();
            div_paypal.InnerHtml = form_paypal.ToString();
            HtmlString htmlresult = new HtmlString(GeneralMethods.Accordion(CoreLanguage.donate, div_paypal.ToString()));
            return htmlresult;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;

namespace Mytrip.Mvc.Helpers
{
   public static class AccordionHelper
    {
       public static string AccordionDonateProject(this HtmlHelper html)
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
           input_paypal3.MergeAttribute("src", "/Content/images/PayPal.gif");
           input_paypal3.MergeAttribute("name", "submit");
           input_paypal3.MergeAttribute("alt", "PayPal - The safer, easier way to pay online!");
           input_paypal3.MergeAttribute("style", "border-width: 0px");
           TagBuilder img_paypal = new TagBuilder("img");
           img_paypal.MergeAttribute("alt", "");
           img_paypal.MergeAttribute("src", "/Content/images/empty.gif");
           img_paypal.MergeAttribute("style", "border-width: 0px; width: 1px; height: 1px");
           form_paypal.InnerHtml = input_paypal1.ToString() + input_paypal2.ToString() + input_paypal3.ToString() + img_paypal.ToString();
           div_paypal.InnerHtml = form_paypal.ToString();
           return GeneralMethods.Accordion("accdon", CoreLanguage.donate, div_paypal.ToString());
       }
       public static string[] accardion()
       {
           string[] acc = {"accsearch","accdon" };
           string _acc = string.Empty;
           if (HttpContext.Current.Session["accardion"] != null)
               _acc = HttpContext.Current.Session["accardion"].ToString();
           else
           { 
               HttpContext.Current.Session["accardion"] = " ";
           }
           if (HttpContext.Current.Request.Cookies["myTripAccardion"] != null)
               _acc = HttpContext.Current.Request.Cookies["myTripAccardion"].Value;
           if (_acc != null)
               acc = _acc.Split(',');
           return acc;
       }
       public static string AccardionScript(this HtmlHelper html)
       {
           StringBuilder result = new StringBuilder();
           result.AppendLine("$(document).ready(function () {");

           foreach (string x in accardion())
           {
               if (x.IndexOf("acc") != -1)
               {
                   result.AppendLine("$(\"#" + x + ".accordion div.accordiontitle\").addClass(\"active\");");
                   result.AppendLine("$(\"#" + x + ".accordion div.accordioncontent\").addClass(\"active\");");
                   result.AppendLine("$(\"#" + x + ".accordion div.accordiontopcon\").addClass(\"active\");");
                   result.AppendLine("$(\"#" + x + ".accordion div.accordionbottomcon\").addClass(\"active\");");
                   result.AppendLine("$(\"#" + x + ".accordion div.accordiontopleft\").addClass(\"active\");");
                   result.AppendLine("$(\"#" + x + ".accordion div.accordionbottomleft\").addClass(\"active\");");
                   result.AppendLine("$(\"#" + x + ".accordion div.accordiontopright\").addClass(\"active\");");
                   result.AppendLine("$(\"#" + x + ".accordion div.accordionbottomright\").addClass(\"active\");");
                   result.AppendLine("$(\"#" + x + ".accordion div.accordioncontentground\").hide();");
               }
           }
           result.AppendLine("});");
           return result.ToString();
       }
    }
}

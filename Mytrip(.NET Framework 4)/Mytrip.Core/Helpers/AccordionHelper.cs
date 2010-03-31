using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Mytrip.Core.Helpers
{
   public static class AccordionHelper
    {
       public static string AccordionDonateProject(this HtmlHelper html)
       {
           StringBuilder result = new StringBuilder();
           TagBuilder div_accordion = new TagBuilder("div");
           div_accordion.AddCssClass("accordion");
           TagBuilder div_accordiontitle = new TagBuilder("div");
           div_accordiontitle.AddCssClass("accordiontitle");
           div_accordiontitle.InnerHtml = CoreLanguage.donate;
           TagBuilder div_accordioncontent = new TagBuilder("div");
           div_accordioncontent.AddCssClass("accordioncontent");
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
           div_paypal.InnerHtml = form_paypal.ToString();// +"mytripmvc@gmail.com";


           div_accordioncontent.InnerHtml = div_paypal.ToString();
           div_accordion.InnerHtml = div_accordiontitle.ToString() + div_accordioncontent.ToString();
           result.AppendLine(div_accordion.ToString());
           return result.ToString();
       }
        /*<div class="accordion">   
     <div class="accordiontitle">
         <%=Language.donate %></div>
     <div class="accordioncontent">
         <div style="padding: 10px; text-align: center; margin-right: auto; margin-left: auto">
             <a href="wmk:payto?Purse=R300527027774&Amount=10&Desc=donate%20project&BringToFront=Y">
                 <img alt="" src="/Content/images/WebMoney.png" style="border-width: 0px;" /></a><br />
             R300527027774<br />
             Z168404196316</div>
         <div style="padding: 10px; text-align: center; margin-right: auto; margin-left: auto">
             <img alt="" src="/Content/images/yandex.png" style="border-width: 0px;" /><br />
             41001382429153</div>
     </div>
 </div>*/
    }
}

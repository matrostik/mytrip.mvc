using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;

namespace Mytrip.Mvc.Controllers
{
  [HandleError]
  public  class AccordionController:Controller
    {
        [HttpPost]
        public void Index(string id)
        {
            if (Request.IsAjaxRequest())
            {
                string accardion=string.Empty;
                id = id.Replace("_", "");
                if (Session["accardion"] == null)
                    accardion = id;
                else
                {
                    accardion = Session["accardion"].ToString();
                    if (accardion.IndexOf(id) == -1)
                        accardion += "," + id;
                    else
                    {
                        accardion = accardion.Replace(id, "");
                        accardion = accardion.Replace(",,", ",");
                    }
                }
                Session["accardion"] = accardion;
                HttpCookie cookie = new HttpCookie("myTripAccardion", accardion);
                cookie.Expires = DateTime.Now.AddYears(1);
                Response.Cookies.Add(cookie);
            }

        }
    }
}

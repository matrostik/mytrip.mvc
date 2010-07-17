//************************************************************ 
// Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich
// To learn more about Mytrip.Mvc.Entyty visit 
// http://starterkitmytripmvc.codeplex.com/
// mytripmvc@gmail.com
// license: Microsoft Public License (Ms-PL) 
// ***********************************************************
using System.Web.Mvc;
using System.Drawing;
using System.Drawing.Imaging;
using Mytrip.Mvc.Repository;
using System.Web.UI;

namespace Mytrip.Mvc.Controllers
{
    /// <summary>
    /// Captcha Controller
    /// </summary>
    [HandleError]
    public class CaptchaController:Controller
    {   
        CaptchaRepository _captchaRepo;
        public CaptchaRepository captchaRepo
        {
            get
            {
                if (_captchaRepo == null)
                    _captchaRepo = new CaptchaRepository();
                return _captchaRepo;
            }
        }
        /// <summary>
        /// URL: /Captcha/Image/ImageWidth/ImageHeight/fontFamily
        /// </summary>
        /// <param name="id">Image Width</param>
        /// <param name="id2">Image Height</param>
        /// <param name="id3">Font Family</param>
        public void Index(int id, int id2, string id3)
        {
            
            string solution = string.Empty;
            Bitmap image = captchaRepo.mtGenerateImage(id, id2, id3, out solution);
            Session["antibotimage"] = solution;
            image.Save(Response.OutputStream, ImageFormat.Png);
        }
    }
}

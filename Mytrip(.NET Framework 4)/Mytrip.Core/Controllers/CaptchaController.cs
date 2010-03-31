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
using Mytrip.Core.Repository;

namespace Mytrip.Core.Controllers
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
        /// <param name="ImageWidth">Image Width</param>
        /// <param name="ImageHeight">Image Height</param>
        /// <param name="fontFamily">Font Family</param>
        public void Index(int ImageWidth, int ImageHeight, string fontFamily)
        {
            string solution = string.Empty;
            Bitmap image = captchaRepo.mtGenerateImage(ImageWidth, ImageHeight, fontFamily, out solution);
            Session["antibotimage"] = solution;
            image.Save(Response.OutputStream, ImageFormat.Png);
        }
    }
}

/* Copyright © 2009 - 2010 Oleg Stuhin & Rostislav Haitovich 
   To learn more about mytrip.mvc visit  
   http://mytripmvc.net  http://mytripmvc.codeplex.com 
   mytripmvc@gmail.com
   license: Microsoft Public License (Ms-PL) */
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;
using System.Web.Security;
using mtm.Core.Settings;

namespace mtm.Core.Repository
{
    /// <summary>
    /// Captcha Repository
    /// </summary>
    internal class CaptchaRepository
    {
        /// <summary>Generate Image
        /// </summary>
        /// <param name="ImageWidth">Image Width</param>
        /// <param name="ImageHeight">Image Height</param>
        /// <param name="fontFamily">Font Family</param>
        /// <param name="_solution">Solution</param>
        /// <returns>Bitmap</returns>
        internal Bitmap mtGenerateImage(int ImageWidth, int ImageHeight, string fontFamily, out string _solution)
        {
            Random d = new Random(DateTime.Now.Millisecond);
            int e = d.Next(5, 7);
            string solution = mtGenerateRandomString(e).ToLower();
            _solution = mtHashCaptcha(solution);
            Random random = new Random();
            Bitmap bitmap = new Bitmap(ImageWidth, ImageHeight, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, ImageWidth, ImageHeight);
            SizeF size;
            float fontSize = rect.Height + 1;
            Font font;
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            do
            {
                fontSize--;
                font = new Font(fontFamily, fontSize, FontStyle.Bold);
                size = g.MeasureString(solution, font, new SizeF(ImageWidth, ImageHeight), format);
            } while (size.Width > rect.Width);
            GraphicsPath path = new GraphicsPath();
            path.AddString(solution, font.FontFamily, (int)font.Style, font.Size, rect, format);
            float v = 4F;
            PointF[] points =
			{
				new PointF(random.Next(rect.Width) / v, random.Next(rect.Height) / v),
				new PointF(rect.Width - random.Next(rect.Width) / v, random.Next(rect.Height) / v),
				new PointF(random.Next(rect.Width) / v, rect.Height - random.Next(rect.Height) / v),
				new PointF(rect.Width - random.Next(rect.Width) / v, rect.Height - random.Next(rect.Height) / v)
			};
            Matrix matrix = new Matrix();
            matrix.Translate(0F, 0F);
            path.Warp(points, rect, matrix, WarpMode.Perspective, 0F);
            HatchBrush hatchBrush = new HatchBrush(HatchStyle.Wave, Color.White, Color.White);
            g.FillRectangle(hatchBrush, rect);
            hatchBrush = new HatchBrush(HatchStyle.DashedUpwardDiagonal, Color.Black, Color.Black);
            g.FillPath(hatchBrush, path);
            hatchBrush = new HatchBrush(HatchStyle.DashedUpwardDiagonal, Color.White, Color.White);
            g.FillPath(hatchBrush, path);
            font.Dispose();
            hatchBrush.Dispose();
            g.Dispose();
            return bitmap;
        }
        /// <summary>
        /// Generate Random String
        /// </summary>
        /// <param name="size">Size</param>
        /// <returns>string</returns>
        internal string mtGenerateRandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random(DateTime.Now.Millisecond);
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return builder.ToString();
        }
        /// <summary>
        /// Hash Captcha
        /// </summary>
        /// <param name="captcha">captcha</param>
        /// <returns>string</returns>
        private string mtHashCaptcha(string captcha)
        {
            string c = FormsAuthentication
                .HashPasswordForStoringInConfigFile((CoreSetting.applicationName() + captcha), "SHA1");
            return c;
        }
    }
}

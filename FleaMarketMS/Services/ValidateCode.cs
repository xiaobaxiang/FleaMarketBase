using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using System.Web.SessionState;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing.Imaging;
using System.Text;
using System.Security.Cryptography;
using MES.Models;

namespace MES.Service
{
    public static class ValidateCode
    {
        private const int height = 22;
        private const int width = 50;
        private const int length = 4;
        private const string chars = "0123456789";

        #region //getCodeGraphic
        public static FileContentResult getCodeGraphic(string VAILD_CODE)
        {
            //var randomText = GenerateRandomText(length);
            var randomText = VAILD_CODE;
            string GetSalt = typeof(FleaMarketMS.Controllers.HomeController).Assembly.FullName;
            var hash = ComputeMd5Hash(randomText + GetSalt);

            var rnd = new Random();
            var fonts = new[] { "Arial" };
            float orientationAngle = rnd.Next(0, 359);

            var index0 = rnd.Next(0, fonts.Length);
            var familyName = fonts[index0];

            using (var bmpOut = new Bitmap(width, height))
            {
                var g = Graphics.FromImage(bmpOut);
                var gradientBrush = new LinearGradientBrush(new Rectangle(0, 0, width, height),
                                                            Color.White, Color.DarkGray,
                                                            orientationAngle);
                g.FillRectangle(gradientBrush, 0, 0, width, height);
                DrawRandomLines(ref g, width, height);
                g.DrawString(randomText, new Font(familyName, 12, FontStyle.Bold), new SolidBrush(Color.Black), 0, 2);
                var ms = new MemoryStream();
                bmpOut.Save(ms, ImageFormat.Png);
                var bmpBytes = ms.GetBuffer();
                bmpOut.Dispose();
                ms.Close();

                return new FileContentResult(bmpBytes, "image/png");
            }
        }
        #endregion

        #region //GenerateRandomText
        private static string GenerateRandomText(int textLength)
        {
            var random = new Random();
            var result = new string(Enumerable.Repeat(chars, textLength)
                  .Select(s => s[random.Next(s.Length)]).ToArray());
            return result;
        }
        #endregion

        #region //ComputeMd5Hash
        private static string ComputeMd5Hash(string input)
        {
            var encoding = new ASCIIEncoding();
            var bytes = encoding.GetBytes(input);
            HashAlgorithm md5Hasher = MD5.Create();
            return BitConverter.ToString(md5Hasher.ComputeHash(bytes));
        }
        #endregion

        #region //DrawRandomLines
        private static void DrawRandomLines(ref Graphics g, int width, int height)
        {
            var rnd = new Random();
            var pen = new Pen(Color.Gray);
            for (var i = 0; i < 10; i++)
            {
                g.DrawLine(pen, rnd.Next(0, width), rnd.Next(0, height),
                                rnd.Next(0, width), rnd.Next(0, height));
            }
        }
        #endregion

        #region //IsValidCaptchaValue
        public static bool IsValidCaptchaValue(string captchaValue, string user_code)
        {
            //var expectedHash = userModel.ImgVaildCode;
            string GetSalt = typeof(FleaMarketMS.Controllers.HomeController).Assembly.FullName;
            var toCheck = captchaValue.ToUpper() + GetSalt;
            var hash = ComputeMd5Hash(toCheck);
            return hash.Equals(user_code);
        }
        #endregion
    }
}
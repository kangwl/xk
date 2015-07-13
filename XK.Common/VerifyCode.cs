using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;

namespace XK.Common {
    public static class VerifyCode {
 
 
        /// <summary>
        /// 随机产生指定长度的验证码字符串
        /// </summary>
        /// <param name="codeCount">验证码字符的个数</param>
        /// <returns>验证码字符串</returns>
        public static string GeneratorVerifyCode(int codeCount) {
            // 随机产生的非负数
            // 根据产生的随机数，按一定规律得到的单个字符
            // 验证码字符串
            string verifyCode = String.Empty;
            Random random = new Random();
            for (int i = 0; i < codeCount; i++) {
                int randomNumber = random.Next();
                //// 如果随机数能够整除3的情况
                char singleCode;
                if (randomNumber % 3 == 0) {
                    // 产生一个'0'到'9'的字符
                    singleCode = (char)('0' + randomNumber % 10);
                }
                else {

                    if (randomNumber % 3 == 1) {
                        // 产生一个'A'到'Z'的字符
                        singleCode = (char)('A' + randomNumber % 26);
                    }
                    else {
                        // 产生一个'a'到'z'的字符
                        singleCode = (char)('a' + randomNumber % 26);
                    }
                }
                verifyCode += singleCode.ToString();
            }
            return verifyCode;
        }

        /// <summary>
        /// 创建验证码图片
        /// </summary>
        public static Image CreateVerifyCodeImage(string verifyCode) {
            if (string.IsNullOrEmpty(verifyCode)) {
                return null;
            }
            Bitmap image = new Bitmap(verifyCode.Length*16, 27);
            using (Graphics g = Graphics.FromImage(image)) {
                Random random = new Random();
                // 清空验证码图片的背景色
                g.Clear(Color.White);
                // 画验证码图片的背景噪音线
                for (int i = 0; i < 25; i++) {
                    int x1 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int x2 = random.Next(image.Width);
                    int y2 = random.Next(image.Height);
                    // 在验证码图片上画单条背景噪音线
                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }

                // 画验证码图片
                Font font = new System.Drawing.Font("Arial", 15,
                    (System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic));
                System.Drawing.Drawing2D.LinearGradientBrush brush =
                    new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height),
                        Color.Blue, Color.DarkRed, 1.2f, true);
                g.DrawString(verifyCode, font, brush, 2, 2);

                // 画验证码图片的前景噪音点
                for (int i = 0; i < 100; i++) {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }

                // 画验证码图片的边框线
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
            }
            return image;
        }
    }
}

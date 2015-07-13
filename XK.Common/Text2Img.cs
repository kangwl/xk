using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace XK.Common
{
    /// <summary>
    /// 此类用于微信文字转化为图片
    /// </summary>
    public static class Text2Img
    {
        /// <summary>
        /// 将文本转化为图片（1280*720）
        /// </summary>
        /// <param name="text">要转化的文字</param>
        /// <param name="imgName">保存的图片名字</param>
        /// <param name="savePath">保存路径</param>
        public static void DrawTextToImg(string text, string imgName, string savePath) {
            try {
                Bitmap bmp = DrawLineString(text);
                string imgSavePath = savePath + imgName;
                bmp.Save(imgSavePath);
                bmp.Dispose();
            }
            catch (Exception ex) {
            }
        }

        public static void DrawTextToImgDiy(string text, string imgName, string savePath) {
            try {
                Bitmap bmp = DrawLineStringDiy(text);
                string imgSavePath = savePath + imgName;
                bmp.Save(imgSavePath);
                bmp.Dispose();
            }
            catch (Exception ex) {
            }
        }
        public static void DrawTextToImgDiyColor(string text, string imgName, string savePath, Color backColor, Brush fontColor) {
            try {
                Bitmap bmp = DrawLineStringDiyColor(text, backColor, fontColor);
                string imgSavePath = savePath + imgName;
                bmp.Save(imgSavePath);
                bmp.Dispose();
            }
            catch (Exception ex) {
            }
        }
        public static Color CreateRandomBgColor() {
            List<Color> listColor = new List<Color>();
            if (System.Web.HttpContext.Current.Cache["list_colors"] == null) {
                const int transparent = 220;
                // List<System.Drawing.Color> listColor = new List<Color>();
                listColor.Add(Color.FromArgb(transparent, 164, 196, 0));
                listColor.Add(Color.FromArgb(transparent, 196, 170, 23));
                listColor.Add(Color.FromArgb(transparent, 0, 138, 0));
                listColor.Add(Color.FromArgb(transparent, 1, 171, 170));
                listColor.Add(Color.FromArgb(transparent, 27, 160, 225));
                listColor.Add(Color.FromArgb(transparent, 217, 0, 115));
                listColor.Add(Color.FromArgb(transparent, 229, 20, 0));
                listColor.Add(Color.FromArgb(transparent, 250, 105, 0));
                listColor.Add(Color.FromArgb(transparent, 241, 103, 11));
                listColor.Add(Color.FromArgb(transparent, 227, 200, 0));
                listColor.Add(Color.FromArgb(transparent, 109, 135, 100));
                listColor.Add(Color.FromArgb(transparent, 101, 118, 136));
                System.Web.HttpContext.Current.Cache["list_colors"] = listColor;
            }
            else {
                listColor = (List<Color>)System.Web.HttpContext.Current.Cache["list_colors"];
            }
            //listColor.Sort();
            return NoRepeatColor(listColor);
        }

        private static Color NoRepeatColor(List<Color> colors) {

            Color theColor = Color.FromArgb(200, 1, 171, 170);
            try {
                Random random = new Random();
                int c_index = random.Next(1, 12);
                theColor = colors[c_index];
                if (System.Web.HttpContext.Current.Session["wordbgcolor"] != null) {
                    if (((Color) System.Web.HttpContext.Current.Session["wordbgcolor"]).Name ==
                        theColor.Name) {
                       // System.Web.HttpContext.Current.Session["wordbgcolor"] = theColor;
                        NoRepeatColor(colors);
                    }
                    else {
                        System.Web.HttpContext.Current.Session["wordbgcolor"] = theColor;
                    }
                }
                else {
                    System.Web.HttpContext.Current.Session["wordbgcolor"] = theColor;
                }
                
            }
            catch (Exception) {
            }
            return theColor;
        }

        /// <summary>
        /// 传入文字，生成图片（图片宽高1280x720（是固定的））
        /// </summary>
        /// <param name="content">文字内容</param>
        /// <returns></returns>
        private static Bitmap DrawLineString(string content) {
            //将内容进行处理
            content = ToSBC(content.Trim());
            //2.绘制背景
            LinearGradientBrush b_bru = new LinearGradientBrush(
            new Rectangle(0, 0, 1280, 720), Color.FromArgb(20, 20, 20),
            Color.FromArgb(60, 60, 60), LinearGradientMode.Horizontal);
            //设置渐变位置
            b_bru.SetSigmaBellShape(0.5f);
            Bitmap backgroundimg = new Bitmap(1280, 720);//图片大小转化
            Graphics g = Graphics.FromImage(backgroundimg);
            g.FillRectangle(b_bru, new Rectangle(0, 0, 1280, 720));
            //两层渐变效果
            LinearGradientBrush b_bru2 = new LinearGradientBrush(
            new Rectangle(0, 0, 1280, 720), Color.FromArgb(100, 20, 20, 20),
            Color.FromArgb(100, 60, 60, 60), LinearGradientMode.Vertical);
            b_bru2.SetSigmaBellShape(0.5f);
            g.FillRectangle(b_bru2, new Rectangle(0, 0, 1280, 720));
            g.SmoothingMode = SmoothingMode.HighSpeed;
            //画刷
            Brush bru = Brushes.White;
            //居中的方法
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            switch (content.Length) {
                case 1:
                    DrawLines(1, content, ref g, bru, sf, 400, 0);
                    break;
                case 2:
                    DrawLines(1, content, ref g, bru, sf, 350, 0);
                    break;
                case 3:
                    DrawLines(1, content, ref g, bru, sf, 250, 0);
                    break;
                case 4:
                    DrawLines(1, content, ref g, bru, sf, 200, 0);
                    break;
                case 5:
                case 6:
                    DrawLines(2, content, ref g, bru, sf, 200, 300);
                    break;
                case 7:
                case 8:
                    DrawLines(2, content, ref g, bru, sf, 180, 300);
                    break;
                case 9:
                case 10:
                    DrawLines(2, content, ref g, bru, sf, 160, 300);
                    break;
                case 11:
                case 12:
                    DrawLines(2, content, ref g, bru, sf, 140, 270);
                    break;
                case 13:
                case 14:
                    DrawLines(2, content, ref g, bru, sf, 120, 250);
                    break;
                case 15:
                case 16:
                    DrawLines(2, content, ref g, bru, sf, 110, 240);
                    break;
                case 17:
                case 18:
                    DrawLines(3, content, ref g, bru, sf, 120, 200);
                    break;
                case 19:
                case 20:
                case 21:
                    DrawLines(3, content, ref g, bru, sf, 115, 200);
                    break;
                case 22:
                case 23:
                case 24:
                    DrawLines(3, content, ref g, bru, sf, 105, 200);
                    break;
                case 25:
                case 26:
                case 27:
                    DrawLines(3, content, ref g, bru, sf, 95, 200);
                    break;
                case 28:
                case 29:
                case 30:
                    DrawLines(3, content, ref g, bru, sf, 88, 200);
                    break;
                case 31:
                case 32:
                case 33:
                    DrawLines(3, content, ref g, bru, sf, 80, 200);
                    break;
                case 34:
                case 35:
                    DrawLines(4, content, ref g, bru, sf, 90, 160);
                    break;
                case 36:
                case 37:
                case 38:
                case 39:
                case 40:
                    DrawLines(4, content, ref g, bru, sf, 82, 150);
                    break;
                case 41:
                case 42:
                case 43:
                case 44:
                    DrawLines(4, content, ref g, bru, sf, 75, 150);
                    break;
                case 45:
                case 46:
                case 47:
                case 48:
                    DrawLines(4, content, ref g, bru, sf, 70, 150);
                    break;
                case 49:
                case 50:
                    DrawLines(5, content, ref g, bru, sf, 80, 125);
                    break;
                case 51:
                case 52:
                case 53:
                case 54:
                case 55:
                    DrawLines(5, content, ref g, bru, sf, 75, 125);
                    break;
                case 56:
                case 57:
                case 58:
                case 59:
                case 60:
                    DrawLines(5, content, ref g, bru, sf, 70, 125);
                    break;
                case 61:
                case 62:
                case 63:
                case 64:
                case 65:
                    DrawLines(5, content, ref g, bru, sf, 65, 125);
                    break;
                case 66:
                case 67:
                case 68:
                case 69:
                case 70:
                    DrawLines(5, content, ref g, bru, sf, 60, 125);
                    break;
                case 71:
                case 72:
                    DrawLines(6, content, ref g, bru, sf, 66, 110);
                    break;
                case 73:
                case 74:
                case 75:
                case 76:
                case 77:
                case 78:
                    DrawLines(6, content, ref g, bru, sf, 60, 110);
                    break;
                case 79:
                case 80:
                case 81:
                case 82:
                case 83:
                case 84:
                    DrawLines(6, content, ref g, bru, sf, 58, 110);
                    break;
                case 85:
                case 86:
                case 87:
                case 88:
                case 89:
                case 90:
                    DrawLines(6, content, ref g, bru, sf, 55, 110);
                    break;
                case 91:
                case 92:
                case 93:
                case 94:
                case 95:
                case 96:
                    DrawLines(6, content, ref g, bru, sf, 52, 110);
                    break;
                case 97:
                case 98:
                    DrawLines(7, content, ref g, bru, sf, 58, 95);
                    break;
                case 99:
                case 100:
                case 101:
                case 102:
                case 103:
                case 104:
                case 105:
                    DrawLines(7, content, ref g, bru, sf, 55, 95);
                    break;
                case 106:
                case 107:
                case 108:
                case 109:
                case 110:
                case 111:
                case 112:
                    DrawLines(7, content, ref g, bru, sf, 52, 95);
                    break;
                case 113:
                case 114:
                case 115:
                case 116:
                case 117:
                case 118:
                case 119:
                    DrawLines(7, content, ref g, bru, sf, 49, 95);
                    break;
                case 120:
                    DrawLines(8, content, ref g, bru, sf, 55, 85);
                    break;
                case 121:
                case 122:
                case 123:
                case 124:
                case 125:
                case 126:
                case 127:
                case 128:
                    DrawLines(8, content, ref g, bru, sf, 50, 85);
                    break;
                case 129:
                case 130:
                case 131:
                case 132:
                case 133:
                case 134:
                case 135:
                case 136:
                    DrawLines(8, content, ref g, bru, sf, 48, 85);
                    break;
                case 137:
                case 138:
                case 139:
                case 140:
                    DrawLines(8, content, ref g, bru, sf, 48, 85);
                    break;
            }
            return backgroundimg;
        }
        private static Bitmap DrawLineStringDiy(string content) {
            //将内容进行处理
            content = ToSBC(content.Trim());
            //2.绘制背景
            SolidBrush b = new SolidBrush(Color.FromArgb(200, 205, 205, 60));//修改背景颜色
            Bitmap backgroundimg = new Bitmap(1280, 720);//图片大小转化
            Graphics g = Graphics.FromImage(backgroundimg);
            g.FillRectangle(b, new Rectangle(0, 0, 1280, 720));
            g.SmoothingMode = SmoothingMode.HighSpeed;
            //画刷改成黑色
            Brush bru = Brushes.Black;

            //居中的方法
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            switch (content.Length) {
                case 1:
                    DrawLines(1, content, ref g, bru, sf, 400, 0);
                    break;
                case 2:
                    DrawLines(1, content, ref g, bru, sf, 350, 0);
                    break;
                case 3:
                    DrawLines(1, content, ref g, bru, sf, 250, 0);
                    break;
                case 4:
                    DrawLines(1, content, ref g, bru, sf, 200, 0);
                    break;
                case 5:
                case 6:
                    DrawLines(2, content, ref g, bru, sf, 200, 300);
                    break;
                case 7:
                case 8:
                    DrawLines(2, content, ref g, bru, sf, 180, 300);
                    break;
                case 9:
                case 10:
                    DrawLines(2, content, ref g, bru, sf, 160, 300);
                    break;
                case 11:
                case 12:
                    DrawLines(2, content, ref g, bru, sf, 140, 270);
                    break;
                case 13:
                case 14:
                    DrawLines(2, content, ref g, bru, sf, 120, 250);
                    break;
                case 15:
                case 16:
                    DrawLines(2, content, ref g, bru, sf, 110, 240);
                    break;
                case 17:
                case 18:
                    DrawLines(3, content, ref g, bru, sf, 120, 200);
                    break;
                case 19:
                case 20:
                case 21:
                    DrawLines(3, content, ref g, bru, sf, 115, 200);
                    break;
                case 22:
                case 23:
                case 24:
                    DrawLines(3, content, ref g, bru, sf, 105, 200);
                    break;
                case 25:
                case 26:
                case 27:
                    DrawLines(3, content, ref g, bru, sf, 95, 200);
                    break;
                case 28:
                case 29:
                case 30:
                    DrawLines(3, content, ref g, bru, sf, 88, 200);
                    break;
                case 31:
                case 32:
                case 33:
                    DrawLines(3, content, ref g, bru, sf, 80, 200);
                    break;
                case 34:
                case 35:
                    DrawLines(4, content, ref g, bru, sf, 90, 160);
                    break;
                case 36:
                case 37:
                case 38:
                case 39:
                case 40:
                    DrawLines(4, content, ref g, bru, sf, 82, 150);
                    break;
                case 41:
                case 42:
                case 43:
                case 44:
                    DrawLines(4, content, ref g, bru, sf, 75, 150);
                    break;
                case 45:
                case 46:
                case 47:
                case 48:
                    DrawLines(4, content, ref g, bru, sf, 70, 150);
                    break;
                case 49:
                case 50:
                    DrawLines(5, content, ref g, bru, sf, 80, 125);
                    break;
                case 51:
                case 52:
                case 53:
                case 54:
                case 55:
                    DrawLines(5, content, ref g, bru, sf, 75, 125);
                    break;
                case 56:
                case 57:
                case 58:
                case 59:
                case 60:
                    DrawLines(5, content, ref g, bru, sf, 70, 125);
                    break;
                case 61:
                case 62:
                case 63:
                case 64:
                case 65:
                    DrawLines(5, content, ref g, bru, sf, 65, 125);
                    break;
                case 66:
                case 67:
                case 68:
                case 69:
                case 70:
                    DrawLines(5, content, ref g, bru, sf, 60, 125);
                    break;
                case 71:
                case 72:
                    DrawLines(6, content, ref g, bru, sf, 66, 110);
                    break;
                case 73:
                case 74:
                case 75:
                case 76:
                case 77:
                case 78:
                    DrawLines(6, content, ref g, bru, sf, 60, 110);
                    break;
                case 79:
                case 80:
                case 81:
                case 82:
                case 83:
                case 84:
                    DrawLines(6, content, ref g, bru, sf, 58, 110);
                    break;
                case 85:
                case 86:
                case 87:
                case 88:
                case 89:
                case 90:
                    DrawLines(6, content, ref g, bru, sf, 55, 110);
                    break;
                case 91:
                case 92:
                case 93:
                case 94:
                case 95:
                case 96:
                    DrawLines(6, content, ref g, bru, sf, 52, 110);
                    break;
                case 97:
                case 98:
                    DrawLines(7, content, ref g, bru, sf, 58, 95);
                    break;
                case 99:
                case 100:
                case 101:
                case 102:
                case 103:
                case 104:
                case 105:
                    DrawLines(7, content, ref g, bru, sf, 55, 95);
                    break;
                case 106:
                case 107:
                case 108:
                case 109:
                case 110:
                case 111:
                case 112:
                    DrawLines(7, content, ref g, bru, sf, 52, 95);
                    break;
                case 113:
                case 114:
                case 115:
                case 116:
                case 117:
                case 118:
                case 119:
                    DrawLines(7, content, ref g, bru, sf, 49, 95);
                    break;
                case 120:
                    DrawLines(8, content, ref g, bru, sf, 55, 85);
                    break;
                case 121:
                case 122:
                case 123:
                case 124:
                case 125:
                case 126:
                case 127:
                case 128:
                    DrawLines(8, content, ref g, bru, sf, 50, 85);
                    break;
                case 129:
                case 130:
                case 131:
                case 132:
                case 133:
                case 134:
                case 135:
                case 136:
                    DrawLines(8, content, ref g, bru, sf, 48, 85);
                    break;
                case 137:
                case 138:
                case 139:
                case 140:
                    DrawLines(8, content, ref g, bru, sf, 48, 85);
                    break;
            }
            return backgroundimg;
        }
        private static Bitmap DrawLineStringDiyColor(string content, Color backColor, Brush fontColor) {
            //将内容进行处理
            content = ToSBC(content.Trim());
            //2.绘制背景
            SolidBrush b = new SolidBrush(backColor);//修改背景颜色
            Bitmap backgroundimg = new Bitmap(1280, 720);//图片大小转化
            Graphics g = Graphics.FromImage(backgroundimg);
            g.FillRectangle(b, new Rectangle(0, 0, 1280, 720));
            g.SmoothingMode = SmoothingMode.HighSpeed;
            //画刷改成黑色
            Brush bru = fontColor;

            //居中的方法
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            switch (content.Length) {
                case 1:
                    DrawLines(1, content, ref g, bru, sf, 400, 0);
                    break;
                case 2:
                    DrawLines(1, content, ref g, bru, sf, 350, 0);
                    break;
                case 3:
                    DrawLines(1, content, ref g, bru, sf, 250, 0);
                    break;
                case 4:
                    DrawLines(1, content, ref g, bru, sf, 200, 0);
                    break;
                case 5:
                case 6:
                    DrawLines(2, content, ref g, bru, sf, 200, 300);
                    break;
                case 7:
                case 8:
                    DrawLines(2, content, ref g, bru, sf, 180, 300);
                    break;
                case 9:
                case 10:
                    DrawLines(2, content, ref g, bru, sf, 160, 300);
                    break;
                case 11:
                case 12:
                    DrawLines(2, content, ref g, bru, sf, 140, 270);
                    break;
                case 13:
                case 14:
                    DrawLines(2, content, ref g, bru, sf, 120, 250);
                    break;
                case 15:
                case 16:
                    DrawLines(2, content, ref g, bru, sf, 110, 240);
                    break;
                case 17:
                case 18:
                    DrawLines(3, content, ref g, bru, sf, 120, 200);
                    break;
                case 19:
                case 20:
                case 21:
                    DrawLines(3, content, ref g, bru, sf, 115, 200);
                    break;
                case 22:
                case 23:
                case 24:
                    DrawLines(3, content, ref g, bru, sf, 105, 200);
                    break;
                case 25:
                case 26:
                case 27:
                    DrawLines(3, content, ref g, bru, sf, 95, 200);
                    break;
                case 28:
                case 29:
                case 30:
                    DrawLines(3, content, ref g, bru, sf, 88, 200);
                    break;
                case 31:
                case 32:
                case 33:
                    DrawLines(3, content, ref g, bru, sf, 80, 200);
                    break;
                case 34:
                case 35:
                    DrawLines(4, content, ref g, bru, sf, 90, 160);
                    break;
                case 36:
                case 37:
                case 38:
                case 39:
                case 40:
                    DrawLines(4, content, ref g, bru, sf, 82, 150);
                    break;
                case 41:
                case 42:
                case 43:
                case 44:
                    DrawLines(4, content, ref g, bru, sf, 75, 150);
                    break;
                case 45:
                case 46:
                case 47:
                case 48:
                    DrawLines(4, content, ref g, bru, sf, 70, 150);
                    break;
                case 49:
                case 50:
                    DrawLines(5, content, ref g, bru, sf, 80, 125);
                    break;
                case 51:
                case 52:
                case 53:
                case 54:
                case 55:
                    DrawLines(5, content, ref g, bru, sf, 75, 125);
                    break;
                case 56:
                case 57:
                case 58:
                case 59:
                case 60:
                    DrawLines(5, content, ref g, bru, sf, 70, 125);
                    break;
                case 61:
                case 62:
                case 63:
                case 64:
                case 65:
                    DrawLines(5, content, ref g, bru, sf, 65, 125);
                    break;
                case 66:
                case 67:
                case 68:
                case 69:
                case 70:
                    DrawLines(5, content, ref g, bru, sf, 60, 125);
                    break;
                case 71:
                case 72:
                    DrawLines(6, content, ref g, bru, sf, 66, 110);
                    break;
                case 73:
                case 74:
                case 75:
                case 76:
                case 77:
                case 78:
                    DrawLines(6, content, ref g, bru, sf, 60, 110);
                    break;
                case 79:
                case 80:
                case 81:
                case 82:
                case 83:
                case 84:
                    DrawLines(6, content, ref g, bru, sf, 58, 110);
                    break;
                case 85:
                case 86:
                case 87:
                case 88:
                case 89:
                case 90:
                    DrawLines(6, content, ref g, bru, sf, 55, 110);
                    break;
                case 91:
                case 92:
                case 93:
                case 94:
                case 95:
                case 96:
                    DrawLines(6, content, ref g, bru, sf, 52, 110);
                    break;
                case 97:
                case 98:
                    DrawLines(7, content, ref g, bru, sf, 58, 95);
                    break;
                case 99:
                case 100:
                case 101:
                case 102:
                case 103:
                case 104:
                case 105:
                    DrawLines(7, content, ref g, bru, sf, 55, 95);
                    break;
                case 106:
                case 107:
                case 108:
                case 109:
                case 110:
                case 111:
                case 112:
                    DrawLines(7, content, ref g, bru, sf, 52, 95);
                    break;
                case 113:
                case 114:
                case 115:
                case 116:
                case 117:
                case 118:
                case 119:
                    DrawLines(7, content, ref g, bru, sf, 49, 95);
                    break;
                case 120:
                    DrawLines(8, content, ref g, bru, sf, 55, 85);
                    break;
                case 121:
                case 122:
                case 123:
                case 124:
                case 125:
                case 126:
                case 127:
                case 128:
                    DrawLines(8, content, ref g, bru, sf, 50, 85);
                    break;
                case 129:
                case 130:
                case 131:
                case 132:
                case 133:
                case 134:
                case 135:
                case 136:
                    DrawLines(8, content, ref g, bru, sf, 48, 85);
                    break;
                case 137:
                case 138:
                case 139:
                case 140:
                    DrawLines(8, content, ref g, bru, sf, 48, 85);
                    break;
            }
            return backgroundimg;
        }
        //很棒的绘图算法
        private static void DrawLines(int LineMode, string content, ref Graphics g, Brush bru, StringFormat sf, int fontsize, int margin) {
            Font f = new Font("微软雅黑", fontsize, FontStyle.Bold);
            //倒数第二行要加入的文字数
            int addtext = 0;
            //需要插入的行数
            int addline = (int)Math.Ceiling((double)LineMode / 2);
            //一个算法索引
            int index = 0;
            //如果余数大于有效行数
            if (content.Length % LineMode >= addline) {
                addtext = 1;
            }
            //奇数模式下
            if (LineMode % 2 != 0) {
                for (int i = 0; i < LineMode; i++) {
                    //判断是否是中间行以后 
                    if (i >= LineMode / 2) {
                        if (i == (LineMode - 1)) {
                            if (addtext != 0) {
                                g.DrawString(content.Substring(i * (content.Length / LineMode) + addline - 1), f, bru, new PointF(640, 360 + (-LineMode / 2 + i) * margin), sf);
                            }
                            else {
                                g.DrawString(content.Substring(i * (content.Length / LineMode)), f, bru, new PointF(640, 360 + (-LineMode / 2 + i) * margin), sf);
                            }
                        }
                        else {
                            if (addtext != 0) {
                                g.DrawString(content.Substring(i * (content.Length / LineMode) + index++, content.Length / LineMode + addtext), f, bru, new PointF(640, 360 + (-LineMode / 2 + i) * margin), sf);
                            }
                            else {
                                g.DrawString(content.Substring(i * (content.Length / LineMode), content.Length / LineMode), f, bru, new PointF(640, 360 + (-LineMode / 2 + i) * margin), sf);
                            }
                        }
                    }
                    else {
                        g.DrawString(content.Substring(i * (content.Length / LineMode), content.Length / LineMode), f, bru, new PointF(640, 360 + (-LineMode / 2 + i) * margin), sf);
                    }
                }
            }
            //偶数模式下
            else {
                for (int i = 0; i < LineMode; i++) {
                    //判断是否是中间行以后 
                    if (i >= LineMode / 2) {
                        if (i == (LineMode - 1)) {
                            if (addtext != 0) {
                                g.DrawString(content.Substring(i * (content.Length / LineMode) + addline - 1), f, bru, new PointF(640, 360 + ((-LineMode / 2 + i + 1) * margin - margin / 2)), sf);
                            }
                            else {
                                g.DrawString(content.Substring(i * (content.Length / LineMode)), f, bru, new PointF(640, 360 + ((-LineMode / 2 + i + 1) * margin - margin / 2)), sf);
                            }
                        }
                        else {
                            if (addtext != 0) {
                                g.DrawString(content.Substring(i * (content.Length / LineMode) + index++, content.Length / LineMode + addtext), f, bru, new PointF(640, 360 + ((-LineMode / 2 + i + 1) * margin - margin / 2)), sf);
                            }
                            else {
                                g.DrawString(content.Substring(i * (content.Length / LineMode), content.Length / LineMode), f, bru, new PointF(640, 360 + ((-LineMode / 2 + i + 1) * margin - margin / 2)), sf);
                            }
                        }
                    }
                    else {
                        g.DrawString(content.Substring(i * (content.Length / LineMode), content.Length / LineMode), f, bru, new PointF(640, 360 + ((-LineMode / 2 + i) * margin + margin / 2)), sf);
                    }
                }
            }
        }
        // 半角转全角：
        public static String ToSBC(String input) {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++) {
                if (c[i] == 32) {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new String(c);
        }
        //删除首标点符号
        public static string delthePunctuation(string input) {
            if (char.IsPunctuation(input[0])) {
                return input.Substring(1);
            }
            return input;
        }
    }
}

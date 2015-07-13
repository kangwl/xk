using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Web;

namespace XK.Common {
   public class PictureHelper {
       /// <summary>
       /// 此类用于微信编辑图片添加文字功能
       /// </summary>
       public class ImageEdit {

           private const string saveEditPath = "~/WeiXin/EditImage/{0}/";
           private const string relative_edit_path = "EditImage/{0}/";

           /// <summary>
           /// 绘制图片
           /// </summary>
           /// <param name="imgUrl"></param>
           /// <param name="dataStyle"></param>
           /// <param name="dataText"></param>
           /// <param name="dataCmd">命令参数（后期多样式时用）</param>
           /// <param name="sNo">屏幕编号</param>
           /// <param name="bgColor">可变参数，无值时全部默认为透明，一个参数时文字画布的颜色，两个参数时第二个参数为主画布的颜色</param>
           /// <returns></returns>
           public static string GenerateImage(string imgUrl, int dataStyle, string dataText, string dataCmd, string sNo, params string[] bgColor) {
               Image imageFrom = null;
               string fromPath = imgUrl;
               // const string savePath = "Images/";//保存图片的路径
               try {
                   if (!string.IsNullOrEmpty(fromPath)) {
                       #region 获取网络图片
                       Uri myUri = new Uri(fromPath);
                       WebRequest webRequest = WebRequest.Create(myUri);
                       WebResponse webResponse = webRequest.GetResponse();
                       System.IO.Stream reStream = webResponse.GetResponseStream();
                       if (reStream != null) imageFrom = new Bitmap(reStream);//读取原图
                       #endregion
                   }
               }
               catch (Exception exp) {
                   // new Common.Log().WriteLog(exp.Message + "PicEditAndPreview.aspx.cs->GenerateImage()->Line64->读取图片时异常");
               }
               if (imageFrom == null) {
                   return null;
               }
               float ratio = (float)imageFrom.Width / imageFrom.Height; //原始图片的长宽比例
               const int imageWidth = 1280; //背景画布宽高(720P)
               const int imageHeight = 720; //背景画布宽高(720P)
               float ratioStyle = 1F; //图片区域的宽高比例
               Size size = new Size();
               ScreenStyle screenType = (ScreenStyle)dataStyle;
               switch (screenType) {
                   case ScreenStyle.UpAndDown: {
                           #region 图片上文字下的风格(图片：1230*420，上半部分：720*0.618=445px 下半部分：275px)

                           ratioStyle = 2.9286F;
                           if (ratio > ratioStyle) //宽度较宽时
                       {
                               size = new Size(1230, (int)(1230 / ratio));
                           }
                           else {
                               size = new Size((int)(420 * ratio), 420);
                           }

                           #endregion
                       }
                       break;
                   case ScreenStyle.LeftAndRight: {
                           #region 图片左文字右的风格(图片：765*670 左边：790，右边490)

                           ratioStyle = 1.1418F;
                           if (ratio > ratioStyle) //宽度较宽时
                       {
                               size = new Size(765, (int)(765 / ratio));
                           }
                           else {
                               size = new Size((int)(670 * ratio), 670);
                           }

                           #endregion
                       }
                       break;
                   case ScreenStyle.FullScreen:

                       #region 图片全屏的风格(1280*720)

                       ratioStyle = 1.7778F;
                       if (ratio > ratioStyle) //宽度较宽时
                   {
                           size = new Size(1280, (int)(1280 / ratio));
                       }
                       else {
                           size = new Size((int)(720 * ratio), 720);
                       }

                       #endregion

                       break;
                   default:
                       break;
               }
               Image bmap = new Bitmap(imageWidth, imageHeight);
               Graphics g = Graphics.FromImage(bmap);

               #region 更改画布颜色

               Color color = Color.Transparent; //默认为透明的
               if (bgColor.Length > 0) {
                   color = ColorTranslator.FromHtml(bgColor[0]); //获取十六进制的颜色
               }
               if (bgColor.Length > 1) {
                   g.Clear(ColorTranslator.FromHtml(bgColor[1])); //给画布上色
               }
               else {
                   g.Clear(Color.Transparent);
               }

               #endregion

               Image imageResize = new Bitmap(imageFrom, size); //重新修改图片的尺寸
               switch (screenType) {
                   case ScreenStyle.UpAndDown: {
                           g.DrawImage(imageResize, (imageWidth - size.Width) / 2, 445 - size.Height + 40); //把图片往画布中画
                       }
                       break;
                   case ScreenStyle.LeftAndRight: {
                           g.DrawImage(imageResize, 790 - size.Width, (imageHeight - size.Height) / 2);
                       }
                       break;
                   case ScreenStyle.FullScreen: {
                           g.DrawImage(imageResize, (imageWidth - size.Width) / 2, (imageHeight - size.Height) / 2);
                       }
                       break;
                   default:
                       break;
               }

               #region 绘制文本

               MyFont myFont;
               Image textBitmap;
               switch (screenType) {
                   case ScreenStyle.UpAndDown: {
                           myFont = new MyFont(dataText, new Bitmap(1230, 250), color, screenType);
                           textBitmap = myFont.GenerateText();
                           g.DrawImage(textBitmap, 25, 445); //往画布中贴图片
                       }
                       break;
                   case ScreenStyle.LeftAndRight: {
                           myFont = new MyFont(dataText, new Bitmap(475, 670), color, screenType);
                           textBitmap = myFont.GenerateText();
                           g.DrawImage(textBitmap, 790, 25);
                       }
                       break;
                   case ScreenStyle.FullScreen: {
                           myFont = new MyFont(dataText, new Bitmap(400, 250), color, screenType);
                           textBitmap = myFont.GenerateText();
                           g.DrawImage(textBitmap, (imageWidth + size.Width) / 2 - 425,
                                       (imageHeight + size.Height) / 2 - 275); //在图片的右下角方位
                       }
                       break;
                   default:
                       break;
               }

               #endregion

               string sPicName = DateTime.Now.ToString("yyyyMMddhhmmss");
               string savePath = HttpContext.Current.Server.MapPath(string.Format(saveEditPath, sNo));
               if (!System.IO.Directory.Exists(savePath)) {
                   System.IO.Directory.CreateDirectory(savePath);
               }
               string sPicPath =
                   HttpContext.Current.Server.MapPath(string.Format(saveEditPath, sNo) + sPicName + "_thumbnail.jpg");
               string sPreFix = HttpContext.Current.Server.MapPath(string.Format(saveEditPath, sNo) + sPicName);

               #region 创建缩略图

               Image smBmap = bmap.GetThumbnailImage(Convert.ToInt32(bmap.Width / 4.267), Convert.ToInt32(bmap.Height / 4.267),
                                                     null, IntPtr.Zero);
               smBmap.Save(sPicPath, System.Drawing.Imaging.ImageFormat.Jpeg);

               #endregion

               bmap.Save(sPreFix + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
               string returnImg = string.Format(relative_edit_path, sNo) + sPicName + "_thumbnail.jpg";

               return returnImg;
           }
       }
       /// <summary>
       /// 自定义一个字体类，用于美化字体样式
       /// </summary>
       class MyFont {
           private Bitmap Bmap { get; set; }
           private string Content { get; set; }
           private Color TextColor { get; set; }
           private float TextFontSize { get; set; }
           private ScreenStyle TextStyle { get; set; }
           public MyFont(string content, Bitmap bmap) {
               Content = content;
               Bmap = bmap;
               TextColor = Color.Black;
               TextFontSize = 25;
               TextStyle = ScreenStyle.UpAndDown;
           }
           public MyFont(string content, Bitmap bmap, Color color) {
               Content = content;
               Bmap = bmap;
               TextColor = color;
               TextFontSize = 25;
               TextStyle = ScreenStyle.UpAndDown;
           }
           /// <summary>
           /// 
           /// </summary>
           /// <param name="content">输入的文本内容</param>
           /// <param name="bmap">绘制图片的画布</param>
           /// <param name="color">画布的背景色</param>
           /// <param name="style">显示风格</param>
           public MyFont(string content, Bitmap bmap, Color color, ScreenStyle style) {
               Content = content;
               Bmap = bmap;
               TextColor = color;
               TextFontSize = 25;
               TextStyle = style;
           }
           public Bitmap GenerateText() {
               var g = Graphics.FromImage(Bmap);
               //g.Clear(Color.Transparent);
               g.Clear(TextColor);
               var drawBrush = new SolidBrush(ColorTranslator.FromHtml("#070707"));//颜色
               var pos = Coordinate();//获取坐标位置
               var drawFont = new Font("微软雅黑", pos["fontSize"], FontStyle.Regular, GraphicsUnit.Pixel); //字体
               switch (TextStyle) {
                   case ScreenStyle.UpAndDown: {
                           if (Content.Length <= 30) {
                               g.DrawString(Content, drawFont, drawBrush, pos["xPos"], pos["yPos"]);
                           }
                           else {
                               //30~60字时，再绘制一行
                               float xPos = (Bmap.Width - TextFontSize * 30) / 2;
                               g.DrawString(Content.Substring(0, 30), drawFont, drawBrush, xPos, pos["yPos"]);
                               g.DrawString(Content.Substring(30), drawFont, drawBrush, xPos, pos["yPos"] + TextFontSize + 10);
                           }
                       }
                       break;
                   case ScreenStyle.LeftAndRight:
                   case ScreenStyle.FullScreen: {
                           //每满15字换一行
                           switch ((Content.Length - 1) / 15)//长度减1，防止当文字刚好为60时不显示
                           {
                               case 0: {
                                       g.DrawString(Content, drawFont, drawBrush, pos["xPos"], pos["yPos"]);
                                   }
                                   break;
                               case 1: {
                                       g.DrawString(Content.Substring(0, 15), drawFont, drawBrush, pos["xPos"], pos["yPos"]);
                                       g.DrawString(Content.Substring(15), drawFont, drawBrush, pos["xPos"], pos["yPos"] + TextFontSize + 10);
                                   }
                                   break;
                               case 2: {
                                       g.DrawString(Content.Substring(0, 15), drawFont, drawBrush, pos["xPos"], pos["yPos"]);
                                       g.DrawString(Content.Substring(15, 15), drawFont, drawBrush, pos["xPos"], pos["yPos"] + TextFontSize + 10);
                                       g.DrawString(Content.Substring(30), drawFont, drawBrush, pos["xPos"], pos["yPos"] + (TextFontSize + 10) * 2);
                                   }
                                   break;
                               case 3: {
                                       g.DrawString(Content.Substring(0, 15), drawFont, drawBrush, pos["xPos"], pos["yPos"]);
                                       g.DrawString(Content.Substring(15, 15), drawFont, drawBrush, pos["xPos"], pos["yPos"] + TextFontSize + 10);
                                       g.DrawString(Content.Substring(30, 15), drawFont, drawBrush, pos["xPos"], pos["yPos"] + (TextFontSize + 10) * 2);
                                       g.DrawString(Content.Substring(45), drawFont, drawBrush, pos["xPos"], pos["yPos"] + (TextFontSize + 10) * 3);
                                   }
                                   break;
                           }
                       }
                       break;
               }

               return Bmap;
           }
           private Dictionary<string, float> Coordinate() {
               var coor = new Dictionary<string, float>();
               float xPos = 0;
               float yPos = 0;
               float mapwidth = Bmap.Width;
               float mapheight = Bmap.Height;
               float textLength = Content.Length;//字数

               switch (TextStyle) {
                   case ScreenStyle.UpAndDown: {
                           xPos = (mapwidth - TextFontSize * textLength) / 2;
                           yPos = 50;
                       }
                       break;
                   case ScreenStyle.LeftAndRight:
                   case ScreenStyle.FullScreen: {

                           switch ((Content.Length - 1) / 15) {
                               case 0: {
                                       xPos = (mapwidth - TextFontSize * textLength) / 2;
                                   }
                                   break;
                               case 1:
                               case 2:
                               case 3: {
                                       xPos = (mapwidth - TextFontSize * 15) / 2;
                                   }
                                   break;
                               default: {
                                       throw new Exception("输入的字数超过60字");
                                   }
                           }
                           yPos = (mapheight - (TextFontSize + 10) * (textLength / 15 + 1)) / 2;//字体高度50px+行间距10px
                       }
                       break;
               }
               coor.Add("fontSize", TextFontSize);
               coor.Add("xPos", xPos);
               coor.Add("yPos", yPos);
               return coor;
           }
       }
       /// <summary>
       /// 图文的风格枚举
       /// </summary>
       enum ScreenStyle {
           /// <summary>
           ///上部分图片下部分文字 
           /// </summary>
           UpAndDown = 1,
           /// <summary>
           ///左边图片右边文字
           /// </summary>
           LeftAndRight = 2,
           /// <summary>
           /// 图片全屏，文字在图片中
           /// </summary>
           FullScreen = 3
       } 
   }
}

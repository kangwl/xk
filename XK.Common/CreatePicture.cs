using System.Drawing;
using System.Drawing.Imaging;

namespace XK.Common {
   public class CreatePicture {

       public static Bitmap DrawTextBmp(string ch, Font font, Color color, Size TextSize, int x, int y, int w, int h) {
           //创建此大小的图片
           //使用GDI+绘制
           Bitmap bmp = new Bitmap(TextSize.Width - x, TextSize.Height - y, PixelFormat.Format64bppArgb);
           Graphics g = Graphics.FromImage(bmp);
           g.Clear(Color.White);

           g.DrawString(ch, font, new SolidBrush(color), new PointF(w, h));
           g.Save();
           g.Dispose();
           //返回图像
           return bmp;
       }
       //去白边
       public static Bitmap ClearWhite(Bitmap bm) {
           int y_l = 0;//左边
           int y_r = 0;//右边
           int i_h = 0;//上边
           int i_d = 0;//下边
           #region 计算----
           for (int i = 0; i < bm.Width; i++) {
               for (int y = 0; y < bm.Height; y++) {
                   if (bm.GetPixel(i, y).R != 255 || bm.GetPixel(i, y).B != 255 || bm.GetPixel(i, y).G != 255) {
                       y_l = i;
                       goto yl;
                   }
               }
           }
       yl:
           for (int i = 0; i < bm.Width; i++) {
               for (int y = 0; y < bm.Height; y++) {
                   if (bm.GetPixel(bm.Width - i - 1, y).R != 255 || bm.GetPixel(bm.Width - i - 1, y).B != 255 || bm.GetPixel(bm.Width - i - 1, y).G != 255) {
                       y_r = i;
                       goto yr;
                   }
               }
           }
       yr:
           for (int i = 0; i < bm.Height; i++) {
               for (int y = 0; y < bm.Width; y++) {
                   if (bm.GetPixel(y, i).R != 255 || bm.GetPixel(y, i).B != 255 || bm.GetPixel(y, i).G != 255) {
                       i_h = i;
                       goto ih;
                   }
               }
           }
       ih:
           for (int i = 0; i < bm.Height; i++) {
               for (int y = 0; y < bm.Width; y++) {
                   if (bm.GetPixel(y, bm.Height - i - 1).R != 255 || bm.GetPixel(y, bm.Height - i - 1).B != 255 || bm.GetPixel(y, bm.Height - i - 1).G != 255) {
                       i_d = i;
                       goto id;
                   }
               }
           }
       id:
           #endregion

           //创建此大小的图片
           Bitmap bmp = new Bitmap(bm.Width - y_l - y_r, bm.Height - i_h - i_d);
           Graphics g = Graphics.FromImage(bmp);
           //(new Point(y_l, i_h), new Point(0, 0), new Size(bm.Width - y_l - y_r, bm.Height - i_h - i_d));
           Rectangle sourceRectangle = new Rectangle(y_l, i_h, bm.Width - y_l - y_r, bm.Height - i_h - i_d);
           Rectangle resultRectangle = new Rectangle(0, 0, bm.Width - y_l - y_r, bm.Height - i_h - i_d);
           g.DrawImage(bm, resultRectangle, sourceRectangle, GraphicsUnit.Pixel);
           return bmp;
       } 
   }
}

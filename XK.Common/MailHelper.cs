using System.Net.Mail;
using System.Text;

namespace XK.Common {
   public class MailHelper {
       #region 发送邮件类
       private const string mailServer = "smtp.163.com";
       private const string mailFromUser = "kangwl2009@163.com";
       private const string mailFromPwd = "password";

       /// <summary>
       /// 发送邮件
       /// </summary>
       /// <param name="to">收件人</param>
       /// <param name="title">标题</param>
       /// <param name="body">内容</param>
       /// <returns></returns>
       public static bool Send(string to, string title, string body) {
           SmtpClient _smtpClient = new SmtpClient();
           _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network; //指定电子邮件发送方式
           _smtpClient.Host = mailServer; //指定SMTP服务器
           _smtpClient.Credentials = new System.Net.NetworkCredential(mailFromUser, mailFromPwd); //用户名和密码

           MailMessage _mailMessage = new MailMessage(mailFromUser, to);
           _mailMessage.Subject = title; //主题
           _mailMessage.Body = body; //内容
           _mailMessage.BodyEncoding = Encoding.Default; //正文编码
           _mailMessage.IsBodyHtml = true; //设置为HTML格式
           _mailMessage.Priority = MailPriority.High; //优先级
           
           try {
               _smtpClient.Send(_mailMessage);
               return true;
           }
           catch {
               return false;
           }
       }
       /// <summary>
       /// 发送邮件
       /// </summary>
       /// <param name="mailFrom">发送邮件的账号</param>
       /// <param name="mailPwd">发送邮件的密码</param>
       /// <param name="mailTo">邮件接收者</param>
       /// <param name="mailTitle">邮件标题</param>
       /// <param name="body">邮件内容</param>
       /// <returns></returns>
       public static bool Send(string mailFrom, string mailPwd, string mailTo, string mailTitle, string body) {
           System.Net.NetworkCredential NetCredentials = new System.Net.NetworkCredential(mailFrom, mailPwd); //用户名和密码
           return Send(NetCredentials, mailTo, mailTitle, body);
       }


       public static bool Send(System.Net.NetworkCredential networkCredential, string to, string title, string body) {
           SmtpClient _smtpClient = new SmtpClient();
           _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network; //指定电子邮件发送方式
           _smtpClient.Host = mailServer; //指定SMTP服务器
           _smtpClient.Credentials = networkCredential; //密码身份验证

           MailMessage _mailMessage = new MailMessage(mailFromUser, to);
           _mailMessage.Subject = title; //主题
           _mailMessage.Body = body; //内容
           _mailMessage.BodyEncoding = Encoding.Default; //正文编码
           _mailMessage.IsBodyHtml = true; //设置为HTML格式
           _mailMessage.Priority = MailPriority.High; //优先级

           try {
               _smtpClient.Send(_mailMessage);
               return true;
           }
           catch {
               return false;
           }
       }


       #endregion

       public static bool SendAttch(string to, string title, string body,string file) {
           SmtpClient _smtpClient = new SmtpClient();
           _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network; //指定电子邮件发送方式
           _smtpClient.Host = mailServer; //指定SMTP服务器
           _smtpClient.Credentials = new System.Net.NetworkCredential(mailFromUser, mailFromPwd); //用户名和密码
           _smtpClient.Timeout = 3 * 1000 * 60;

           MailMessage _mailMessage = new MailMessage(mailFromUser, to);
           _mailMessage.Subject = title; //主题
           _mailMessage.Body = body; //内容
           _mailMessage.BodyEncoding = Encoding.Default; //正文编码
           _mailMessage.IsBodyHtml = true; //设置为HTML格式
           _mailMessage.Priority = MailPriority.High; //优先级
           _mailMessage.Attachments.Add(new Attachment(file));
           try {
               _smtpClient.Send(_mailMessage);
               return true;
           }
           catch {
               return false;
           }
       }
   }
}

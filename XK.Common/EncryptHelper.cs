using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
 

namespace XK.Common {
   public class EncryptHelper {
       private const string _keyExt = "kwL13xRp";
       /// <summary>
       /// DES加密
       /// </summary>
       /// <param name="pToEncrypt">加密字符串</param>
       /// <param name="sKey">密钥</param>
       /// <returns></returns>
       public static string Encrypt(string pToEncrypt, string sKey) {
           if (pToEncrypt == "") return "";
           if (sKey.Length < 8) sKey = sKey + _keyExt;
           if (sKey.Length > 8) sKey = sKey.Substring(0, 8);
           DESCryptoServiceProvider des = new DESCryptoServiceProvider();
           //把字符串放到byte数组中  
           //原来使用的UTF8编码，我改成Unicode编码了，不行  
           byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);
           //建立加密对象的密钥和偏移量  
           //原文使用ASCIIEncoding.ASCII方法的GetBytes方法  
           //使得输入密码必须输入英文文本  
           des.Key = Encoding.Default.GetBytes(sKey);
           des.IV = Encoding.Default.GetBytes(sKey);
           MemoryStream ms = new MemoryStream();
           CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
           //Write  the  byte  array  into  the  crypto  stream  
           //(It  will  end  up  in  the  memory  stream)  
           cs.Write(inputByteArray, 0, inputByteArray.Length);
           cs.FlushFinalBlock();
           //Get  the  data  back  from  the  memory  stream,  and  into  a  string  
           StringBuilder ret = new StringBuilder();
           foreach (byte b in ms.ToArray()) {
               //Format  as  hex  
               ret.AppendFormat("{0:X2}", b);
           }
           return ret.ToString();
       }

       /// <summary>
       /// DES解密
       /// </summary>
       /// <param name="pToDecrypt">解密字符串</param>
       /// <param name="sKey">解密密钥</param>
       /// <param name="outstr">返回值</param>
       /// <returns></returns> 
       public static bool Decrypt(string pToDecrypt, string sKey, out string outstr) {
           if (pToDecrypt == "") {
               outstr = "";
               return true;
           }
           if (sKey.Length < 8) sKey = sKey + _keyExt;
           if (sKey.Length > 8) sKey = sKey.Substring(0, 8);
           try {
               DESCryptoServiceProvider des = new DESCryptoServiceProvider();
               //Put  the  input  string  into  the  byte  array  
               byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
               for (int x = 0; x < pToDecrypt.Length / 2; x++) {
                   int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                   inputByteArray[x] = (byte)i;
               }
               //建立加密对象的密钥和偏移量，此值重要，不能修改  
               des.Key = Encoding.Default.GetBytes(sKey);
               des.IV = Encoding.Default.GetBytes(sKey);
               MemoryStream ms = new MemoryStream();
               CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
               //Flush  the  data  through  the  crypto  stream  into  the  memory  stream  
               cs.Write(inputByteArray, 0, inputByteArray.Length);
               cs.FlushFinalBlock();
               //Get  the  decrypted  data  back  from  the  memory  stream  
               //建立StringBuild对象，CreateDecrypt使用的是流对象，必须把解密后的文本变成流对象  
               outstr = Encoding.Default.GetString(ms.ToArray());
               return true;
           }
           catch {
               outstr = "";
               return false;
           }
       }

       /// <summary> 
       /// 加密
       /// </summary> 
       public class AES {
           //默认密钥向量
           private static readonly byte[] Keys = { 0x41, 0x72, 0x65, 0x79, 0x6F, 0x75, 0x6D, 0x79, 0x53, 0x6E, 0x6F, 0x77, 0x6D, 0x61, 0x6E, 0x3F };

           public static string Encode(string encryptString, string encryptKey) {
               encryptKey = StringHelper.GetSubString(encryptKey, 32, "");
               encryptKey = encryptKey.PadRight(32, ' ');

               RijndaelManaged rijndaelProvider = new RijndaelManaged();
               rijndaelProvider.Key = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 32));
               rijndaelProvider.IV = Keys;
               ICryptoTransform rijndaelEncrypt = rijndaelProvider.CreateEncryptor();

               byte[] inputData = Encoding.UTF8.GetBytes(encryptString);
               byte[] encryptedData = rijndaelEncrypt.TransformFinalBlock(inputData, 0, inputData.Length);

               return Convert.ToBase64String(encryptedData);
           }

           public static string Decode(string decryptString, string decryptKey) {
               try {
                   decryptKey = StringHelper.GetSubString(decryptKey, 32, "");
                   decryptKey = decryptKey.PadRight(32, ' ');

                   RijndaelManaged rijndaelProvider = new RijndaelManaged();
                   rijndaelProvider.Key = Encoding.UTF8.GetBytes(decryptKey);
                   rijndaelProvider.IV = Keys;
                   ICryptoTransform rijndaelDecrypt = rijndaelProvider.CreateDecryptor();

                   byte[] inputData = Convert.FromBase64String(decryptString);
                   byte[] decryptedData = rijndaelDecrypt.TransformFinalBlock(inputData, 0, inputData.Length);

                   return Encoding.UTF8.GetString(decryptedData);
               }
               catch {
                   return "";
               }

           }

       }

       /// <summary> 
       /// 加密
       /// </summary> 
       public class DES {
           //默认密钥向量
           private static readonly byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

           /// <summary>
           /// DES加密字符串
           /// </summary>
           /// <param name="encryptString">待加密的字符串</param>
           /// <param name="encryptKey">加密密钥,要求为8位</param>
           /// <returns>加密成功返回加密后的字符串,失败返回源串</returns>
           public static string Encode(string encryptString, string encryptKey) {
               encryptKey = StringHelper.GetSubString(encryptKey, 8, "");
               encryptKey = encryptKey.PadRight(8, ' ');
               byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
               byte[] rgbIV = Keys;
               byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
               DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
               MemoryStream mStream = new MemoryStream();
               CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
               cStream.Write(inputByteArray, 0, inputByteArray.Length);
               cStream.FlushFinalBlock();
               return Convert.ToBase64String(mStream.ToArray());

           }

           /// <summary>
           /// DES解密字符串
           /// </summary>
           /// <param name="decryptString">待解密的字符串</param>
           /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
           /// <returns>解密成功返回解密后的字符串,失败返源串</returns>
           public static string Decode(string decryptString, string decryptKey) {
               try {
                   decryptKey = StringHelper.GetSubString(decryptKey, 8, "");
                   decryptKey = decryptKey.PadRight(8, ' ');
                   byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
                   byte[] rgbIV = Keys;
                   byte[] inputByteArray = Convert.FromBase64String(decryptString);
                   DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();

                   MemoryStream mStream = new MemoryStream();
                   CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                   cStream.Write(inputByteArray, 0, inputByteArray.Length);
                   cStream.FlushFinalBlock();
                   return Encoding.UTF8.GetString(mStream.ToArray());
               }
               catch {
                   return "";
               }
           }
       }

       /// <summary>
       /// MD5函数
       /// </summary>
       /// <param name="str">原始字符串</param>
       /// <returns>MD5结果</returns>
       public static string MD5(string str) {
           byte[] b = Encoding.UTF8.GetBytes(str);
           b = new MD5CryptoServiceProvider().ComputeHash(b);
           string ret = "";
           for (int i = 0; i < b.Length; i++)
               ret += b[i].ToString("x").PadLeft(2, '0');

           return ret;
       }

       /// <summary>
       /// SHA256函数
       /// </summary>
       /// /// <param name="str">原始字符串</param>
       /// <returns>SHA256结果</returns>
       public static string SHA256(string str) {
           byte[] SHA256Data = Encoding.UTF8.GetBytes(str);
           SHA256Managed Sha256 = new SHA256Managed();
           byte[] Result = Sha256.ComputeHash(SHA256Data);
           return Convert.ToBase64String(Result);  //返回长度为44字节的字符串
       } 
   }
}

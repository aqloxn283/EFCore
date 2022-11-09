using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;


namespace Tools
{
    //DESC加密解密帮助类  DESCryptoServiceProvider 弃用
    public class DESCryptoHelper
    {
        private static readonly string _key = "12345678";
        private static readonly string _iv = "12345678";

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Encrypt(string text)
        {
            var des = DES.Create();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(text);
            des.Key = ASCIIEncoding.ASCII.GetBytes(_key);
            des.IV = ASCIIEncoding.ASCII.GetBytes(_iv);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Decrypt(string text)
        {
            var des = DES.Create();
            int len;
            len = text.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(text.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(_key);
            des.IV = ASCIIEncoding.ASCII.GetBytes(_iv);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            return System.Text.Encoding.UTF8.GetString(ms.ToArray());
        }
    }

}
using KYSharp.SM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    public static class SM4Handler
    {
        
        public static string SecretKey = "da1f3ffdbb9a4b60b94c046216531cf6";
        public static string SecretSecret = "579d9ce807c746008df391d66b5029e3";
        public static string SM4ECBeEncrypt(string plaitText)
        {
            SM4Utils sm4 = new SM4Utils();
            sm4.secretKey = SecretKey;
            sm4.hexString = false;
            return sm4.Encrypt_ECB(plaitText);

        }
        public static string SM4ECBeDecrypt(string cipherText)
        {
            SM4Utils sm4 = new SM4Utils();
            sm4.secretKey = SecretKey;
            sm4.hexString = false;
            return sm4.Decrypt_ECB(cipherText);
        }
        
        public static string SM4CBCEncrypt(string plaitText)
        {
            SM4Utils sm4 = new SM4Utils();
            sm4.secretKey = SecretKey;
            sm4.iv = SecretSecret;
            sm4.hexString = false;
            return sm4.Encrypt_CBC(plaitText);
        }

        public static string SM4CBCDecrypt(string cipherText)
        {
            SM4Utils sm4 = new SM4Utils();
            sm4.secretKey = SecretKey;
            sm4.iv = SecretSecret;
            sm4.hexString = false;
            return sm4.Decrypt_CBC(cipherText);
        }
    }

}

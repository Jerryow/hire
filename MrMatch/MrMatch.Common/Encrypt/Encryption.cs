using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MrMatch.Common.Encrypt
{
    public class Encryption
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="myString">密码</param>
        /// <returns></returns>
        public static string GetMD5(string myString)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = System.Text.Encoding.Unicode.GetBytes(myString);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;
            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x");
            }
            return byte2String;
        }



        #region  加密解密
        //公钥
        private const string keyString = "MudbA1mpYXw=";
        //向量
        private const string ivString = "YuanN3+Cb6M=";


        #region 加密和解密字符串
        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="Value">加密前的字符串</param>
        ///  <param name="privateKey">用户持有的私钥</param>
        /// <returns>加密后返回的字符串</returns>
        public static string EncryptString(string Value)
        {
            //key=公钥+私钥
            string key = keyString;//privateKey+

            SymmetricAlgorithm mCSP = new DESCryptoServiceProvider();

            mCSP.Key = Convert.FromBase64String(key);
            mCSP.IV = Convert.FromBase64String(ivString);

            ICryptoTransform ct;
            MemoryStream ms;
            CryptoStream cs;
            byte[] byt;

            ct = mCSP.CreateEncryptor(mCSP.Key, mCSP.IV);

            byt = Encoding.UTF8.GetBytes(Value);

            ms = new MemoryStream();
            cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();

            cs.Close();

            return Convert.ToBase64String(ms.ToArray());

        }
        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="Value">解密前的字符串</param>
        /// <returns>返回解密后的字符串</returns>
        public static string DecryptString(string Value)
        {
            try
            {
                //key=公钥+私钥
                string key = keyString;//privateKey+

                SymmetricAlgorithm mCSP = new DESCryptoServiceProvider();
                mCSP.Key = Convert.FromBase64String(key);
                mCSP.IV = Convert.FromBase64String(ivString);

                ICryptoTransform ct;
                MemoryStream ms;
                CryptoStream cs;
                byte[] byt;

                ct = mCSP.CreateDecryptor(mCSP.Key, mCSP.IV);

                byt = Convert.FromBase64String(Value);

                ms = new MemoryStream();
                cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
                cs.Write(byt, 0, byt.Length);
                cs.FlushFinalBlock();

                cs.Close();

                return Encoding.UTF8.GetString(ms.ToArray());
            }
            catch (Exception e)
            {
                return "-100";

            }

        }
        #endregion
        #endregion
    }
}

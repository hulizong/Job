using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web.Model;
using Web.Common.LogHleper;

namespace Web.Controllers
{
   
    public class BaseController : Controller
    {

        public Login _login = null;
        /// <summary>
        /// 获取登录信息
        /// </summary>
        public Login login {
            get
            {
                if (_login!=null)
                {
                   return  _login;
                }
                //aPI
                var token = "8l+dratVk1K+I7nW2TKGRdqdwWmyNet3C4eHU6qYaTmxKt8rDQIgjdmsyZ0ERYekOiWxPXTXO6xJK3UO0X7EXBl/zOh+6taLpw4pGmsQXgg=";// this.Request.Headers["Authorization"];
                try
                {
                    //后台
                    //var token= Request.Cookies["token"];
                    Login info = JsonConvert.DeserializeObject<Login>(Encoding.UTF8.GetString(AESDecrypt(Convert.FromBase64String(token))));
                    if (info == null)
                    {
                        _login = new Login();
                    }
                    return info;
                }
                catch (Exception  ex)
                {
                    LogHelp.Error(ex);
                    return _login=new Login ();
                }
               
              
            }
        }

        /// <summary>
        /// 获取手机号
        /// </summary>
        public string phone {
            get {
                return login.Phone;
            }
        }

        /// <summary>
        /// 获取用户id
        /// </summary>
        public int userId
        {
            get
            {
                return login.ID;
            }
        }

        /// <summary>
        /// 获取密码
        /// </summary>
        public string passWord
        {
            get
            {
                return login.Password;
            }
        }

        /// <summary>
        /// 获取姓名
        /// </summary>
        public string name
        {
            get
            {
                return login.Name;
            }
        }

        /// <summary>
        /// 获取性别
        /// </summary>
        public string sex
        {
            get
            {
                if (login.Sex==1)
                {
                    return "男";
                }
                else
                {
                    return "女";
                }
            }
        }
        //默认密钥向量
        private static byte[] _key1 = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF, 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        /// <summary>
        /// AES加密算法
        /// </summary>
        /// <param name="plainText">明文字符串</param>
        /// <param name="strKey">密钥</param>
        /// <returns>返回加密后的密文字节数组</returns>
        public static byte[] AESEncrypt(string plainText)//, string strKey
        {
            //分组加密算法
            SymmetricAlgorithm des = Rijndael.Create();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(plainText);//得到需要加密的字节数组
            //设置密钥及密钥向量
            des.Key = Encoding.UTF8.GetBytes("dongbinhuiasxiny");
            des.IV = _key1;
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            byte[] cipherBytes = ms.ToArray();//得到加密后的字节数组
            cs.Close();
            ms.Close();
            return cipherBytes;
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="cipherText">密文字节数组</param>
        /// <param name="strKey">密钥</param>
        /// <returns>返回解密后的字符串</returns>
        public static byte[] AESDecrypt(byte[] cipherText)
        {
            SymmetricAlgorithm des = Rijndael.Create();
            des.Key = Encoding.UTF8.GetBytes("dongbinhuiasxiny");
            des.IV = _key1;
            byte[] decryptBytes = new byte[cipherText.Length];
            MemoryStream ms = new MemoryStream(cipherText);
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Read);
            cs.Read(decryptBytes, 0, decryptBytes.Length);
            cs.Close();
            ms.Close();
            return decryptBytes;
        }
    }
   

}
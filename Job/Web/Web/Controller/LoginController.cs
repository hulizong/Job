using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Model;
using Web.Common;
using Microsoft.Extensions.Configuration;
using Web.DBHelper;
using Web.Enums; 
using Web.Common.LogHleper;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net;
using Web.RedisHelper;
using Web.RabbitMQ;

namespace Web.Controllers
{
    public class LoginController : BaseController
    {
      
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public  JsonResult Login(Login login)
        {
            Response<object> result = new Response<object>()
            {
                code= Convert.ToInt32(Status.Failed)
            };
            var count = SqlDapperHelper.ReturnT<int>("select count(1) from [User] where Phone=@Phone and Password=@Password", login);
            if (count>0)
            {

                result.code = Convert.ToInt32(Status.Succeed);
                result.msg = "登录成功";
                Login tokens = SqlDapperHelper.ReturnT<Login>("select * from [User] where Phone=@Phone and Password=@Password", login);

                try
                {
                    //存redis
                    RedisManager.redisHelp.SetValue("Login", JsonConvert.SerializeObject(tokens));
                }
                catch (Exception ex)
                {  
                    LogHelp.Error(ex);
                }


                try
                {
                    //登录加密保存信息

                    //var token = AESEncrypt(JsonConvert.SerializeObject(tokens));
                    // Convert.ToBase64String(Encode(Encoding.UTF8.GetBytes(password)));
                   
                    var token = Convert.ToBase64String(AESEncrypt(JsonConvert.SerializeObject(tokens)));
                    //SignIn(tokens, true).Wait();

                    //登录成功，信息加入MQ

                    PushMQ.SendMQ(tokens, Key.PushMQUserKey);
                }
                catch (Exception ex)
                {
                    LogHelp.Error(ex);
                }
                return Json(new { result });
            }

            //测试日志
            LogHelp.Debug("登录失败：账号"+login.Phone+"  密码："+login.Password);
            result.msg = "登录失败";
            return Json(new { result });
        }

        public ActionResult See()
        {
            //测试拿redis
            var redis = RedisManager.redisHelp.GetValue("Login");
            return View();
        }

        public async Task SignIn(Login user, bool createPersistentCookie)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.ID.ToString()),
                };
            var claimsIdentity = new ClaimsIdentity(
               claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(120),
            };
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
            string infoCookie = string.Format("{0},{1},{2},{3}", user.Phone, user.Password, user.Name,user.ID);
            HttpContext.Session.SetString("login", user.Name);
            AddCookie("token", infoCookie, isNeedEncode: false);
        }

        /// <summary>
        /// 新增Cookie值
        /// </summary>
        /// <param name="cookieName">名称</param>
        /// <param name="value">值</param>
        /// <param name="expiresTime">有效时间</param>
        protected void AddCookie(string key, string val, int expiresDay = 0, bool isNeedEncode = false, string domain = null)
        {
            if (string.IsNullOrEmpty(val))
                return;
            var option = new CookieOptions();
            option.HttpOnly = true;
            if (expiresDay != 0)
                option.Expires = DateTime.Now.AddDays(expiresDay);
            if (!string.IsNullOrEmpty(domain))
            {
                option.Domain = domain;
            }
            Response.Cookies.Append(
                key,
                isNeedEncode ? WebUtility.UrlEncode(val) : val,
                option
            );
        }

    }
}
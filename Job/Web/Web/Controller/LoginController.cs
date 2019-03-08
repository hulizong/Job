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

namespace Web.Controllers
{
    public class LoginController : Controller
    {
      
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Login(Login login)
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
                
                return Json(new { result });
            } 
            result.msg = "登录失败";
            return Json(new { result });
        }

        public ActionResult See()
        {
            return View();
        }

    }
}
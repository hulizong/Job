using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.DBHelper;
using Web.Model;

namespace Web.Controllers
{
    public class InfoController : BaseController
    {
       
        public ActionResult Index()
        {
            ViewBag.Id = userId;
            ViewBag.Phone = phone;
            ViewBag.PassWord = passWord;
            ViewBag.Name = name;
            ViewBag.Sex = sex;
            Login userInfo = new Login()
            {
                Name = "2",
                Phone = "2",
                Password="2",
                Sex = 1
            };
            SqlDapperHelper.Insert(userInfo);
            return View();
        }
 
    }
}
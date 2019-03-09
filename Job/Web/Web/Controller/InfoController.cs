using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            return View();
        }
 
    }
}
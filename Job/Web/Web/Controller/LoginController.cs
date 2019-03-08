using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Model;

namespace Web.Controllers
{
    public class LoginController : Controller
    {
      
        public ActionResult Index(Login login)
        {

            return View();
        }

        
    }
}
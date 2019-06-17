using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace agenda.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()  //changer le index sur la page web
        {
            return View("Index");
        }
    }
}
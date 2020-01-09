using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TesteDeSolucao.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            //Thiado.DataDll.ThiagoTesteData referenciaThiagoData = new Thiado.DataDll.ThiagoTesteData();
            //var nome = referenciaThiagoData.Nome();

            return View();
        }
    }
}

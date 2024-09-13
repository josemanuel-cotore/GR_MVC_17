using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GR_MVC_17.Controllers
{
    public class Pruebas2Controller : Controller
    {
        // GET: Pruebas2
        public ActionResult Asincrono2()
        {
            return View();
        }

        public ActionResult Duplicador_CS(int cantidad_cs)
        {
            int resultado = Duplicar(cantidad_cs);
            ViewBag.Duplicado = resultado;
            return View("Asincrono2");
        }

        private int Duplicar(int cantidad_cs)
        {
            return cantidad_cs * 2;
        }


        public ActionResult Duplicador_Ajax(int cant_ajax)
        {
            int resultado = Duplicar(cant_ajax);
            return Content(resultado.ToString());

        }
    }
}
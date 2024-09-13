using GR_MVC_17.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GR_MVC_17.Controllers
{

    public class PruebasController : Controller
    {

        public class  Persona{
            public string Nombre { get; set; }
            public string Direccion { get; set;  }
            public int Edad { get; set; }
        }


    public ActionResult Usuarios()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var usuario = db.Usuario.FirstOrDefault();

            ViewBag.Message = usuario;

            return View(usuario);
        }

        public JsonResult JSON()
        {

            var p1 = new Persona()
            {
                Nombre = "Jose",
                Direccion = "Calle Alberti",
                Edad = 36
            };

            var p2 = new Persona()
            {
                Nombre = "Juan",
                Direccion = "Calle Ramón y Cajal",
                Edad = 56
            };

            var listaPersonas = new List<Persona>() { p1, p2 };

            return Json(listaPersonas, JsonRequestBehavior.AllowGet);

        }

        public FileResult DescargarPDF()
        {
            var ruta = Server.MapPath("CAPAS.pdf");
            return File(ruta, "application/pdf", "Mi Fichero");
        }


        public ActionResult misDisplays()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var usuario = db.Usuario.FirstOrDefault();

            ViewBag.Propiedad = usuario;

            return View();
        }

        public ActionResult Asincrono()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Duplicador_CS(int cantidad_CS)
        {
            var resultado = Duplicar(cantidad_CS);
            ViewBag.Resultado = resultado;
            return View("Asincrono");

        }

        private int Duplicar(int cantidad)
        {
            return cantidad * 2;
        }

        [HttpPost]
        public ContentResult Duplicador_Ajax(int cantidad_Ajax)
        {
            var resultado = Duplicar(cantidad_Ajax);
            return Content(resultado.ToString());
        }














































    }
}
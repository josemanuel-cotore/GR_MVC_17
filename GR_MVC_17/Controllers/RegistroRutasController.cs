using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GR_MVC_17;

namespace GR_MVC_17.Controllers
{
    public class RegistroRutasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RegistroRutas
        public ActionResult Index()
        {
            //var registroRutas = db.RegistroRutas.Include(r => r.Herramienta).Include(r => r.Perfil).Include(r => r.RutasUsuario).Include(r => r.TipoInconveniente).Include(r => r.Usuario);
            var registroRutas = db.RegistroRutas.ToList();
            return View(registroRutas.ToList());
        }

        // GET: RegistroRutas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegistroRutas registroRutas = db.RegistroRutas.Find(id);
            if (registroRutas == null)
            {
                return HttpNotFound();
            }
            return View(registroRutas);
        }

        // GET: RegistroRutas/Create
        public ActionResult Create()
        {
            ViewBag.IdHerramienta = new SelectList(db.Herramienta, "Id", "Nombre");
            ViewBag.IdPerfil = new SelectList(db.Perfil, "Id", "Nombre");
            ViewBag.IdRuta = new SelectList(db.RutasUsuario, "id", "Nombre");
            ViewBag.IdInconveniente = new SelectList(db.TipoInconveniente, "Id", "Nombre");
            ViewBag.IdUsuario = new SelectList(db.Usuario, "Id", "NombreUsuario");
            return View();
        }

        // POST: RegistroRutas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Fecha,Km,IdUsuario,IdPerfil,IdRuta,IdHerramienta,IdInconveniente")] RegistroRutas registroRutas)
        {
            if (ModelState.IsValid)
            {
                db.RegistroRutas.Add(registroRutas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdHerramienta = new SelectList(db.Herramienta, "Id", "Nombre", registroRutas.IdHerramienta);
            ViewBag.IdPerfil = new SelectList(db.Perfil, "Id", "Nombre", registroRutas.IdPerfil);
            ViewBag.IdRuta = new SelectList(db.RutasUsuario, "id", "Nombre", registroRutas.IdRuta);
            ViewBag.IdInconveniente = new SelectList(db.TipoInconveniente, "Id", "Nombre", registroRutas.IdInconveniente);
            ViewBag.IdUsuario = new SelectList(db.Usuario, "Id", "NombreUsuario", registroRutas.IdUsuario);
            return View(registroRutas);
        }

        // GET: RegistroRutas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RegistroRutas registroRutas = db.RegistroRutas.Find(id);
            if (registroRutas == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdHerramienta = new SelectList(db.Herramienta, "Id", "Nombre", registroRutas.IdHerramienta);
            ViewBag.IdPerfil = new SelectList(db.Perfil, "Id", "Nombre", registroRutas.IdPerfil);
            ViewBag.IdRuta = new SelectList(db.RutasUsuario, "id", "Nombre", registroRutas.IdRuta);
            ViewBag.IdInconveniente = new SelectList(db.TipoInconveniente, "Id", "Nombre", registroRutas.IdInconveniente);
            ViewBag.IdUsuario = new SelectList(db.Usuario, "Id", "NombreUsuario", registroRutas.IdUsuario);
            return View(registroRutas);
        }

        // POST: RegistroRutas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Fecha,Km,IdUsuario,IdPerfil,IdRuta,IdHerramienta,IdInconveniente")] RegistroRutas registroRutas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registroRutas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdHerramienta = new SelectList(db.Herramienta, "Id", "Nombre", registroRutas.IdHerramienta);
            ViewBag.IdPerfil = new SelectList(db.Perfil, "Id", "Nombre", registroRutas.IdPerfil);
            ViewBag.IdRuta = new SelectList(db.RutasUsuario, "id", "Nombre", registroRutas.IdRuta);
            ViewBag.IdInconveniente = new SelectList(db.TipoInconveniente, "Id", "Nombre", registroRutas.IdInconveniente);
            ViewBag.IdUsuario = new SelectList(db.Usuario, "Id", "NombreUsuario", registroRutas.IdUsuario);
            return View(registroRutas);
        }

        // GET: RegistroRutas/Delete/5
        public ActionResult Delete(int id)
        {
            //if (idRegistro == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            RegistroRutas registroRutas = db.RegistroRutas.Find(id);
            if (registroRutas == null)
            {
                return HttpNotFound();
            }
            return View(registroRutas);
        }

        // POST: RegistroRutas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RegistroRutas registroRutas = db.RegistroRutas.Find(id);

            int idU = registroRutas.IdUsuario;
            //int idH = registroRutas.IdHerramienta;
            int idP = registroRutas.IdPerfil;



            db.RegistroRutas.Remove(registroRutas);
            db.SaveChanges();
            //return RedirectToAction("Registro_Herramienta", "Home", new { idHerramienta = idH, idUsuario = idU, idPerfil = idP });
            return RedirectToAction("Registro_Herramienta", "Home", new { idUsuario = idU, idPerfil = idP });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

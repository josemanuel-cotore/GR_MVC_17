using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GR_MVC_17.DAL
{
    public class HerramientaRepositorio
    {
        public ApplicationDbContext db = new ApplicationDbContext();

        public List<Herramienta> dameHerramientasUsuarioPerfil(int idPerfil, int idUsuario)
        {
            List<Herramienta> LHerramientasUsuario = new List<Herramienta>();
            LHerramientasUsuario = (from j in db.HerramientasUsuario where j.idPerfil == idPerfil && j.idUsuario == idUsuario select j.Herramienta).OrderBy(x=>x.Orden).ToList();
            return LHerramientasUsuario;
        }

        public bool InsertarHerramienta(string herramientaNueva)
        {
            try
            {
                Herramienta herramienta = new Herramienta()
                {
                    Nombre = herramientaNueva
                };
                db.Herramienta.Add(herramienta);
                db.SaveChanges();

            }
            catch (Exception)
            {

                return false;
            }
            return true;

        }

        public Herramienta DameHerramientaPorNombre(string herramientaNueva)
        {
            return db.Herramienta.Where(x => x.Nombre == herramientaNueva).FirstOrDefault();
        }

        public Herramienta DameNombreHerramientaPorId(int id)
        {
            return db.Herramienta.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
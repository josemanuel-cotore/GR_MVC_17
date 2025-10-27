using GR_MVC_17.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GR_MVC_17.DAL
{
    public class ZapatillaRepositorio : IZapatillasRepositorio
    {
        public ApplicationDbContext db = new ApplicationDbContext();

        public List<Herramienta> dameZapatillasUsuarioPerfil(int idPerfil, int idUsuario)
        {
            List<Herramienta> LHerramientasUsuario = new List<Herramienta>();
            LHerramientasUsuario = (from j in db.HerramientasUsuario where j.idPerfil == idPerfil && j.idUsuario == idUsuario select j.Herramienta).OrderBy(x=>x.Orden).ToList();
            return LHerramientasUsuario;
        }

        public List<Herramienta> dameZapatillasUsuario(int idUsuario)
        {
            List<Herramienta> LHerramientasUsuario = new List<Herramienta>();
            LHerramientasUsuario = (from j in db.HerramientasUsuario where j.idUsuario == idUsuario select j.Herramienta).OrderBy(x => x.Orden).ToList();
            return LHerramientasUsuario;
        }

        public bool InsertarZapatilla(string zapatillaNueva)
        {
            try
            {
                Herramienta herramienta = new Herramienta()
                {
                    Nombre = zapatillaNueva
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

        public Herramienta DameZapatillaPorNombre(string herramientaNueva)
        {
            return db.Herramienta.Where(x => x.Nombre == herramientaNueva).FirstOrDefault();
        }

        public Herramienta DameNombreZapatillaPorId(int id)
        {
            return db.Herramienta.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
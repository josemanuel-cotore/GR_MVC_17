using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GR_MVC_17.DAL
{
    public class RutasRepositorio
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public List<RutasUsuario> DameRutasUsuarioPerfil(int idUsuario, int idPerfil)
        {
            var listadoRutas = (from j in db.RutasUsuario
                                where j.Orden > 0 &&
                                j.idUsuario==idUsuario &&
                                j.idPerfil==idPerfil
                                orderby j.Orden
                                select j).ToList();
            return listadoRutas;
        }

        public string dameNombreRuta(int? idRuta)
        {
            return db.RutasUsuario.Where(x => x.id == idRuta).Select(x => x.Nombre).FirstOrDefault();
        }

        public Boolean AñadirNuevaRuta(RutasUsuario ruta)
        {
            try
            {
                RutasUsuario existe = new RutasUsuario();

                existe = db.RutasUsuario
                    .Where(x => x.idPerfil == ruta.idPerfil &&
                    x.idUsuario == ruta.idUsuario &&
                    x.Nombre == ruta.Nombre).FirstOrDefault();

                if (existe == null)
                {
                    db.RutasUsuario.Add(ruta);
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("La ruta ya existe");
                }

             
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return true;
        }

        public RutasUsuario DameRutaPorNombrePerfilUsuario(string nombreRuta, int idPerfil, int idUsuario)
        {
            return db.RutasUsuario.Where(x => x.idPerfil == idPerfil && x.idUsuario == idUsuario && x.Nombre == nombreRuta).FirstOrDefault();

            //return db.Ruta.Where(x => x.Nombre == nombreRuta).FirstOrDefault();
        }

        public int DameOrdenMaximo(int idPerfil, int idUsuario)
        {
            RutasUsuario existe = new RutasUsuario();
            existe = db.RutasUsuario.Where(w => w.idUsuario == idUsuario).Where(q => q.idPerfil == idPerfil).FirstOrDefault();

            if (existe != null)
            {
                return db.RutasUsuario.Where(w => w.idUsuario == idUsuario).Where(q => q.idPerfil == idPerfil).Max(x => x.Orden);
            }
            else
            {
                return 0;
            }
            
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GR_MVC_17.DAL
{
    public class PerfilRepositorio
    {
        public ApplicationDbContext db = new ApplicationDbContext();

        public List<Perfil> PerfilesPosiblesUsuario(List<Perfil> listaExisten)
        {
            List<Perfil> listaPerfiles = new List<Perfil>();
            listaPerfiles = (from j in db.Perfil select j).ToList();


            List<Perfil> listaFinal = new List<Perfil>();
            listaFinal = listaPerfiles.Except(listaExisten).ToList();
            return listaFinal;
        }

        public List<Perfil> DameListaPerfilesUsuario(Usuario usuario)
        {

            List<Perfil> listaPerfiles = new List<Perfil>();
            listaPerfiles = (from p in db.PerfilUsuario where p.IdUsuario == usuario.Id select p.Perfil).OrderBy(x=>x.Orden).ToList();
            return listaPerfiles;
        }

        public List<Perfil> DameListaPerfilesIdUsuario(int idUsuario)
        {
            List<Perfil> listaPerfiles = new List<Perfil>();
            listaPerfiles = (from p in db.PerfilUsuario where p.IdUsuario == idUsuario select p.Perfil).ToList();
            return listaPerfiles;
        }

        public void InsertarPerfil(PerfilUsuario perfilUsuario)
        {
            db.PerfilUsuario.Add(perfilUsuario);
            db.SaveChanges();
        }

        public Perfil DamePerfilPorId(int idPerfil)
        {
            return db.Perfil.Where(x => x.Id == idPerfil).FirstOrDefault();
        }

        public int DameMaxPerfiles()
        {
            return db.Perfil.Count();
        }

        public string dameTemaPerfil(int idPerfil)
        {
            return db.Perfil.Where(x => x.Id == idPerfil).Select(x => x.Tema).FirstOrDefault();
        }
    }
}
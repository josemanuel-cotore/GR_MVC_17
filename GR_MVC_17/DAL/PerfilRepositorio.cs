using GR_MVC_17.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GR_MVC_17.DAL
{
    public class PerfilRepositorio : IPerfilRepositorio
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

        public int DameListaPerfilesOcultos(Usuario usuario)
        {

            List<PerfilUsuario> listaOcultos = new List<PerfilUsuario>();
            listaOcultos = (from p in db.PerfilUsuario where p.IdUsuario == usuario.Id && p.MostrarPerfil == false select p).ToList();
            return listaOcultos.Count;
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

        public PerfilUsuario ComprobarUsuarioTienePerfil(int idPerfil, int idUsuario)
        {
            return db.PerfilUsuario.Where(x => x.IdPerfil == idPerfil && x.IdUsuario == idUsuario).FirstOrDefault();
        }

        public int DameMaxPerfiles()
        {
            return db.Perfil.Count();
        }

        public string dameTemaPerfil(int idPerfil)
        {
            return db.Perfil.Where(x => x.Id == idPerfil).Select(x => x.Tema).FirstOrDefault();
        }

        public bool ocultarMostrarPerfilUsuario(int? idPerfil, int? idUsuario, bool accion)
        {
            if (idPerfil == null || idUsuario == null)
            {
                return false;
            }

            PerfilUsuario existe = new PerfilUsuario();
            existe = db.PerfilUsuario.Where(x => x.IdPerfil == idPerfil && x.IdUsuario == idUsuario).FirstOrDefault();

            if (existe != null)
            {

                // Actualizar mostrar perfil a false

                existe.MostrarPerfil = accion;
                //db.PerfilUsuario.Remove(existe);
                db.SaveChanges();


                return true;
            }

            return false;

        }

        public List<Perfil> ActualizarPerfilesOcultos(int idUsuario)
        {
            List<Perfil> listaPerfiles = new List<Perfil>();
            listaPerfiles = (from p in db.PerfilUsuario where p.IdUsuario == idUsuario && p.MostrarPerfil == false select p.Perfil).OrderBy(x => x.Orden).ToList();

            foreach (var item in listaPerfiles)
            {

                PerfilUsuario existe = new PerfilUsuario();
                existe = db.PerfilUsuario.Where(x => x.IdPerfil == item.Id && x.IdUsuario == idUsuario).FirstOrDefault();

                if (existe != null)
                {
                    existe.MostrarPerfil = true;
                    db.SaveChanges();
                }
                //ocultarMostrarPerfilUsuario(item.Id, idUsuario, true);
            }

            return listaPerfiles;
        }

        public List<Perfil> DameListaPerfilesParaMostrar(Usuario usuario)
        {
            var l = db.PerfilUsuario.Where(j => j.MostrarPerfil == true && j.IdUsuario == usuario.Id).Select(x=>x.Perfil).ToList();
            return l;
        }

        public Perfil DamePerfilPorId(int idPerfil)
        {
            return new Perfil();
        }
    }
}
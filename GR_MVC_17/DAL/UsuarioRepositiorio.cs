using GR_MVC_17.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GR_MVC_17.DAL
{
    public class UsuarioRepositiorio : IUsuarioRepositiorio
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public List<Usuario> DameTodosUsuarios()
        {
            return db.Usuario.Where(x => x.IdRol != 1).ToList();
        }

        public Usuario ExisteUsuario(Usuario usuario)
        {
            Usuario existe = new Usuario();
            existe = (from j in db.Usuario
                      where j.NombreUsuario == usuario.NombreUsuario & j.Contraseña == usuario.Contraseña
                      select j).FirstOrDefault();

            return existe;
        }

        public Usuario ExisteUsuarioPorId(int idUsuario)
        {
            Usuario existe = new Usuario();
            existe = (from j in db.Usuario
                      where j.Id == idUsuario
                      select j).FirstOrDefault();

            return existe;
        }

        public Usuario DameUsuarioPorId(int idUsuario)
        {
            return db.Usuario.Where(x => x.Id == idUsuario).FirstOrDefault();
        }

        public bool InsertaUsuarioHerramientaPerfil(int idHerramienta, int idUsuario, int idPerfil)
        {
            try
            {
                HerramientasUsuario nuevoRegistro = new HerramientasUsuario()
                {
                    idHerramienta = idHerramienta,
                    idUsuario = idUsuario,
                    idPerfil = idPerfil
                };

                db.HerramientasUsuario.Add(nuevoRegistro);
                db.SaveChanges();

            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
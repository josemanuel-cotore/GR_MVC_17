using GR_MVC_17.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GR_MVC_17.DAL
{
    public class PerfilRepositiorio : IPerfilRepositorio
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public List<Perfil> DameListaPerfilesUsuario(Usuario usuario)
        {
            return db.PerfilUsuario.Where(x => x.IdUsuario == usuario.Id).Select(x=> x.Perfil).ToList();
        }

     
    }
}

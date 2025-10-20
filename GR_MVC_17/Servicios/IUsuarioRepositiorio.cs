using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GR_MVC_17.Servicios
{
    public interface IUsuarioRepositiorio
    {
        Usuario ExisteUsuario(Usuario usuario);
        Usuario ExisteUsuarioPorId(int idUsuario);
        Usuario DameUsuarioPorId(int idUsuario);
        bool InsertaUsuarioHerramientaPerfil(int idHerramienta, int idUsuario, int idPerfil);
    }
}

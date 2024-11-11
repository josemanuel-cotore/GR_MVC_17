using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GR_MVC_17.Servicios
{
    public interface IHerramientaRepositorio
    {
        List<Herramienta> dameHerramientasUsuarioPerfil(int idPerfil, int idUsuario);
        bool InsertarHerramienta(string herramientaNueva);
        Herramienta DameHerramientaPorNombre(string herramientaNueva);
        Herramienta DameNombreHerramientaPorId(int id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GR_MVC_17.Servicios
{
    public interface IZapatillasRepositorio
    {
        List<Herramienta> dameZapatillasUsuarioPerfil(int idPerfil, int idUsuario);
        bool InsertarZapatilla(string zapatillaNueva);
        Herramienta DameZapatillaPorNombre(string zapatilla);
        Herramienta DameNombreZapatillaPorId(int id);
    }
}

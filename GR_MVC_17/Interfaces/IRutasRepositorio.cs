using GR_MVC_17.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GR_MVC_17.Interfaces
{
    public interface IRutasRepositorio
    {
        List<RutasUsuario> DameRutasUsuarioPerfil(int idUsuario, int idPerfil);
        string dameNombreRuta(int? idRuta);
        Boolean AñadirNuevaRuta(RutasUsuario ruta);
        RutasUsuario DameRutaPorNombrePerfilUsuario(string nombreRuta, int idPerfil, int idUsuario);
        int DameOrdenMaximo(int idPerfil, int idUsuario);
        List<RutaOrdenUso_DTO> DameRutasOrdenNuevo(int idUsuario, int idPerfil);
    }
}

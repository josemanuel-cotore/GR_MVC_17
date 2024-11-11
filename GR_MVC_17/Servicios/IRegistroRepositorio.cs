using GR_MVC_17.Partial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GR_MVC_17.Servicios
{
    public interface IRegistroRepositorio
    {
        List<RegistroRutas> dameRegistroHerramienta(int idHerramienta, int idPerfil);
        Boolean AñadirRegistroRuta(RegistroRutas registro);
        double dameCalculoPorHerramienta(int idUsuario, int idHerramienta);
        bool eliminarRegistroRutas(int? id);
        List<RegistroRutas_Partial> dameRegistroPorPerfil(int idPerfil);
        List<RegistroRutas_Partial> convierteListaRegistrosEnPartial(List<RegistroRutas> lista);
        RegistroRutas dameRegistroPorId(int idRegistro);
    }
}

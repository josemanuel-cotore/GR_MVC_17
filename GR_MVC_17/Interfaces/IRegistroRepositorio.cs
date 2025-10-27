using GR_MVC_17.Partial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GR_MVC_17.Interfaces
{
    public interface IRegistroRepositorio
    {
        List<RegistroRutas> dameRegistroHerramienta(int idHerramienta, int idPerfil);
        Boolean AñadirRegistroRuta(RegistroRutas registro);
        double dameCalculoPorHerramienta(int idUsuario, int idHerramienta);
        bool eliminarRegistroRutas(int? id);
        RegistroRutas dameRegistroPorId(int idRegistro);
    }
}

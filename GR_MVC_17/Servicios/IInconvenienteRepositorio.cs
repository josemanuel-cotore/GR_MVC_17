using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GR_MVC_17.Servicios
{
    public interface IInconvenienteRepositorio
    {
        List<TipoInconveniente> dameInconvenientes();
    }
}

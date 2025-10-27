using GR_MVC_17.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GR_MVC_17.Servicios
{
    public interface IDesnivelService
    {
        List<DesnivelFecha_DTO> dameDesnivelAño();
        List<DesnivelFecha_DTO> dameDesnivelMes(int año);
        List<RegistroRutas> dameRegistrosDesnivel(int año, int mes);
    }
}

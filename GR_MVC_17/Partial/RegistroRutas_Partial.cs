using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GR_MVC_17.Partial
{
    public class RegistroRutas_Partial
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        public double? Km_Correr { get; set; }

        public int IdUsuario { get; set; }

        public int IdPerfil { get; set; }

        public int? IdRuta { get; set; }

        public int? IdHerramienta { get; set; }

        public int IdInconveniente { get; set; }

        public int? IdRutaBici { get; set; }

        public string TiempoRutaCorrer { get; set; }

        public string TiempoRutaBici { get; set; }

        // ALTERNATIVA

        public int? IdRuta_Alt { get; set; }

        public double? Km_Correr_Alt { get; set; }

        public string TiempoRutaCorrer_Alt { get; set; }

        //

        public string Observaciones { get; set; }

        public virtual RutasUsuario RutasUsuario { get; set; }

        public virtual TipoInconveniente TipoInconveniente { get; set; }

        public virtual Usuario Usuario { get; set; }
        public string nombreRutaCorrer { get; set; }
        public string nombreRutaBici { get; set; }
        public string nombreRutaCorrer_Alt { get; set; }

        public double? Km_Bici { get; set; }
    }
}
namespace GR_MVC_17
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RegistroRutas
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        public double Km { get; set; }

        public int IdUsuario { get; set; }

        public int IdPerfil { get; set; }

        public int? IdRuta { get; set; }

        public int? IdHerramienta { get; set; }

        public int IdInconveniente { get; set; }

        public int? IdRutaBici { get; set; }

        [StringLength(150)]
        public string TiempoRutaCorrer { get; set; }

        [StringLength(150)]
        public string TiempoRutaBici { get; set; }

        public double? Km_Bici { get; set; }

        [StringLength(250)]
        public string Observaciones { get; set; }

        public virtual RutasUsuario RutasUsuario { get; set; }

        public virtual TipoInconveniente TipoInconveniente { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}

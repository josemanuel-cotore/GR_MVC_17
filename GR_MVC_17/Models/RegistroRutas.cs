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

        public double? Km { get; set; }

        public int? Desnivel { get; set; }

        public int IdUsuario { get; set; }

        public int IdPerfil { get; set; }

        public int? IdRuta { get; set; }

        public int? IdHerramienta { get; set; }

        public int IdInconveniente { get; set; }

        [StringLength(250)]
        public string Observaciones { get; set; }

        public virtual RutasUsuario RutasUsuario { get; set; }

        public virtual TipoInconveniente TipoInconveniente { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}

namespace GR_MVC_17
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RutasUsuario")]
    public partial class RutasUsuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RutasUsuario()
        {
            RegistroRutas = new List<RegistroRutas>();
        }

        public int id { get; set; }

        public int idPerfil { get; set; }

        public int idUsuario { get; set; }

        [Required]
        [StringLength(150)]
        public string Nombre { get; set; }

        public int Orden { get; set; }

        public DateTime? fechaInserta { get; set; }

        public virtual Perfil Perfil { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual List<RegistroRutas> RegistroRutas { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}

namespace GR_MVC_17
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TipoInconveniente")]
    public partial class TipoInconveniente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TipoInconveniente()
        {
            RegistroRutas = new List<RegistroRutas>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Nombre { get; set; }

        public string Imagen { get; set; }

        [StringLength(150)]
        public string Observaciones { get; set; }

        public int? Orden { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual List<RegistroRutas> RegistroRutas { get; set; }
    }
}

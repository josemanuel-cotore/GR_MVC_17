namespace GR_MVC_17
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Usuario")]
    public partial class Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            HerramientasUsuario = new List<HerramientasUsuario>();
            PerfilUsuario = new List<PerfilUsuario>();
            RegistroRutas = new List<RegistroRutas>();
            RutasUsuario = new List<RutasUsuario>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string NombreUsuario { get; set; }

        [Required]
        [StringLength(150)]
        public string Contraseña { get; set; }

        [StringLength(150)]
        public string Nombre { get; set; }

        [StringLength(150)]
        public string Apellidos { get; set; }

        public DateTime? Edad { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual List<HerramientasUsuario> HerramientasUsuario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual List<PerfilUsuario> PerfilUsuario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual List<RegistroRutas> RegistroRutas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual List<RutasUsuario> RutasUsuario { get; set; }
    }
}

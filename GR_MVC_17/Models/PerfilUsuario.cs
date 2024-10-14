namespace GR_MVC_17
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PerfilUsuario")]
    public partial class PerfilUsuario
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdPerfil { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdUsuario { get; set; }

        public bool MostrarPerfil { get; set; }

        public DateTime? FechaInserta { get; set; }

        public virtual Perfil Perfil { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}

namespace GR_MVC_17
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HerramientasUsuario")]
    public partial class HerramientasUsuario
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idUsuario { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idHerramienta { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idPerfil { get; set; }

        public DateTime? FechaInserccion { get; set; }

        public virtual Herramienta Herramienta { get; set; }

        public virtual Perfil Perfil { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}

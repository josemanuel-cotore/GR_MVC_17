namespace GR_MVC_17
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("name=ApplicationDbContext")
        {
        }

        public virtual DbSet<Herramienta> Herramienta { get; set; }
        public virtual DbSet<HerramientasUsuario> HerramientasUsuario { get; set; }
        public virtual DbSet<Perfil> Perfil { get; set; }
        public virtual DbSet<PerfilUsuario> PerfilUsuario { get; set; }
        public virtual DbSet<RegistroRutas> RegistroRutas { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<RutasUsuario> RutasUsuario { get; set; }
        public virtual DbSet<TipoInconveniente> TipoInconveniente { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Herramienta>()
                .HasMany(e => e.HerramientasUsuario)
                .WithRequired(e => e.Herramienta)
                .HasForeignKey(e => e.idHerramienta)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Perfil>()
                .HasMany(e => e.HerramientasUsuario)
                .WithRequired(e => e.Perfil)
                .HasForeignKey(e => e.idPerfil)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Perfil>()
                .HasMany(e => e.PerfilUsuario)
                .WithRequired(e => e.Perfil)
                .HasForeignKey(e => e.IdPerfil)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Perfil>()
                .HasMany(e => e.RutasUsuario)
                .WithRequired(e => e.Perfil)
                .HasForeignKey(e => e.idPerfil)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Rol>()
                .HasMany(e => e.Usuario)
                .WithRequired(e => e.Rol)
                .HasForeignKey(e => e.IdRol)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RutasUsuario>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<RutasUsuario>()
                .HasMany(e => e.RegistroRutas)
                .WithOptional(e => e.RutasUsuario)
                .HasForeignKey(e => e.IdRuta);

            modelBuilder.Entity<TipoInconveniente>()
                .HasMany(e => e.RegistroRutas)
                .WithRequired(e => e.TipoInconveniente)
                .HasForeignKey(e => e.IdInconveniente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.HerramientasUsuario)
                .WithRequired(e => e.Usuario)
                .HasForeignKey(e => e.idUsuario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.PerfilUsuario)
                .WithRequired(e => e.Usuario)
                .HasForeignKey(e => e.IdUsuario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.RegistroRutas)
                .WithRequired(e => e.Usuario)
                .HasForeignKey(e => e.IdUsuario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .HasMany(e => e.RutasUsuario)
                .WithRequired(e => e.Usuario)
                .HasForeignKey(e => e.idUsuario)
                .WillCascadeOnDelete(false);
        }
    }
}

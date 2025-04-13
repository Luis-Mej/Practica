using Microsoft.EntityFrameworkCore;
using Entidades.Models;

namespace Entidades.Context
{
    public partial class PracticaContext : DbContext
    {
        public PracticaContext()
        {
        }

        public PracticaContext(DbContextOptions<PracticaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__PRODUCTO__3214EC27E28BDE65");

                entity.ToTable("PRODUCTOS");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Estado)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("ESTADO");
                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");
                entity.Property(e => e.Precio)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("PRECIO");
                entity.Property(e => e.Stock).HasColumnName("STOCK");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario).HasName("PK__USUARIOS__91136B90208DD43E");

                entity.ToTable("USUARIOS");

                entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");
                entity.Property(e => e.CodigoUsuario)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("CODIGO_USUARIO");
                entity.Property(e => e.Contrasenia)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CONTRASENIA");
                entity.Property(e => e.Estado)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("ESTADO");
                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

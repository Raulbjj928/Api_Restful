using Api_Restful.Models;
using Microsoft.EntityFrameworkCore;

namespace Api_Restful.Context;

public partial class ApiContext : DbContext
{
    public ApiContext()
    {
    }

    public ApiContext(DbContextOptions<ApiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Usuarios> Usuarios { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuarios>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__usuarios__645723A6CFC69871");

            entity.ToTable("usuarios");

            entity.HasIndex(e => e.Email, "UQ__usuarios__AB6E616472B57CAA").IsUnique();

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Nome)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("nome");
            entity.Property(e => e.Senha)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("senha");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

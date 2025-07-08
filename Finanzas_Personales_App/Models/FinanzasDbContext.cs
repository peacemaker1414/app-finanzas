using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Finanzas_Personales_App.Models;

public partial class FinanzasDbContext : DbContext
{
    public FinanzasDbContext()
    {
    }

    public FinanzasDbContext(DbContextOptions<FinanzasDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Egreso> Egresos { get; set; }

    public virtual DbSet<Ingreso> Ingresos { get; set; }

    public virtual DbSet<LookCategoriasEgreso> LookCategoriasEgresos { get; set; }

    public virtual DbSet<LookCategoriasIngreso> LookCategoriasIngresos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Egreso>(entity =>
        {
            entity.HasKey(e => e.IdEgreso).HasName("egresos_pkey");

            entity.ToTable("egresos");

            entity.Property(e => e.IdEgreso).HasColumnName("id_egreso");
            entity.Property(e => e.DetalleEgreso).HasColumnName("detalle_egreso");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.IdCatEgreso)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_cat_egreso");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Monto).HasColumnName("monto");

            entity.HasOne(d => d.IdCatEgresoNavigation).WithMany(p => p.Egresos)
                .HasForeignKey(d => d.IdCatEgreso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("egresos_id_cat_egreso_fkey");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Egresos)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("egresos_id_usuario_fkey");
        });

        modelBuilder.Entity<Ingreso>(entity =>
        {
            entity.HasKey(e => e.IdIngreso).HasName("ingresos_pkey");

            entity.ToTable("ingresos");

            entity.Property(e => e.IdIngreso).HasColumnName("id_ingreso");
            entity.Property(e => e.DetalleIngreso).HasColumnName("detalle_ingreso");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.IdCatIngreso)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_cat_ingreso");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Monto).HasColumnName("monto");

            entity.HasOne(d => d.IdCatIngresoNavigation).WithMany(p => p.Ingresos)
                .HasForeignKey(d => d.IdCatIngreso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ingresos_id_cat_ingreso_fkey");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Ingresos)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("ingresos_id_usuario_fkey");
        });

        modelBuilder.Entity<LookCategoriasEgreso>(entity =>
        {
            entity.HasKey(e => e.IdCatEgreso).HasName("look_categorias_egreso_pkey");

            entity.ToTable("look_categorias_egreso");

            entity.Property(e => e.IdCatEgreso).HasColumnName("id_cat_egreso");
            entity.Property(e => e.CategoriaEgreso).HasColumnName("categoria_egreso");
            entity.Property(e => e.SubcategoriaEgreso).HasColumnName("subcategoria_egreso");
        });

        modelBuilder.Entity<LookCategoriasIngreso>(entity =>
        {
            entity.HasKey(e => e.IdCatIngreso).HasName("look_categorias_ingreso_pkey");

            entity.ToTable("look_categorias_ingreso");

            entity.Property(e => e.IdCatIngreso).HasColumnName("id_cat_ingreso");
            entity.Property(e => e.CategoriaIngreso).HasColumnName("categoria_ingreso");
            entity.Property(e => e.SubcategoriaIngreso).HasColumnName("subcategoria_ingreso");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("usuarios_pkey");

            entity.ToTable("usuarios");

            entity.Property(e => e.IdUsuario)
                .ValueGeneratedNever()
                .HasColumnName("id_usuario");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Fecharegistro)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecharegistro");
            entity.Property(e => e.Nombre).HasColumnName("nombre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

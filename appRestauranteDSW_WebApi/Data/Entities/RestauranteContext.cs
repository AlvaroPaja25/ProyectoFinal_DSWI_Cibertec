using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace appRestauranteDSW_WebApi.Data.Entities;

public partial class RestauranteContext : DbContext
{
    public RestauranteContext()
    {
    }

    public RestauranteContext(DbContextOptions<RestauranteContext> options)
        : base(options)
    {
    }

    public virtual DbSet<caja> caja { get; set; }

    public virtual DbSet<cargo> cargo { get; set; }

    public virtual DbSet<categoria_plato> categoria_plato { get; set; }

    public virtual DbSet<cliente> cliente { get; set; }

    public virtual DbSet<comanda> comanda { get; set; }

    public virtual DbSet<comprobante> comprobante { get; set; }

    public virtual DbSet<detalle_comanda> detalle_comanda { get; set; }

    public virtual DbSet<detalle_comprobante> detalle_comprobante { get; set; }

    public virtual DbSet<empleado> empleado { get; set; }

    public virtual DbSet<establecimiento> establecimiento { get; set; }

    public virtual DbSet<estado_comanda> estado_comanda { get; set; }

    public virtual DbSet<mesa> mesa { get; set; }

    public virtual DbSet<metodo_pago> metodo_pago { get; set; }

    public virtual DbSet<plato> plato { get; set; }

    public virtual DbSet<tipo_comprobante> tipo_comprobante { get; set; }

    public virtual DbSet<usuario> usuario { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-MTIRLNL\\SQLEXPRESS;Database=RestauranteDSW;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<caja>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__caja__3213E83FA6EF6AC4");

            entity.HasOne(d => d.establecimiento).WithMany(p => p.caja).HasConstraintName("FK__caja__establecim__66603565");
        });

        modelBuilder.Entity<cargo>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__cargo__3213E83FD5B5DA6F");
        });

        modelBuilder.Entity<categoria_plato>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__categori__3213E83FEA9565E5");
        });

        modelBuilder.Entity<cliente>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__cliente__3213E83F9FBEBEF1");
        });

        modelBuilder.Entity<comanda>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__comanda__3213E83F30337C97");

            entity.HasOne(d => d.empleado).WithMany(p => p.comanda).HasConstraintName("FK__comanda__emplead__59FA5E80");

            entity.HasOne(d => d.estado_comanda).WithMany(p => p.comanda).HasConstraintName("FK__comanda__estado___5AEE82B9");

            entity.HasOne(d => d.mesa).WithMany(p => p.comanda).HasConstraintName("FK__comanda__mesa_id__5BE2A6F2");
        });

        modelBuilder.Entity<comprobante>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__comproba__3213E83F59121DBC");

            entity.HasOne(d => d.caja).WithMany(p => p.comprobante).HasConstraintName("FK__comproban__caja___6B24EA82");

            entity.HasOne(d => d.cliente).WithMany(p => p.comprobante).HasConstraintName("FK__comproban__clien__6C190EBB");

            entity.HasOne(d => d.comanda).WithMany(p => p.comprobante).HasConstraintName("FK__comproban__coman__6D0D32F4");

            entity.HasOne(d => d.empleado).WithMany(p => p.comprobante).HasConstraintName("FK__comproban__emple__6E01572D");

            entity.HasOne(d => d.tipo_comprobante).WithMany(p => p.comprobante).HasConstraintName("FK__comproban__tipo___6EF57B66");
        });

        modelBuilder.Entity<detalle_comanda>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__detalle___3213E83F3989D35E");

            entity.HasOne(d => d.comanda).WithMany(p => p.detalle_comanda).HasConstraintName("FK__detalle_c__coman__5EBF139D");

            entity.HasOne(d => d.plato).WithMany(p => p.detalle_comanda).HasConstraintName("FK__detalle_c__plato__5FB337D6");
        });

        modelBuilder.Entity<detalle_comprobante>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__detalle___3213E83F0909E389");

            entity.HasOne(d => d.comprobante).WithMany(p => p.detalle_comprobante).HasConstraintName("FK__detalle_c__compr__73BA3083");

            entity.HasOne(d => d.metodo_pago).WithMany(p => p.detalle_comprobante).HasConstraintName("FK__detalle_c__metod__74AE54BC");
        });

        modelBuilder.Entity<empleado>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__empleado__3213E83FF676B7B1");

            entity.HasOne(d => d.cargo).WithMany(p => p.empleado).HasConstraintName("FK__empleado__cargo___5629CD9C");

            entity.HasOne(d => d.usuario).WithMany(p => p.empleado).HasConstraintName("FK__empleado__usuari__571DF1D5");
        });

        modelBuilder.Entity<establecimiento>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__establec__3213E83FF47D9293");
        });

        modelBuilder.Entity<estado_comanda>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__estado_c__3213E83F79AF458E");
        });

        modelBuilder.Entity<mesa>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__mesa__3213E83F545649BF");
        });

        modelBuilder.Entity<metodo_pago>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__metodo_p__3213E83FF924FC96");
        });

        modelBuilder.Entity<plato>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__plato__3213E83F3BE50D04");

            entity.HasOne(d => d.categoria_plato).WithMany(p => p.plato).HasConstraintName("FK__plato__categoria__4D94879B");
        });

        modelBuilder.Entity<tipo_comprobante>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__tipo_com__3213E83F877C28CC");
        });

        modelBuilder.Entity<usuario>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__usuario__3213E83F35E31F39");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

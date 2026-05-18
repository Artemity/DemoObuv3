using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DemoObuv3.ModelsDB;

public partial class DemoObuvContext : DbContext
{
    public DemoObuvContext()
    {
    }

    public DemoObuvContext(DbContextOptions<DemoObuvContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Pvz> Pvzs { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=WIN-TNT0KK312B5\\ARTEMITY; Database = DemoObuv; Integrated Security=True; Encrypt = false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Orders_1");

            entity.Property(e => e.Article).HasMaxLength(50);
            entity.Property(e => e.OrderStatus).HasMaxLength(50);
            entity.Property(e => e.Pvzadress).HasColumnName("PVZAdress");

            entity.HasOne(d => d.ArticleNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Article)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_Products");

            entity.HasOne(d => d.PvzadressNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Pvzadress)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_PVZ");

            entity.HasOne(d => d.UserNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.User)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_Users");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Article);

            entity.Property(e => e.Article).HasMaxLength(50);
            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.Manufacturer).HasMaxLength(50);
            entity.Property(e => e.Photo).HasMaxLength(50);
            entity.Property(e => e.ProductName)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Supplier).HasMaxLength(50);
            entity.Property(e => e.Unit).HasMaxLength(50);
        });

        modelBuilder.Entity<Pvz>(entity =>
        {
            entity.HasKey(e => e.IdPvz).HasName("PK_PVZ_1");

            entity.ToTable("PVZ");

            entity.Property(e => e.IdPvz).HasColumnName("IdPVZ");
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.Pvznumber).HasColumnName("PVZNumber");
            entity.Property(e => e.Street).HasMaxLength(50);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.RoleId).ValueGeneratedNever();
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Login).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Patronymic).HasMaxLength(50);
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.Surname).HasMaxLength(50);

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

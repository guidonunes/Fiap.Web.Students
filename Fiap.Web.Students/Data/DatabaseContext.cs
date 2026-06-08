using Fiap.Web.Students.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Web.Students.Data;

public class DatabaseContext: DbContext
{
    

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }
    
    public virtual DbSet<RepresentativeModel> Representative { get; set; }
    public virtual DbSet<ClientModel> Client { get; set; }
    public virtual DbSet<ProductModel> Product { get; set; }
    public virtual DbSet<StoreModel> Store { get; set; }
    public virtual DbSet<SupplierModel> Supplier { get; set; }
    public virtual DbSet<OrderModel> Order { get; set; }
    public virtual DbSet<OrderProductModel> OrderProduct { get; set; }
  

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RepresentativeModel>(entity =>
        {
            entity.ToTable("TB_REPRESENTATIVE");

            entity.HasKey(e => e.RepresentativeId);

            entity.Property(e => e.RepresentativeId)
                .HasColumnName("REPRESENTATIVE_ID");

            entity.Property(e => e.RepresentativeName)
                .HasColumnName("REPRESENTATIVE_NAME")
                .IsRequired();

            entity.Property(e => e.Cpf)
                .HasColumnName("CPF")
                .IsRequired();

            entity.HasIndex(e => e.Cpf)
                .IsUnique();
        });

        modelBuilder.Entity<ClientModel>(entity =>
        {
            entity.ToTable("TB_CLIENT");
            
            entity.HasKey(e => e.ClientId);
            entity.Property(e => e.ClientId)
                .HasColumnName("CLIENT_ID");

            entity.Property(e => e.FirstName)
                .HasColumnName("FIRST_NAME")
                .IsRequired();
            
            entity.Property(e => e.LastName)
                .HasColumnName("LAST_NAME")
                .IsRequired();
            
            entity.Property(e => e.Email)
                .HasColumnName("EMAIL")
                .IsRequired();

            entity.Property(e => e.BirthDate)
                .HasColumnName("BIRTH_DATE")
                .IsRequired()
                .HasColumnType("date");

            entity.Property(e => e.Observation)
                .HasColumnName("OBSERVATION")
                .HasMaxLength(200);

            entity.Property(e => e.RepresentativeId)
                .HasColumnName("REPRESENTATIVE_ID");

            entity.HasOne(e => e.Representative)
                .WithMany()
                .HasForeignKey(e => e.RepresentativeId)
                .IsRequired();
        });

        modelBuilder.Entity<ProductModel>(entity =>
        {
            entity.ToTable("TB_PRODUCT");
            entity.HasKey(e => e.ProductId);
            entity.Property(p => p.Name).IsRequired();
            entity.Property(p => p.Description);
            entity.Property(p => p.Price).HasColumnType("NUMBER(18,2)");

            entity.HasOne(p => p.Supplier)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.SupplierId);
        });

        modelBuilder.Entity<StoreModel>(entity =>
        {
            entity.ToTable("TB_STORE");
            entity.HasKey(store => store.StoreId);
            entity.Property(store => store.Name).IsRequired();
            entity.Property(store => store.Address);
            
            entity.HasMany(store => store.Orders)
                .WithOne(p => p.Store)
                .HasForeignKey(store => store.StoreId);
        });

        modelBuilder.Entity<OrderModel>(entity =>
        {
            entity.ToTable("TB_ORDER");
            entity.HasKey(order => order.OrderId);
            entity.Property(order => order.OrderDate).HasColumnType("DATE");
            
            entity.HasOne(order => order.Client)
                .WithMany()
                .HasForeignKey(order => order.ClientId);

            entity.HasMany(order => order.OrderProducts)
                .WithOne(op => op.Order)
                .HasForeignKey(op => op.OrderId);
        });

        modelBuilder.Entity<SupplierModel>(entity =>
        {
            entity.ToTable("TB_SUPPLIER");
            entity.HasKey(e => e.SupplierId);
            entity.Property(e => e.Name).IsRequired();
        });

        modelBuilder.Entity<OrderProductModel>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.ProductId });

            entity.HasOne(e => e.Order)
                .WithMany(e => e.OrderProducts)
                .HasForeignKey(e => e.ProductId);
            
            entity.HasOne(e => e.Product)
                .WithMany(e=>e.OrderProducts)
                .HasForeignKey(e => e.OrderId);
        });
    }
}
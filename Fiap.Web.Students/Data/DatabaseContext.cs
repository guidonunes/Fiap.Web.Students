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
    }
}
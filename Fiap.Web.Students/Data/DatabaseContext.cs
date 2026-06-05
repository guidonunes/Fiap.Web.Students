using Fiap.Web.Students.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Web.Students.Data;

public class DatabaseContext: DbContext
{
    public virtual DbSet<RepresentativeModel> Representative { get; set; }
    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

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
    }
}
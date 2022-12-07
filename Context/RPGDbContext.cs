using Microsoft.EntityFrameworkCore;
using RpgApi.Models;

namespace RpgApi.Context;

public class RPGDbContext : DbContext
{
    public RPGDbContext(DbContextOptions<RPGDbContext> options)
        : base(options) { }

    public DbSet<Character> Characters { get; set; }
    public DbSet<RpgClass> RpgClasses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Character>(entity =>
        {
            entity.ToTable("Characters");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn()
                .IsRequired();

            entity.Property(e => e.Name)
                .HasColumnName("Name")
                .HasColumnType("nvarchar(80)")
                .IsRequired();

            entity.Property(e => e.Race)
                .HasColumnName("Race")
                .HasColumnType("nvarchar(80)")
                .IsRequired();

            entity.Property(e => e.Height)
                .HasColumnName("Height")
                .HasColumnType("real")
                .IsRequired();

            entity.Property(e => e.Weight)
                .HasColumnName("Weight")
                .HasColumnType("real")
                .IsRequired();

            entity.HasOne(e => e.RpgClass)
                .WithMany(p => p.Characters)
                .HasConstraintName("FK_Character_RPGClass")
                .HasForeignKey(key => key.RpgClassId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<RpgClass>(entity =>
        {
            entity.ToTable("RpgClasses");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn()
                .IsRequired();

            entity.Property(e => e.Name)
                .HasColumnName("Name")
                .HasColumnType("nvarchar(30)")
                .IsRequired();

            entity.Property(e => e.Description)
                .HasColumnName("Description")
                .HasColumnType("nvarchar(255)")
                .IsRequired();

            // entity.HasMany(e => e.Characters)
            //     .WithOne(x => x.RpgClass)
            //     .HasConstraintName("FK_RPGClass_Character")
            //     .HasForeignKey(key => key.Id)
            //     .OnDelete(DeleteBehavior.Cascade);
        });
    }
}

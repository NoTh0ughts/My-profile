using System.Configuration;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data;

#nullable disable
public class MyProjectsContext : DbContext
{
    public MyProjectsContext() : base() {  }

    public MyProjectsContext(DbContextOptions<MyProjectsContext> options)
        : base(options)
    {}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            //var conString = ConfigurationManager.ConnectionStrings["MyProjectsContext"].ConnectionString;
            
            //uncomment on migration
            var conString = "user=root;Database=profile;Server=localhost;Port=3306;Password=Stepup007;";
            
            optionsBuilder.UseMySql(conString, ServerVersion.AutoDetect(conString), 
                x => x.MigrationsAssembly("Migrations"));
        }
    }


    public virtual DbSet<Project> Projects { get; set; }
    public virtual DbSet<Technology> Technologies { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Project>().HasKey(p => p.ID);
        modelBuilder.Entity<Project>().Property(p => p.RepositoryName).IsRequired();
        modelBuilder.Entity<Project>().Property(p => p.CreatedAt).IsRequired();
        modelBuilder.Entity<Project>().Property(p => p.UpdatedAt).IsRequired();
        modelBuilder.Entity<Project>().Property(p => p.MainTitle)
            .IsRequired()
            .HasMaxLength(50);
        modelBuilder.Entity<Project>().Property(p => p.SubTitle) 
            .IsRequired()
            .HasMaxLength(300);

        modelBuilder.Entity<Project>().HasIndex(e => e.RepositoryName).IsUnique();

        modelBuilder.Entity<Project>()
            .HasMany(p => p.Technologies)
            .WithMany(t => t.Projects);

        modelBuilder.Entity<Technology>().HasKey(t => t.ID);
        modelBuilder.Entity<Technology>().HasIndex(x => x.Name).IsUnique();
        modelBuilder.Entity<Technology>().Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(30);

    }
}
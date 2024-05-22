using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models.Entities;
using Repositories.Configurations;


namespace Repositories;

public class DatabaseContext : DbContext
{

    #region dbset
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<MangaEntity> Mangas { get; set; }
    public DbSet<ChapterEntity> Chapters { get; set; }
    #endregion

    public DatabaseContext() { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = new ConfigurationBuilder()
                      .SetBasePath(Directory.GetCurrentDirectory())
                      .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        IConfigurationRoot configuration = builder.Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("MangaConnectionString"));
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new MangaConfiguration());
        builder.ApplyConfiguration(new ChapterConfiguration());
    }
}

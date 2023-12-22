using EntityFrameworkCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.Data;

public class FootballLeagueDbContext : DbContext
{
    public FootballLeagueDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Combine(path, "FootballLeague_EfCore.db");
    }


    public DbSet<Team> Teams { get; set; }
    public DbSet<Coach> Coaches { get; set; }

    public string DbPath { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //SQL Server example
        //optionsBuilder.UseSqlServer(
        //"Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=FootballLeague_EfCore; Encrypt=False");

        //This will be moved out to a settings file. DO NOT DO THAT IN REAL LIFE.
        // Added logging of queries
        // EnableSensitiveDataLogging and EnableDetailErrors is only for this tutorial. DO NOT DO THIS IN PROD.
        optionsBuilder.UseSqlite($"Data Source={DbPath}").LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging().EnableDetailedErrors();
    }

    // Creating seed data
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Team>().HasData(
            new Team
            {
                Id = 1,
                Name = "Nebraska Huskers",
                CreatedDate = DateTimeOffset.UtcNow.DateTime
            },
            new Team
            {
                Id = 2,
                Name = "Oklahoma Sooners",
                CreatedDate = DateTimeOffset.UtcNow.DateTime
            },
            new Team
            {
                Id = 3,
                Name = "Miami Hurricanes",
                CreatedDate = DateTimeOffset.UtcNow.DateTime
            }
        );
    }
}
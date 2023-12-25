using EntityFrameworkCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkCore.Data.Configurations;

internal class LeagueConfiguration : IEntityTypeConfiguration<League>
{
    public void Configure(EntityTypeBuilder<League> builder)
    {
        builder.HasData(
            new League
            {
                Id = 1,
                Name = "NCAA"
            },
            new League
            {
                Id = 2,
                Name = "NFL"
            },
            new League
            {
                Id = 3,
                Name = "XFL"
            }
        );
    }
}
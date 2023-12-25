using EntityFrameworkCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkCore.Data.Configurations;

internal class TeamConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.HasData(
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
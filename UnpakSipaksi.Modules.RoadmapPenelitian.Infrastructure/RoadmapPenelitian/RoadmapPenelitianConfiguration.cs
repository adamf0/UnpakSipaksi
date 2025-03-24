using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UnpakSipaksi.Modules.RoadmapPenelitian.Infrastructure.RoadmapPenelitian
{
    internal sealed class RoadmapPenelitianConfiguration : IEntityTypeConfiguration<Domain.RoadmapPenelitian.RoadmapPenelitian>
    {
        public void Configure(EntityTypeBuilder<Domain.RoadmapPenelitian.RoadmapPenelitian> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}

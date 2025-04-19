using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UnpakSipaksi.Modules.Roadmap.Infrastructure.Roadmap
{
    internal sealed class RoadmapConfiguration : IEntityTypeConfiguration<Domain.Roadmap.Roadmap>
    {
        public void Configure(EntityTypeBuilder<Domain.Roadmap.Roadmap> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}

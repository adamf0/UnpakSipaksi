using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Infrastructure.PeningkatanKeberdayaanMitra
{
    internal sealed class PeningkatanKeberdayaanMitraConfiguration : IEntityTypeConfiguration<Domain.PeningkatanKeberdayaanMitra.PeningkatanKeberdayaanMitra>
    {
        public void Configure(EntityTypeBuilder<Domain.PeningkatanKeberdayaanMitra.PeningkatanKeberdayaanMitra> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}

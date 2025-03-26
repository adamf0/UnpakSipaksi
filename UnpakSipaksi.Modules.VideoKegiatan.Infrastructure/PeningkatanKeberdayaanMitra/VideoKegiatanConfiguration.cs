using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UnpakSipaksi.Modules.VideoKegiatan.Infrastructure.VideoKegiatan
{
    internal sealed class VideoKegiatanConfiguration : IEntityTypeConfiguration<Domain.VideoKegiatan.VideoKegiatan>
    {
        public void Configure(EntityTypeBuilder<Domain.VideoKegiatan.VideoKegiatan> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}

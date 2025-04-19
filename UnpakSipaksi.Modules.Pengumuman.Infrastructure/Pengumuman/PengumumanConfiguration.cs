using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UnpakSipaksi.Modules.Pengumuman.Infrastructure.Pengumuman
{
    internal sealed class PengumumanConfiguration : IEntityTypeConfiguration<Domain.Pengumuman.Pengumuman>
    {
        public void Configure(EntityTypeBuilder<Domain.Pengumuman.Pengumuman> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}

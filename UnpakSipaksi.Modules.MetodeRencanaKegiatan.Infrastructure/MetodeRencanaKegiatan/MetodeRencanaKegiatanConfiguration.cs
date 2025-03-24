using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UnpakSipaksi.Modules.MetodeRencanaKegiatan.Infrastructure.MetodeRencanaKegiatan
{
    internal sealed class MetodeRencanaKegiatanConfiguration : IEntityTypeConfiguration<Domain.MetodeRencanaKegiatan.MetodeRencanaKegiatan>
    {
        public void Configure(EntityTypeBuilder<Domain.MetodeRencanaKegiatan.MetodeRencanaKegiatan> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}

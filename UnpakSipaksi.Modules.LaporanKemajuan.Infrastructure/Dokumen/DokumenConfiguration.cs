using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UnpakSipaksi.Modules.LaporanKemajuan.Infrastructure.Dokumen
{
    internal sealed class DokumenConfiguration : IEntityTypeConfiguration<Domain.Dokumen.Dokumen>
    {
        public void Configure(EntityTypeBuilder<Domain.Dokumen.Dokumen> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}

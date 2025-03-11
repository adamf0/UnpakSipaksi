using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Infrastructure.KetajamanPerumusanMasalah
{
    internal sealed class KetajamanPerumusanMasalahConfiguration : IEntityTypeConfiguration<Domain.KetajamanPerumusanMasalah.KetajamanPerumusanMasalah>
    {
        public void Configure(EntityTypeBuilder<Domain.KetajamanPerumusanMasalah.KetajamanPerumusanMasalah> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}

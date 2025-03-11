using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Infrastructure.KualitasKuantitasPublikasiProsiding
{
    internal sealed class KualitasKuantitasPublikasiProsidingConfiguration : IEntityTypeConfiguration<Domain.KualitasKuantitasPublikasiProsiding.KualitasKuantitasPublikasiProsiding>
    {
        public void Configure(EntityTypeBuilder<Domain.KualitasKuantitasPublikasiProsiding.KualitasKuantitasPublikasiProsiding> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}

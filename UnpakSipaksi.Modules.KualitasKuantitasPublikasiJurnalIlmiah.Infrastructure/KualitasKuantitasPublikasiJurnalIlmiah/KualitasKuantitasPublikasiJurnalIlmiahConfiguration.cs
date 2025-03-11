using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiJurnalIlmiah.Infrastructure.KualitasKuantitasPublikasiJurnalIlmiah
{
    internal sealed class KualitasKuantitasPublikasiJurnalIlmiahConfiguration : IEntityTypeConfiguration<Domain.KualitasKuantitasPublikasiJurnalIlmiah.KualitasKuantitasPublikasiJurnalIlmiah>
    {
        public void Configure(EntityTypeBuilder<Domain.KualitasKuantitasPublikasiJurnalIlmiah.KualitasKuantitasPublikasiJurnalIlmiah> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}

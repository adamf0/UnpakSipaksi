using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UnpakSipaksi.Modules.RelevansiKualitasReferensi.Infrastructure.RelevansiKualitasReferensi
{
    internal sealed class RelevansiKualitasReferensiConfiguration : IEntityTypeConfiguration<Domain.RelevansiKualitasReferensi.RelevansiKualitasReferensi>
    {
        public void Configure(EntityTypeBuilder<Domain.RelevansiKualitasReferensi.RelevansiKualitasReferensi> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}

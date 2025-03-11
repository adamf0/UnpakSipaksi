using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UnpakSipaksi.Modules.KualitasIpteks.Infrastructure.KualitasIpteks
{
    internal sealed class KualitasIpteksConfiguration : IEntityTypeConfiguration<Domain.KualitasIpteks.KualitasIpteks>
    {
        public void Configure(EntityTypeBuilder<Domain.KualitasIpteks.KualitasIpteks> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}

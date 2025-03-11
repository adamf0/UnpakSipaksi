using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UnpakSipaksi.Modules.KewajaranTahapanTarget.Infrastructure.KewajaranTahapanTarget
{
    internal sealed class KewajaranTahapanTargetConfiguration : IEntityTypeConfiguration<Domain.KewajaranTahapanTarget.KewajaranTahapanTarget>
    {
        public void Configure(EntityTypeBuilder<Domain.KewajaranTahapanTarget.KewajaranTahapanTarget> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}

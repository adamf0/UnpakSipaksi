using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UnpakSipaksi.Modules.SotaKebaharuan.Infrastructure.SotaKebaharuan
{
    internal sealed class SotaKebaharuanConfiguration : IEntityTypeConfiguration<Domain.SotaKebaharuan.SotaKebaharuan>
    {
        public void Configure(EntityTypeBuilder<Domain.SotaKebaharuan.SotaKebaharuan> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}

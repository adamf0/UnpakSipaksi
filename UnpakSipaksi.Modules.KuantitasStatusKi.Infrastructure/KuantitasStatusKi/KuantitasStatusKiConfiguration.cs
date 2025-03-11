using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UnpakSipaksi.Modules.KuantitasStatusKi.Infrastructure.KuantitasStatusKi
{
    internal sealed class KuantitasStatusKiConfiguration : IEntityTypeConfiguration<Domain.KuantitasStatusKi.KuantitasStatusKi>
    {
        public void Configure(EntityTypeBuilder<Domain.KuantitasStatusKi.KuantitasStatusKi> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UnpakSipaksi.Modules.Metode.Infrastructure.Metode
{
    internal sealed class MetodeConfiguration : IEntityTypeConfiguration<Domain.Metode.Metode>
    {
        public void Configure(EntityTypeBuilder<Domain.Metode.Metode> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}

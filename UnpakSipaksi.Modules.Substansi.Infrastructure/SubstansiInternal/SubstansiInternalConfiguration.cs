using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UnpakSipaksi.Modules.Substansi.Infrastructure.SubstansiInternal
{
    internal sealed class SubstansiInternalConfiguration : IEntityTypeConfiguration<Domain.SubstansiInternal.SubstansiInternal>
    {
        public void Configure(EntityTypeBuilder<Domain.SubstansiInternal.SubstansiInternal> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}

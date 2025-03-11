using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UnpakSipaksi.Modules.Komponen.Infrastructure.Komponen
{
    internal sealed class KomponenConfiguration : IEntityTypeConfiguration<Domain.Komponen.Komponen>
    {
        public void Configure(EntityTypeBuilder<Domain.Komponen.Komponen> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}

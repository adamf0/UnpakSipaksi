using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UnpakSipaksi.Modules.KategoriSkema.Infrastructure.KategoriSkema
{
    internal sealed class KategoriSkemaConfiguration : IEntityTypeConfiguration<Domain.KategoriSkema.KategoriSkema>
    {
        public void Configure(EntityTypeBuilder<Domain.KategoriSkema.KategoriSkema> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}

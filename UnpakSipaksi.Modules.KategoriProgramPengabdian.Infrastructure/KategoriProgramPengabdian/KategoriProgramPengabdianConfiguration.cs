using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UnpakSipaksi.Modules.KategoriProgramPengabdian.Infrastructure.KategoriProgramPengabdian
{
    internal sealed class KategoriProgramPengabdianConfiguration : IEntityTypeConfiguration<Domain.KategoriProgramPengabdian.KategoriProgramPengabdian>
    {
        public void Configure(EntityTypeBuilder<Domain.KategoriProgramPengabdian.KategoriProgramPengabdian> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}

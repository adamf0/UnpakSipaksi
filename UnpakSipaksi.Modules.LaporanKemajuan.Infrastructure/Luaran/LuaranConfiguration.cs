using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UnpakSipaksi.Modules.LaporanKemajuan.Infrastructure.Luaran
{
    internal sealed class LuaranConfiguration : IEntityTypeConfiguration<Domain.Luaran.Luaran>
    {
        public void Configure(EntityTypeBuilder<Domain.Luaran.Luaran> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UnpakSipaksi.Modules.PotensiKetercapaianLuaranDijanjikan.Infrastructure.PotensiKetercapaianLuaranDijanjikan
{
    internal sealed class PotensiKetercapaianLuaranDijanjikanConfiguration : IEntityTypeConfiguration<Domain.PotensiKetercapaianLuaranDijanjikan.PotensiKetercapaianLuaranDijanjikan>
    {
        public void Configure(EntityTypeBuilder<Domain.PotensiKetercapaianLuaranDijanjikan.PotensiKetercapaianLuaranDijanjikan> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}

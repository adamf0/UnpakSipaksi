using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UnpakSipaksi.Modules.LuaranArtikel.Infrastructure.LuaranArtikel
{
    internal sealed class LuaranArtikelConfiguration : IEntityTypeConfiguration<Domain.LuaranArtikel.LuaranArtikel>
    {
        public void Configure(EntityTypeBuilder<Domain.LuaranArtikel.LuaranArtikel> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}

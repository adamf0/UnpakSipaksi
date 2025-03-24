using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UnpakSipaksi.Modules.RelevansiProdukKepentinganNasional.Infrastructure.RelevansiProdukKepentinganNasional
{
    internal sealed class RelevansiProdukKepentinganNasionalConfiguration : IEntityTypeConfiguration<Domain.RelevansiProdukKepentinganNasional.RelevansiProdukKepentinganNasional>
    {
        public void Configure(EntityTypeBuilder<Domain.RelevansiProdukKepentinganNasional.RelevansiProdukKepentinganNasional> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}

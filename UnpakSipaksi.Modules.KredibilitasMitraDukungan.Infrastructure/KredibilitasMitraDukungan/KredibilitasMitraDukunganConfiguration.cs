using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UnpakSipaksi.Modules.KredibilitasMitraDukungan.Infrastructure.KredibilitasMitraDukungan
{
    internal sealed class KredibilitasMitraDukunganConfiguration : IEntityTypeConfiguration<Domain.KredibilitasMitraDukungan.KredibilitasMitraDukungan>
    {
        public void Configure(EntityTypeBuilder<Domain.KredibilitasMitraDukungan.KredibilitasMitraDukungan> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UnpakSipaksi.Modules.KetajamanAnalisis.Infrastructure.KetajamanAnalisis
{
    internal sealed class KetajamanAnalisisConfiguration : IEntityTypeConfiguration<Domain.KetajamanAnalisis.KetajamanAnalisis>
    {
        public void Configure(EntityTypeBuilder<Domain.KetajamanAnalisis.KetajamanAnalisis> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}

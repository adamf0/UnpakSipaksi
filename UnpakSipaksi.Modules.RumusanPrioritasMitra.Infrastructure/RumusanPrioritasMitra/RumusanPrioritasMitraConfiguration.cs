using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UnpakSipaksi.Modules.RumusanPrioritasMitra.Infrastructure.RumusanPrioritasMitra
{
    internal sealed class RumusanPrioritasMitraConfiguration : IEntityTypeConfiguration<Domain.RumusanPrioritasMitra.RumusanPrioritasMitra>
    {
        public void Configure(EntityTypeBuilder<Domain.RumusanPrioritasMitra.RumusanPrioritasMitra> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UnpakSipaksi.Modules.ModelFeasibilityStudys.Infrastructure.ModelFeasibilityStudys
{
    internal sealed class ModelFeasibilityStudysConfiguration : IEntityTypeConfiguration<Domain.ModelFeasibilityStudys.ModelFeasibilityStudys>
    {
        public void Configure(EntityTypeBuilder<Domain.ModelFeasibilityStudys.ModelFeasibilityStudys> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}

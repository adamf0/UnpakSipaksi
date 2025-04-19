using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UnpakSipaksi.Modules.UrgensiPenelitian.Infrastructure.UrgensiPenelitian
{
    internal sealed class UrgensiPenelitianConfiguration : IEntityTypeConfiguration<Domain.UrgensiPenelitian.UrgensiPenelitian>
    {
        public void Configure(EntityTypeBuilder<Domain.UrgensiPenelitian.UrgensiPenelitian> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}

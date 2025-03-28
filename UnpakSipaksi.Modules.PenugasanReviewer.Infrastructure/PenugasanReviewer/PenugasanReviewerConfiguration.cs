using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UnpakSipaksi.Modules.PenugasanReviewer.Infrastructure.PenugasanReviewer
{
    internal sealed class PenugasanReviewerConfiguration : IEntityTypeConfiguration<Domain.PenugasanReviewer.PenugasanReviewer>
    {
        public void Configure(EntityTypeBuilder<Domain.PenugasanReviewer.PenugasanReviewer> builder)
        {
            //builder.HasOne<Category>().WithMany();
        }
    }
}

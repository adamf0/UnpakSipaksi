using Microsoft.EntityFrameworkCore;
using System;
using UnpakSipaksi.Modules.PenugasanReviewer.Domain.PenugasanReviewer;
using UnpakSipaksi.Modules.PenugasanReviewer.Infrastructure.Database;

namespace UnpakSipaksi.Modules.PenugasanReviewer.Infrastructure.PenugasanReviewer
{
    internal sealed class PenugasanReviewerRepository(PenugasanReviewerDbContext context) : IPenugasanReviewerRepository
    {
        public async Task<Domain.PenugasanReviewer.PenugasanReviewer> GetAsync(Guid Uuid, CancellationToken cancellationToken = default)
        {
            Domain.PenugasanReviewer.PenugasanReviewer PenugasanReviewer = await context.PenugasanReviewer.SingleOrDefaultAsync(e => e.Uuid == Uuid, cancellationToken);
            return PenugasanReviewer;
        }

        public async Task DeleteAsync(Domain.PenugasanReviewer.PenugasanReviewer PenugasanReviewer)
        {
            context.PenugasanReviewer.Remove(PenugasanReviewer);
        }

        public void Insert(Domain.PenugasanReviewer.PenugasanReviewer PenugasanReviewer)
        {
            context.PenugasanReviewer.Add(PenugasanReviewer);
        }
    }
}

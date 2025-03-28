using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenugasanReviewer.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenugasanReviewer.Domain.PenugasanReviewer;

namespace UnpakSipaksi.Modules.PenugasanReviewer.Application.UpdatePenugasanReviewer
{
    internal sealed class StatusPenugasanReviewerCommandHandler(
    IPenugasanReviewerRepository PenugasanReviewerRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<StatusPenugasanReviewerCommand>
    {
        public async Task<Result> Handle(StatusPenugasanReviewerCommand request, CancellationToken cancellationToken)
        {
            Domain.PenugasanReviewer.PenugasanReviewer? existingPenugasanReviewer = await PenugasanReviewerRepository.GetAsync(request.Uuid, cancellationToken);

            if (existingPenugasanReviewer is null)
            {
                Result.Failure(PenugasanReviewerErrors.NotFound(request.Uuid));
            }

            Result<Domain.PenugasanReviewer.PenugasanReviewer> asset = Domain.PenugasanReviewer.PenugasanReviewer.Update(existingPenugasanReviewer!)
                         .ChangeNidn(request.Nidn)
                         .ChangeStatus(request.Status)
                         .Build();

            if (asset.IsFailure)
            {
                return Result.Failure<Guid>(asset.Error);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

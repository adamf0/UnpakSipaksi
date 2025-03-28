using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenugasanReviewer.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenugasanReviewer.Domain.PenugasanReviewer;

namespace UnpakSipaksi.Modules.PenugasanReviewer.Application.CreatePenugasanReviewer
{
    internal sealed class CreatePenugasanReviewerCommandHandler(
    IPenugasanReviewerRepository PenugasanReviewerRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreatePenugasanReviewerCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreatePenugasanReviewerCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.PenugasanReviewer.PenugasanReviewer> result = Domain.PenugasanReviewer.PenugasanReviewer.Create(
                request.Nidn,
                request.Status
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            PenugasanReviewerRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}

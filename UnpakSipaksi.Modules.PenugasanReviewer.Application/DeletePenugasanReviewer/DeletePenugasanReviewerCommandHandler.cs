using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenugasanReviewer.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenugasanReviewer.Domain.PenugasanReviewer;

namespace UnpakSipaksi.Modules.PenugasanReviewer.Application.DeletePenugasanReviewer
{
    internal sealed class DeletePenugasanReviewerCommandHandler(
    IPenugasanReviewerRepository PenugasanReviewerRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeletePenugasanReviewerCommand>
    {
        public async Task<Result> Handle(DeletePenugasanReviewerCommand request, CancellationToken cancellationToken)
        {
            Domain.PenugasanReviewer.PenugasanReviewer? existingPenugasanReviewer = await PenugasanReviewerRepository.GetAsync(request.uuid, cancellationToken);

            if (existingPenugasanReviewer is null)
            {
                return Result.Failure(PenugasanReviewerErrors.NotFound(request.uuid));
            }

            await PenugasanReviewerRepository.DeleteAsync(existingPenugasanReviewer!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

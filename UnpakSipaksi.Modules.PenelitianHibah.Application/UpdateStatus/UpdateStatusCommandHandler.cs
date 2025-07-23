using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.UpdateStatus
{
    internal sealed class UpdateStatusCommandHandler(
        IPenelitianHibahRepository penelitianHibahRepository,
        IUnitOfWorkHibah unitOfWork)
        : ICommandHandler<UpdateStatusCommand>
    {
        public async Task<Result> Handle(UpdateStatusCommand request, CancellationToken cancellationToken)
        {
            Domain.PenelitianHibah.PenelitianHibah? existingPenelitianHibah = await penelitianHibahRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            var result = Domain.PenelitianHibah.PenelitianHibah.UpdateStatus(
                existingPenelitianHibah,
                request.Status
            );

            if (result.IsFailure)
                return Result.Failure(result.Error);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

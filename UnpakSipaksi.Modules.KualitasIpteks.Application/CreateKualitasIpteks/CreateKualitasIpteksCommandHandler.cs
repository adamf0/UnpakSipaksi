using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KualitasIpteks.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KualitasIpteks.Domain.KualitasIpteks;

namespace UnpakSipaksi.Modules.KualitasIpteks.Application.CreateKualitasIpteks
{
    internal sealed class CreateKualitasIpteksCommandHandler(
    IKualitasIpteksRepository KualitasIpteksRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateKualitasIpteksCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateKualitasIpteksCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.KualitasIpteks.KualitasIpteks> result = Domain.KualitasIpteks.KualitasIpteks.Create(
                request.Nama,
                request.Nilai
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            KualitasIpteksRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}

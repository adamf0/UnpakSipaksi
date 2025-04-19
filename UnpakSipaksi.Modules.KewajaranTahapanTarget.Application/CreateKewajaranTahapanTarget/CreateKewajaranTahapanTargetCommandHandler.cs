using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KewajaranTahapanTarget.Domain.KewajaranTahapanTarget;
using UnpakSipaksi.Modules.KewajaranTahapanTarget.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.KewajaranTahapanTarget.Application.CreateKewajaranTahapanTarget
{
    internal sealed class CreateKewajaranTahapanTargetCommandHandler(
    IKewajaranTahapanTargetRepository KewajaranTahapanTargetRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateKewajaranTahapanTargetCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateKewajaranTahapanTargetCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.KewajaranTahapanTarget.KewajaranTahapanTarget> result = Domain.KewajaranTahapanTarget.KewajaranTahapanTarget.Create(
                request.Nama,
                request.Nilai
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            KewajaranTahapanTargetRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}

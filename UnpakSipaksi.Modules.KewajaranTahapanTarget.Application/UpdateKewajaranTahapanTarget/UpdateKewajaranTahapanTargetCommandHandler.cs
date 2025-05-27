using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KewajaranTahapanTarget.Domain.KewajaranTahapanTarget;
using UnpakSipaksi.Modules.KewajaranTahapanTarget.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.KewajaranTahapanTarget.Application.UpdateKewajaranTahapanTarget
{
    internal sealed class UpdateKewajaranTahapanTargetCommandHandler(
    IKewajaranTahapanTargetRepository KewajaranTahapanTargetRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateKewajaranTahapanTargetCommand>
    {
        public async Task<Result> Handle(UpdateKewajaranTahapanTargetCommand request, CancellationToken cancellationToken)
        {
            Domain.KewajaranTahapanTarget.KewajaranTahapanTarget? existingKewajaranTahapanTarget = await KewajaranTahapanTargetRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingKewajaranTahapanTarget is null)
            {
                Result.Failure(KewajaranTahapanTargetErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Result<Domain.KewajaranTahapanTarget.KewajaranTahapanTarget> asset = Domain.KewajaranTahapanTarget.KewajaranTahapanTarget.Update(existingKewajaranTahapanTarget!)
                         .ChangeNama(request.Nama)
                         .ChangeNilai(request.Nilai)
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

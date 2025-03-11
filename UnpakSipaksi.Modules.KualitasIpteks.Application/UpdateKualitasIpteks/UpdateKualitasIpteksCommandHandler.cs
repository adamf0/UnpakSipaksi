using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KualitasIpteks.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KualitasIpteks.Domain.KualitasIpteks;

namespace UnpakSipaksi.Modules.KualitasIpteks.Application.UpdateKualitasIpteks
{
    internal sealed class UpdateKualitasIpteksCommandHandler(
    IKualitasIpteksRepository KualitasIpteksRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateKualitasIpteksCommand>
    {
        public async Task<Result> Handle(UpdateKualitasIpteksCommand request, CancellationToken cancellationToken)
        {
            Domain.KualitasIpteks.KualitasIpteks? existingKualitasIpteks = await KualitasIpteksRepository.GetAsync(request.Uuid, cancellationToken);

            if (existingKualitasIpteks is null)
            {
                Result.Failure(KualitasIpteksErrors.NotFound(request.Uuid));
            }

            Result<Domain.KualitasIpteks.KualitasIpteks> asset = Domain.KualitasIpteks.KualitasIpteks.Update(existingKualitasIpteks!)
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

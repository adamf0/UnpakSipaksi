using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Domain.KualitasKuantitasPublikasiProsiding;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Application.UpdateKualitasKuantitasPublikasiProsiding
{
    internal sealed class UpdateKualitasKuantitasPublikasiProsidingCommandHandler(
    IKualitasKuantitasPublikasiProsidingRepository KualitasKuantitasPublikasiProsidingRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateKualitasKuantitasPublikasiProsidingCommand>
    {
        public async Task<Result> Handle(UpdateKualitasKuantitasPublikasiProsidingCommand request, CancellationToken cancellationToken)
        {
            Domain.KualitasKuantitasPublikasiProsiding.KualitasKuantitasPublikasiProsiding? existingKualitasKuantitasPublikasiProsiding = await KualitasKuantitasPublikasiProsidingRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingKualitasKuantitasPublikasiProsiding is null)
            {
                Result.Failure(KualitasKuantitasPublikasiProsidingErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Result<Domain.KualitasKuantitasPublikasiProsiding.KualitasKuantitasPublikasiProsiding> asset = Domain.KualitasKuantitasPublikasiProsiding.KualitasKuantitasPublikasiProsiding.Update(existingKualitasKuantitasPublikasiProsiding!)
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

using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Domain.KetajamanPerumusanMasalah;

namespace UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Application.UpdateKetajamanPerumusanMasalah
{
    internal sealed class UpdateKetajamanPerumusanMasalahCommandHandler(
    IKetajamanPerumusanMasalahRepository KetajamanPerumusanMasalahRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateKetajamanPerumusanMasalahCommand>
    {
        public async Task<Result> Handle(UpdateKetajamanPerumusanMasalahCommand request, CancellationToken cancellationToken)
        {
            Domain.KetajamanPerumusanMasalah.KetajamanPerumusanMasalah? existingKetajamanPerumusanMasalah = await KetajamanPerumusanMasalahRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingKetajamanPerumusanMasalah is null)
            {
                Result.Failure(KetajamanPerumusanMasalahErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Result<Domain.KetajamanPerumusanMasalah.KetajamanPerumusanMasalah> asset = Domain.KetajamanPerumusanMasalah.KetajamanPerumusanMasalah.Update(existingKetajamanPerumusanMasalah!)
                         .ChangeNama(request.Nama)
                         .ChangeSkor(request.Skor)
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

using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KetajamanAnalisis.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KetajamanAnalisis.Domain.KetajamanAnalisis;

namespace UnpakSipaksi.Modules.KetajamanAnalisis.Application.UpdateKetajamanAnalisis
{
    internal sealed class UpdateKetajamanAnalisisCommandHandler(
    IKetajamanAnalisisRepository KetajamanAnalisisRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateKetajamanAnalisisCommand>
    {
        public async Task<Result> Handle(UpdateKetajamanAnalisisCommand request, CancellationToken cancellationToken)
        {
            Domain.KetajamanAnalisis.KetajamanAnalisis? existingKetajamanAnalisis = await KetajamanAnalisisRepository.GetAsync(request.Uuid, cancellationToken);

            if (existingKetajamanAnalisis is null)
            {
                Result.Failure(KetajamanAnalisisErrors.NotFound(request.Uuid));
            }

            Result<Domain.KetajamanAnalisis.KetajamanAnalisis> asset = Domain.KetajamanAnalisis.KetajamanAnalisis.Update(existingKetajamanAnalisis!)
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

using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Domain.KetajamanPerumusanMasalah;

namespace UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Application.CreateKetajamanPerumusanMasalah
{
    internal sealed class CreateKetajamanPerumusanMasalahCommandHandler(
    IKetajamanPerumusanMasalahRepository KetajamanPerumusanMasalahRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateKetajamanPerumusanMasalahCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateKetajamanPerumusanMasalahCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.KetajamanPerumusanMasalah.KetajamanPerumusanMasalah> result = Domain.KetajamanPerumusanMasalah.KetajamanPerumusanMasalah.Create(
                request.Nama,
                request.Skor
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            KetajamanPerumusanMasalahRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}

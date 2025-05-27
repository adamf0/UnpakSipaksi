using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Domain.KetajamanPerumusanMasalah;

namespace UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Application.DeleteKetajamanPerumusanMasalah
{
    internal sealed class DeleteKetajamanPerumusanMasalahCommandHandler(
    IKetajamanPerumusanMasalahRepository KetajamanPerumusanMasalahRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteKetajamanPerumusanMasalahCommand>
    {
        public async Task<Result> Handle(DeleteKetajamanPerumusanMasalahCommand request, CancellationToken cancellationToken)
        {
            Domain.KetajamanPerumusanMasalah.KetajamanPerumusanMasalah? existingKetajamanPerumusanMasalah = await KetajamanPerumusanMasalahRepository.GetAsync(Guid.Parse(request.uuid), cancellationToken);

            if (existingKetajamanPerumusanMasalah is null)
            {
                return Result.Failure(KetajamanPerumusanMasalahErrors.NotFound(Guid.Parse(request.uuid)));
            }

            await KetajamanPerumusanMasalahRepository.DeleteAsync(existingKetajamanPerumusanMasalah!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

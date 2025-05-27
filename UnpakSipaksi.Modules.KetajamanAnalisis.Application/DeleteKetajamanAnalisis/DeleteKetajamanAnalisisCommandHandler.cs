using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KetajamanAnalisis.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KetajamanAnalisis.Domain.KetajamanAnalisis;

namespace UnpakSipaksi.Modules.KetajamanAnalisis.Application.DeleteKetajamanAnalisis
{
    internal sealed class DeleteKetajamanAnalisisCommandHandler(
    IKetajamanAnalisisRepository KetajamanAnalisisRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteKetajamanAnalisisCommand>
    {
        public async Task<Result> Handle(DeleteKetajamanAnalisisCommand request, CancellationToken cancellationToken)
        {
            Domain.KetajamanAnalisis.KetajamanAnalisis? existingKetajamanAnalisis = await KetajamanAnalisisRepository.GetAsync(Guid.Parse(request.uuid), cancellationToken);

            if (existingKetajamanAnalisis is null)
            {
                return Result.Failure(KetajamanAnalisisErrors.NotFound(Guid.Parse(request.uuid)));
            }

            await KetajamanAnalisisRepository.DeleteAsync(existingKetajamanAnalisis!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

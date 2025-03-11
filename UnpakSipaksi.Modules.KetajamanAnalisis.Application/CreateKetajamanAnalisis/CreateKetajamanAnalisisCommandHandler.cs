using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KetajamanAnalisis.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KetajamanAnalisis.Domain.KetajamanAnalisis;

namespace UnpakSipaksi.Modules.KetajamanAnalisis.Application.CreateKetajamanAnalisis
{
    internal sealed class CreateKetajamanAnalisisCommandHandler(
    IKetajamanAnalisisRepository KetajamanAnalisisRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateKetajamanAnalisisCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateKetajamanAnalisisCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.KetajamanAnalisis.KetajamanAnalisis> result = Domain.KetajamanAnalisis.KetajamanAnalisis.Create(
                request.Nama,
                request.Nilai
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            KetajamanAnalisisRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}

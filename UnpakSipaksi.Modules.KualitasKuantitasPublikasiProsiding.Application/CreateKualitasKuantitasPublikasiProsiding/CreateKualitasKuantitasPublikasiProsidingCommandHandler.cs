using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Domain.KualitasKuantitasPublikasiProsiding;

namespace UnpakSipaksi.Modules.KualitasKuantitasPublikasiProsiding.Application.CreateKualitasKuantitasPublikasiProsiding
{
    internal sealed class CreateKualitasKuantitasPublikasiProsidingCommandHandler(
    IKualitasKuantitasPublikasiProsidingRepository KualitasKuantitasPublikasiProsidingRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateKualitasKuantitasPublikasiProsidingCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateKualitasKuantitasPublikasiProsidingCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.KualitasKuantitasPublikasiProsiding.KualitasKuantitasPublikasiProsiding> result = Domain.KualitasKuantitasPublikasiProsiding.KualitasKuantitasPublikasiProsiding.Create(
                request.Nama,
                request.Nilai
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            KualitasKuantitasPublikasiProsidingRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}

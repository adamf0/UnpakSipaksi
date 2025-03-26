using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.SotaKebaharuan.Application.Abstractions.Data;
using UnpakSipaksi.Modules.SotaKebaharuan.Domain.SotaKebaharuan;

namespace UnpakSipaksi.Modules.SotaKebaharuan.Application.CreateSotaKebaharuan
{
    internal sealed class CreateSotaKebaharuanCommandHandler(
    ISotaKebaharuanRepository SotaKebaharuanRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateSotaKebaharuanCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateSotaKebaharuanCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.SotaKebaharuan.SotaKebaharuan> result = Domain.SotaKebaharuan.SotaKebaharuan.Create(
                request.Nama,
                request.BobotPDP,
                request.BobotTerapan,
                request.BobotKerjasama,
                request.BobotPenelitianDasar,
                request.Skor
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            SotaKebaharuanRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}

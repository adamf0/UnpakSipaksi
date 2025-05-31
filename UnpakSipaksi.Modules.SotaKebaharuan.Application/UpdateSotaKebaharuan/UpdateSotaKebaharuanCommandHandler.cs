using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.SotaKebaharuan.Application.Abstractions.Data;
using UnpakSipaksi.Modules.SotaKebaharuan.Domain.SotaKebaharuan;

namespace UnpakSipaksi.Modules.SotaKebaharuan.Application.UpdateSotaKebaharuan
{
    internal sealed class UpdateSotaKebaharuanCommandHandler(
    ISotaKebaharuanRepository SotaKebaharuanRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateSotaKebaharuanCommand>
    {
        public async Task<Result> Handle(UpdateSotaKebaharuanCommand request, CancellationToken cancellationToken)
        {
            Domain.SotaKebaharuan.SotaKebaharuan? existingSotaKebaharuan = await SotaKebaharuanRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingSotaKebaharuan is null)
            {
                return Result.Failure(SotaKebaharuanErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Result<Domain.SotaKebaharuan.SotaKebaharuan> asset = Domain.SotaKebaharuan.SotaKebaharuan.Update(existingSotaKebaharuan!)
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

using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.SotaKebaharuan.Application.Abstractions.Data;
using UnpakSipaksi.Modules.SotaKebaharuan.Domain.SotaKebaharuan;

namespace UnpakSipaksi.Modules.SotaKebaharuan.Application.DeleteSotaKebaharuan
{
    internal sealed class DeleteSotaKebaharuanCommandHandler(
    ISotaKebaharuanRepository SotaKebaharuanRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteSotaKebaharuanCommand>
    {
        public async Task<Result> Handle(DeleteSotaKebaharuanCommand request, CancellationToken cancellationToken)
        {
            Domain.SotaKebaharuan.SotaKebaharuan? existingSotaKebaharuan = await SotaKebaharuanRepository.GetAsync(Guid.Parse(request.uuid), cancellationToken);

            if (existingSotaKebaharuan is null)
            {
                return Result.Failure(SotaKebaharuanErrors.NotFound(Guid.Parse(request.uuid)));
            }

            await SotaKebaharuanRepository.DeleteAsync(existingSotaKebaharuan!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Pengumuman.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Pengumuman.Domain.Pengumuman;

namespace UnpakSipaksi.Modules.Pengumuman.Application.DeletePengumuman
{
    internal sealed class DeletePengumumanCommandHandler(
    IPengumumanRepository PengumumanRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeletePengumumanCommand>
    {
        public async Task<Result> Handle(DeletePengumumanCommand request, CancellationToken cancellationToken)
        {
            Domain.Pengumuman.Pengumuman? existingPengumuman = await PengumumanRepository.GetAsync(request.uuid, cancellationToken);

            if (existingPengumuman is null)
            {
                return Result.Failure(PengumumanErrors.NotFound(request.uuid));
            }

            await PengumumanRepository.DeleteAsync(existingPengumuman!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.MetodeRencanaKegiatan.Application.Abstractions.Data;
using UnpakSipaksi.Modules.MetodeRencanaKegiatan.Domain.MetodeRencanaKegiatan;

namespace UnpakSipaksi.Modules.MetodeRencanaKegiatan.Application.DeleteMetodeRencanaKegiatan
{
    internal sealed class DeleteMetodeRencanaKegiatanCommandHandler(
    IMetodeRencanaKegiatanRepository MetodeRencanaKegiatanRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteMetodeRencanaKegiatanCommand>
    {
        public async Task<Result> Handle(DeleteMetodeRencanaKegiatanCommand request, CancellationToken cancellationToken)
        {
            Domain.MetodeRencanaKegiatan.MetodeRencanaKegiatan? existingMetodeRencanaKegiatan = await MetodeRencanaKegiatanRepository.GetAsync(Guid.Parse(request.uuid), cancellationToken);

            if (existingMetodeRencanaKegiatan is null)
            {
                return Result.Failure(MetodeRencanaKegiatanErrors.NotFound(Guid.Parse(request.uuid)));
            }

            await MetodeRencanaKegiatanRepository.DeleteAsync(existingMetodeRencanaKegiatan!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

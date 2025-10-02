using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.LaporanKemajuan.Application.Abstractions.Data;
using UnpakSipaksi.Modules.LaporanKemajuan.Domain.Dokumen;

namespace UnpakSipaksi.Modules.LaporanKemajuan.Application.Dokumen.Hibah.DeleteDokumenHibah
{
    internal sealed class DeleteDokumenHibahCommandHandler(
    IDokumenHibahRepository repository,
    IUnitOfWorkDokumenHibah unitOfWork)
    : ICommandHandler<DeleteDokumenHibahCommand>
    {
        public async Task<Result> Handle(DeleteDokumenHibahCommand request, CancellationToken cancellationToken)
        {
            Domain.Dokumen.Dokumen? existing = await repository.GetAsync(Guid.Parse(request.uuid), cancellationToken);

            if (existing is null)
            {
                return Result.Failure(DokumenErrors.NotFound(Guid.Parse(request.uuid)));
            }

            await repository.DeleteAsync(existing!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.LaporanKemajuan.Application.Abstractions.Data;
using UnpakSipaksi.Modules.LaporanKemajuan.Domain.Dokumen;
using UnpakSipaksi.Modules.PenelitianHibah.PublicApi;

namespace UnpakSipaksi.Modules.LaporanKemajuan.Application.Dokumen.Hibah.UpdateDokumenHibah
{
    internal sealed class UpdateDokumenHibahCommandHandler(
    IDokumenHibahRepository repository,
    IPenelitianHibahApi penelitianHibahApi,
    IUnitOfWorkDokumenHibah unitOfWork)
    : ICommandHandler<UpdateDokumenHibahCommand>
    {
        public async Task<Result> Handle(UpdateDokumenHibahCommand request, CancellationToken cancellationToken)
        {
            Domain.Dokumen.Dokumen? existing = await repository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);
            PenelitianHibahResponse? hibah = await penelitianHibahApi.GetAsync(Guid.Parse(request.UuidPenenitianHibah));

            Result<Domain.Dokumen.Dokumen> result = Domain.Dokumen.Dokumen.Update(
                existing,
                int.Parse(hibah?.Id ?? "0"),
                request.File,
                request.Type
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

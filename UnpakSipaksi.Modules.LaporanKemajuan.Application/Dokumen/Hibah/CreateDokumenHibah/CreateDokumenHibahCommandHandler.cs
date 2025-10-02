using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.LaporanKemajuan.Application.Abstractions.Data;
using UnpakSipaksi.Modules.LaporanKemajuan.Domain.Dokumen;
using UnpakSipaksi.Modules.PenelitianHibah.PublicApi;

namespace UnpakSipaksi.Modules.LaporanKemajuan.Application.Dokumen.Hibah.CreateDokumenHibah
{
    internal sealed class CreateDokumenHibahCommandHandler(
    IDokumenHibahRepository repository,
    IPenelitianHibahApi penelitianHibahApi,
    IUnitOfWorkDokumenHibah unitOfWork)
    : ICommandHandler<CreateDokumenHibahCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateDokumenHibahCommand request, CancellationToken cancellationToken)
        {
            PenelitianHibahResponse? hibah = await penelitianHibahApi.GetAsync(Guid.Parse(request.UuidPenenitianHibah));

            Result<Domain.Dokumen.Dokumen> result = Domain.Dokumen.Dokumen.Create(
                int.Parse(hibah?.Id ?? "0"),
                request.File,
                request.Type
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            repository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}

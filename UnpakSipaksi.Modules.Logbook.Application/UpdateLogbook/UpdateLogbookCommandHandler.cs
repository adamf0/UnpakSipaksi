using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Logbook.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Logbook.Domain.Logbook;
using UnpakSipaksi.Modules.PenelitianHibah.PublicApi;
using UnpakSipaksi.Modules.PenelitianPkm.PublicApi;

namespace UnpakSipaksi.Modules.Logbook.Application.UpdateLogbook
{
    internal sealed class UpdateLogbookCommandHandler(
    ILogbookRepository LogbookRepository,
    IPenelitianHibahApi penelitianHibahApi,
    IPenelitianPkmApi penelitianPkmApi,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateLogbookCommand>
    {
        public async Task<Result> Handle(UpdateLogbookCommand request, CancellationToken cancellationToken)
        {
            Guid UuidHibah = string.IsNullOrEmpty(request.UuidPenelitianHibah)? Guid.Empty:Guid.Parse(request.UuidPenelitianHibah);
            Guid UuidPkm = string.IsNullOrEmpty(request.UuidPenelitianPkm) ? Guid.Empty : Guid.Parse(request.UuidPenelitianPkm);

            Domain.Logbook.Logbook? existing = await LogbookRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);
            PenelitianHibahResponse? hibah = await penelitianHibahApi.GetAsync(UuidHibah, cancellationToken);
            PenelitianPkmResponse? pkm = await penelitianPkmApi.GetAsync(UuidPkm, cancellationToken);

            Result<Domain.Logbook.Logbook> result = Domain.Logbook.Logbook.Update(
                existing,
                int.Parse(hibah?.Id ?? "0"),
                int.Parse(pkm?.Id ?? "0"),
                request.TanggalKegiatan,
                request.Lampiran,
                request.Deskripsi,
                request.Persentase,
                0
            );

            if (result.IsFailure)
            {
                return Result.Failure(result.Error);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

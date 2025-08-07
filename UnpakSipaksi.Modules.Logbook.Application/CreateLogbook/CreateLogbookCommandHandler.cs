using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Logbook.Domain.Logbook;
using UnpakSipaksi.Modules.Logbook.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianHibah.PublicApi;
using UnpakSipaksi.Modules.PenelitianPkm.PublicApi;

namespace UnpakSipaksi.Modules.Logbook.Application.CreateLogbook
{
    internal sealed class CreateLogbookCommandHandler(
    ILogbookRepository LogbookRepository,
    IPenelitianHibahApi penelitianHibahApi,
    IPenelitianPkmApi penelitianPkmApi,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateLogbookCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateLogbookCommand request, CancellationToken cancellationToken)
        {
            PenelitianHibahResponse? hibah = await penelitianHibahApi.GetAsync(Guid.Parse(request.UuidPenelitianHibah!), cancellationToken);
            PenelitianPkmResponse? pkm = await penelitianPkmApi.GetAsync(Guid.Parse(request.UuidPenelitianPkm!), cancellationToken);

            Result<Domain.Logbook.Logbook> result = Domain.Logbook.Logbook.Create(
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
                return Result.Failure<Guid>(result.Error);
            }

            LogbookRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}

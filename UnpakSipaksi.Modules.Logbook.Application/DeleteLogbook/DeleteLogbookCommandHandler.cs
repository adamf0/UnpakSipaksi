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

namespace UnpakSipaksi.Modules.Logbook.Application.DeleteLogbook
{
    internal sealed class DeleteLogbookCommandHandler(
    ILogbookRepository LogbookRepository,
    IPenelitianHibahApi penelitianHibahApi,
    IPenelitianPkmApi penelitianPkmApi,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteLogbookCommand>
    {
        public async Task<Result> Handle(DeleteLogbookCommand request, CancellationToken cancellationToken)
        {
            Guid UuidHibah = string.IsNullOrEmpty(request.UuidPenelitianHibah) ? Guid.Empty : Guid.Parse(request.UuidPenelitianHibah);
            Guid UuidPkm = string.IsNullOrEmpty(request.UuidPenelitianPkm) ? Guid.Empty : Guid.Parse(request.UuidPenelitianPkm);

            PenelitianHibahResponse? hibah = await penelitianHibahApi.GetAsync(UuidHibah, cancellationToken);
            PenelitianPkmResponse? pkm = await penelitianPkmApi.GetAsync(UuidPkm, cancellationToken);
            Domain.Logbook.Logbook? existingLogbook = await LogbookRepository.GetAsync(Guid.Parse(request.Uuid), int.Parse(hibah?.Id ?? "0"), int.Parse(pkm?.Id ?? "0"), cancellationToken);

            if (existingLogbook is null)
            {
                return Result.Failure(LogbookErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            await LogbookRepository.DeleteAsync(existingLogbook!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

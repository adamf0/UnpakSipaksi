using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.DokumenPendukung;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.DeleteDokumenPendukung
{
    internal sealed class DeleteDokumenPendukungCommandHandler(
    IDokumenPendukungRepository dokumenPendukungRepository,
    IPenelitianHibahRepository penelitianHibahRepository,
    IUnitOfWorkDokumenPendukung unitOfWork)
    : ICommandHandler<DeleteDokumenPendukungCommand>
    {
        //[PR] ini masih 2 query, seharusnya jadi 1 query atau mungkin ini benar sisanya salah. i dont know
        public async Task<Result> Handle(DeleteDokumenPendukungCommand request, CancellationToken cancellationToken)
        {
            Domain.DokumenPendukung.DokumenPendukung? existingDokumenPendukung = await dokumenPendukungRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);
            if (existingDokumenPendukung == null)
            {
                return Result.Failure<Guid>(DokumenPendukungErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Domain.PenelitianHibah.PenelitianHibah? existingPenelitianHibah = await penelitianHibahRepository.GetAsync(Guid.Parse(request.UuidPenelitianHibah), cancellationToken);
            if (existingPenelitianHibah == null)
            {
                return Result.Failure<Guid>(DokumenPendukungErrors.NotFoundHibah(Guid.Parse(request.UuidPenelitianHibah)));
            }

            if (existingDokumenPendukung?.PenelitianHibahId != existingPenelitianHibah?.Id) {
                return Result.Failure<Guid>(DokumenPendukungErrors.InvalidData());
            }

            await dokumenPendukungRepository.DeleteAsync(existingDokumenPendukung!);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

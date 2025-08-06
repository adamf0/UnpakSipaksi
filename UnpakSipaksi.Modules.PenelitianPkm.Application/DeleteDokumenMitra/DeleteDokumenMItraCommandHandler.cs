using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianPkm.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.DokumenMitra;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.DeleteDokumenMitra
{
    internal sealed class DeleteDokumenMItraCommandHandler(
    IDokumenMitraRepository dokumenMitraRepository,
    IPenelitianPkmRepository penelitianPkmRepository,
    IUnitOfWorkDokumenMitra unitOfWork)
    : ICommandHandler<DeleteDokumenMItraCommand>
    {
        public async Task<Result> Handle(DeleteDokumenMItraCommand request, CancellationToken cancellationToken)
        {
            Domain.DokumenMitra.DokumenMitra? existingDokumenMitra = await dokumenMitraRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);
            if (existingDokumenMitra == null)
            {
                return Result.Failure<Guid>(DokumenMitraErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Domain.PenelitianPkm.PenelitianPkm? existingPenelitianPkm = await penelitianPkmRepository.GetAsync(Guid.Parse(request.UuidPenelitianPkm), cancellationToken);
            if (existingPenelitianPkm == null)
            {
                return Result.Failure<Guid>(DokumenMitraErrors.NotFoundPkm(Guid.Parse(request.UuidPenelitianPkm)));
            }

            await dokumenMitraRepository.DeleteAsync(existingDokumenMitra!);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

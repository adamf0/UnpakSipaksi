using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.DokumenLainnya;
using UnpakSipaksi.Modules.PenelitianPkm.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.DokumenLainnya;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.DeleteDokumenLainnya
{
    internal sealed class DeleteDokumenLainnyaCommandHandler(
    IDokumenLainnyaRepository dokumenLainnyaRepository,
    IPenelitianPkmRepository penelitianPkmRepository,
    IUnitOfWorkDokumenLainnya unitOfWork)
    : ICommandHandler<DeleteDokumenLainnyaCommand>
    {
        public async Task<Result> Handle(DeleteDokumenLainnyaCommand request, CancellationToken cancellationToken)
        {
            Domain.DokumenLainnya.DokumenLainnya? existingDokumenLainnya = await dokumenLainnyaRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);
            if (existingDokumenLainnya == null)
            {
                return Result.Failure<Guid>(DokumenLainnyaErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Domain.PenelitianPkm.PenelitianPkm? existingPenelitianPkm = await penelitianPkmRepository.GetAsync(Guid.Parse(request.UuidPenelitianPkm), cancellationToken);
            if (existingPenelitianPkm == null)
            {
                return Result.Failure<Guid>(DokumenLainnyaErrors.NotFoundPkm(Guid.Parse(request.UuidPenelitianPkm)));
            }

            await dokumenLainnyaRepository.DeleteAsync(existingDokumenLainnya!);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

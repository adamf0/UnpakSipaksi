using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.DokumenKontrak;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.DeleteDokumenKontrak
{
    internal sealed class DeleteDokumenKontrakCommandHandler(
    IDokumenKontrakRepository dokumenKontrakRepository,
    IPenelitianHibahRepository penelitianHibahRepository,
    IUnitOfWorkDokumenKontrak unitOfWork)
    : ICommandHandler<DeleteDokumenKontrakCommand>
    {
        public async Task<Result> Handle(DeleteDokumenKontrakCommand request, CancellationToken cancellationToken)
        {
            Domain.DokumenKontrak.DokumenKontrak? existingDokumenKontrak = await dokumenKontrakRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);
            if (existingDokumenKontrak == null)
            {
                return Result.Failure<Guid>(DokumenKontrakErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Domain.PenelitianHibah.PenelitianHibah? existingPenelitianHibah = await penelitianHibahRepository.GetAsync(Guid.Parse(request.UuidPenelitianHibah), cancellationToken);
            if (existingPenelitianHibah == null)
            {
                return Result.Failure<Guid>(DokumenKontrakErrors.NotFoundHibah(Guid.Parse(request.UuidPenelitianHibah)));
            }

            if (existingDokumenKontrak?.PenelitianHibahId != existingPenelitianHibah?.Id) {
                return Result.Failure<Guid>(DokumenKontrakErrors.InvalidData());
            }

            await dokumenKontrakRepository.DeleteAsync(existingDokumenKontrak!);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

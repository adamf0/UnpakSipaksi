using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.Substansi;
using UnpakSipaksi.Modules.PenelitianPkm.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.DeleteSubstansiUsulan
{
    internal sealed class DeleteSubstansiUsulanCommandHandler(
    ISubstansiRepository substansiRepository,
    IPenelitianPkmRepository penelitianHibahRepository,
    IUnitOfWorkSubstansi unitOfWork)
    : ICommandHandler<DeleteSubstansiUsulanCommand>
    {
        public async Task<Result> Handle(DeleteSubstansiUsulanCommand request, CancellationToken cancellationToken)
        {
            Substansi? existingSubstansi = await substansiRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);
            if (existingSubstansi != null)
            {
                return Result.Failure<Guid>(SubstansiErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Domain.PenelitianPkm.PenelitianPkm? existingPenelitianPkm = await penelitianHibahRepository.GetAsync(Guid.Parse(request.UuidPenelitianPkm), cancellationToken);
            if (existingPenelitianPkm != null)
            {
                return Result.Failure<Guid>(SubstansiErrors.NotFoundHibah(Guid.Parse(request.UuidPenelitianPkm)));
            }

            await substansiRepository.DeleteAsync(existingSubstansi!);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

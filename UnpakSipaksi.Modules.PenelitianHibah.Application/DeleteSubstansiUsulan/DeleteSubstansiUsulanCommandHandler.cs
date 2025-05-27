using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.Substansi;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.DeleteSubstansiUsulan
{
    internal sealed class DeleteSubstansiUsulanCommandHandler(
    ISubstansiRepository substansiRepository,
    IPenelitianHibahRepository penelitianHibahRepository,
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

            Domain.PenelitianHibah.PenelitianHibah? existingPenelitianHibah = await penelitianHibahRepository.GetAsync(Guid.Parse(request.UuidPenelitianHibah), cancellationToken);
            if (existingPenelitianHibah != null)
            {
                return Result.Failure<Guid>(SubstansiErrors.NotFoundHibah(Guid.Parse(request.UuidPenelitianHibah)));
            }

            await substansiRepository.DeleteAsync(existingSubstansi!);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

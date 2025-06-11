using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.RAB;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm;
using UnpakSipaksi.Modules.PenelitianPkm.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.DeleteRAB
{
    internal sealed class DeleteRABCommandHandler(
    IRABRepository rabRepository,
    IPenelitianPkmRepository penelitianHibahRepository,
    IUnitOfWorkRAB unitOfWork)
    : ICommandHandler<DeleteRABCommand>
    {
        public async Task<Result> Handle(DeleteRABCommand request, CancellationToken cancellationToken)
        {
            Domain.RAB.RAB? existingRAB = await rabRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);
            if (existingRAB == null)
            {
                return Result.Failure<Guid>(RABErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Domain.PenelitianPkm.PenelitianPkm? existingPenelitianPkm = await penelitianHibahRepository.GetAsync(Guid.Parse(request.UuidPenelitianPkm), cancellationToken);
            if (existingPenelitianPkm == null)
            {
                return Result.Failure<Guid>(RABErrors.NotFoundHibah(Guid.Parse(request.UuidPenelitianPkm)));
            }

            if (existingRAB?.PenelitianPkmId == existingPenelitianPkm?.Id)
            {
                return Result.Failure<Guid>(RABErrors.InvalidData());
            }

            await rabRepository.DeleteAsync(existingRAB!);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

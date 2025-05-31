using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.RAB;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.DeleteRAB
{
    internal sealed class DeleteRABCommandHandler(
    IRABRepository rabRepository,
    IPenelitianHibahRepository penelitianHibahRepository,
    IUnitOfWorkRAB unitOfWork)
    : ICommandHandler<DeleteRABCommand>
    {
        //[PR] ini masih 2 query, seharusnya jadi 1 query atau mungkin ini benar sisanya salah. i dont know
        public async Task<Result> Handle(DeleteRABCommand request, CancellationToken cancellationToken)
        {
            Domain.RAB.RAB? existingRAB = await rabRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);
            if (existingRAB == null)
            {
                return Result.Failure<Guid>(RABErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Domain.PenelitianHibah.PenelitianHibah? existingPenelitianHibah = await penelitianHibahRepository.GetAsync(Guid.Parse(request.UuidPenelitianHibah), cancellationToken);
            if (existingPenelitianHibah == null)
            {
                return Result.Failure<Guid>(RABErrors.NotFoundHibah(Guid.Parse(request.UuidPenelitianHibah)));
            }

            if (existingRAB?.PenelitianHibahId == existingPenelitianHibah?.Id) {
                return Result.Failure<Guid>(RABErrors.InvalidData());
            }

            await rabRepository.DeleteAsync(existingRAB!);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

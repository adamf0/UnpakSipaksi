using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.DeletePenelitianHibah
{
    internal sealed class DeletePenelitianHibahCommandHandler(
    IPenelitianHibahRepository penelitianHibahRepository,
    IUnitOfWorkHibah unitOfWork)
    : ICommandHandler<DeletePenelitianHibahCommand>
    {
        public async Task<Result> Handle(DeletePenelitianHibahCommand request, CancellationToken cancellationToken)
        {
            Domain.PenelitianHibah.PenelitianHibah? existingPenelitianHibah = await penelitianHibahRepository.GetAsync(Guid.Parse(request.Uuid), request.Nidn, cancellationToken);

            if (existingPenelitianHibah is null)
            {
                return Result.Failure(PenelitianHibahErrors.NotFound(Guid.Parse(request.Uuid)));
            }
            if (existingPenelitianHibah.NIDN != request.Nidn) {
                return Result.Failure(PenelitianHibahErrors.InvalidData());
            }

            await penelitianHibahRepository.DeleteAsync(existingPenelitianHibah!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

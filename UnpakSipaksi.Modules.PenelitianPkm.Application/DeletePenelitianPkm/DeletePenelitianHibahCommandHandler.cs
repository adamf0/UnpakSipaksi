using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm;
using UnpakSipaksi.Modules.PenelitianPkm.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.DeletePenelitianPkm
{
    internal sealed class DeletePenelitianPkmCommandHandler(
    IPenelitianPkmRepository penelitianHibahRepository,
    IUnitOfWorkHibah unitOfWork)
    : ICommandHandler<DeletePenelitianPkmCommand>
    {
        public async Task<Result> Handle(DeletePenelitianPkmCommand request, CancellationToken cancellationToken)
        {
            Domain.PenelitianPkm.PenelitianPkm? existingPenelitianPkm = await penelitianHibahRepository.GetAsync(Guid.Parse(request.Uuid), request.Nidn, cancellationToken);

            if (existingPenelitianPkm is null)
            {
                return Result.Failure(PenelitianPkmErrors.NotFound(Guid.Parse(request.Uuid)));
            }
            if (existingPenelitianPkm.NIDN != request.Nidn)
            {
                return Result.Failure(PenelitianPkmErrors.InvalidData());
            }

            await penelitianHibahRepository.DeleteAsync(existingPenelitianPkm!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

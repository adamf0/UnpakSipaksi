using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Insentif.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Insentif.Domain.Insentif;

namespace UnpakSipaksi.Modules.Insentif.Application.DeleteInsentif
{
    internal sealed class DeleteInsentifCommandHandler(
    IInsentifRepository InsentifPenelitianRepository,
    IUnitOfWorkInsentif unitOfWork)
    : ICommandHandler<DeleteInsentifCommand>
    {
        public async Task<Result> Handle(DeleteInsentifCommand request, CancellationToken cancellationToken)
        {
            Domain.Insentif.Insentif? existingInsentif = await InsentifPenelitianRepository.GetAsync(request.uuid, cancellationToken);

            if (existingInsentif is null)
            {
                return Result.Failure(InsentifErrors.NotFound(request.uuid));
            }

            await InsentifPenelitianRepository.DeleteAsync(existingInsentif!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

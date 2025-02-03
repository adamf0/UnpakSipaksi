using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.FokusPenelitian.Domain.FokusPenelitian;
using UnpakSipaksi.Modules.FokusPenelitian.Application.Abstractions.Data;
using UnpakSipaksi.Modules.FokusPenelitian.Application.DeleteFokusPenelitian;

namespace UnpakSipaksi.Modules.FokusPenelitian.Application.DeleteFokusPenelitian
{
    internal sealed class DeleteFokusPenelitianCommandHandler(
    IFokusPenelitianRepository fokusPenelitianRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteFokusPenelitianCommand>
    {
        public async Task<Result> Handle(DeleteFokusPenelitianCommand request, CancellationToken cancellationToken)
        {
            Domain.FokusPenelitian.FokusPenelitian? existingFokusPenelitian = await fokusPenelitianRepository.GetAsync(request.uuid, cancellationToken);

            if (existingFokusPenelitian is null)
            {
                return Result.Failure(FokusPenelitianErrors.NotFound(request.uuid));
            }

            await fokusPenelitianRepository.DeleteAsync(existingFokusPenelitian!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

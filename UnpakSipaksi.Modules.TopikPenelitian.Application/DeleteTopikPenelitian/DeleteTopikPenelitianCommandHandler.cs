using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.TopikPenelitian.Application.Abstractions.Data;
using UnpakSipaksi.Modules.TopikPenelitian.Domain.TopikPenelitian;

namespace UnpakSipaksi.Modules.TopikPenelitian.Application.DeleteTopikPenelitian
{
    internal sealed class DeleteTopikPenelitianCommandHandler(
    ITopikPenelitianRepository topikPenelitianRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteTopikPenelitianCommand>
    {
        public async Task<Result> Handle(DeleteTopikPenelitianCommand request, CancellationToken cancellationToken)
        {
            Domain.TopikPenelitian.TopikPenelitian? existingTopikPenelitian = await topikPenelitianRepository.GetAsync(request.uuid, cancellationToken);

            if (existingTopikPenelitian is null)
            {
                return Result.Failure(TopikPenelitianErrors.NotFound(request.uuid));
            }

            await topikPenelitianRepository.DeleteAsync(existingTopikPenelitian!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

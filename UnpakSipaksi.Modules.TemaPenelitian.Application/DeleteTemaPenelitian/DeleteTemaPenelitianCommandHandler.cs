using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.TemaPenelitian.Domain.TemaPenelitian;
using UnpakSipaksi.Modules.TemaPenelitian.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.TemaPenelitian.Application.DeleteTemaPenelitian
{
    internal sealed class DeleteTemaPenelitianCommandHandler(
    ITemaPenelitianRepository temaPenelitianRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteTemaPenelitianCommand>
    {
        public async Task<Result> Handle(DeleteTemaPenelitianCommand request, CancellationToken cancellationToken)
        {
            Domain.TemaPenelitian.TemaPenelitian? existingTemaPenelitian = await temaPenelitianRepository.GetAsync(Guid.Parse(request.uuid), cancellationToken);

            if (existingTemaPenelitian is null)
            {
                return Result.Failure(TemaPenelitianErrors.NotFound(Guid.Parse(request.uuid)));
            }

            await temaPenelitianRepository.DeleteAsync(existingTemaPenelitian!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

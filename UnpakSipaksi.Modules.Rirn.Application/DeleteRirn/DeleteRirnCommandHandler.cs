using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Rirn.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Rirn.Domain.Rirn;

namespace UnpakSipaksi.Modules.Rirn.Application.DeleteRirn
{
    internal sealed class DeleteRirnCommandHandler(
    IRirnRepository RirnRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteRirnCommand>
    {
        public async Task<Result> Handle(DeleteRirnCommand request, CancellationToken cancellationToken)
        {
            Domain.Rirn.Rirn? existingRirn = await RirnRepository.GetAsync(Guid.Parse(request.uuid), cancellationToken);

            if (existingRirn is null)
            {
                return Result.Failure(RirnErrors.NotFound(Guid.Parse(request.uuid)));
            }

            await RirnRepository.DeleteAsync(existingRirn!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

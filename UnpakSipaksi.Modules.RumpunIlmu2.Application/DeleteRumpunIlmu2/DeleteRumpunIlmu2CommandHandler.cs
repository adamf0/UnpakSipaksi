using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RumpunIlmu2.Domain.RumpunIlmu2;
using UnpakSipaksi.Modules.RumpunIlmu2.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.RumpunIlmu2.Application.DeleteRumpunIlmu2
{
    internal sealed class DeleteRumpunIlmu2CommandHandler(
    IRumpunIlmu2Repository RumpunIlmu2Repository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteRumpunIlmu2Command>
    {
        public async Task<Result> Handle(DeleteRumpunIlmu2Command request, CancellationToken cancellationToken)
        {
            Domain.RumpunIlmu2.RumpunIlmu2? existingRumpunIlmu2 = await RumpunIlmu2Repository.GetAsync(Guid.Parse(request.uuid), cancellationToken);

            if (existingRumpunIlmu2 is null)
            {
                return Result.Failure(RumpunIlmu2Errors.NotFound(Guid.Parse(request.uuid)));
            }

            await RumpunIlmu2Repository.DeleteAsync(existingRumpunIlmu2!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

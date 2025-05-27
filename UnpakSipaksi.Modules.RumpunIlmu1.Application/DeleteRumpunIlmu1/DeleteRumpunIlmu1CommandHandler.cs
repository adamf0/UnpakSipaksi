using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RumpunIlmu1.Application.Abstractions.Data;
using UnpakSipaksi.Modules.RumpunIlmu1.Domain.RumpunIlmu1;

namespace UnpakSipaksi.Modules.RumpunIlmu1.Application.DeleteRumpunIlmu1
{
    internal sealed class DeleteRumpunIlmu1CommandHandler(
    IRumpunIlmu1Repository RumpunIlmu1Repository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteRumpunIlmu1Command>
    {
        public async Task<Result> Handle(DeleteRumpunIlmu1Command request, CancellationToken cancellationToken)
        {
            Domain.RumpunIlmu1.RumpunIlmu1? existingRumpunIlmu1 = await RumpunIlmu1Repository.GetAsync(Guid.Parse(request.uuid), cancellationToken);

            if (existingRumpunIlmu1 is null)
            {
                return Result.Failure(RumpunIlmu1Errors.NotFound(Guid.Parse(request.uuid)));
            }

            await RumpunIlmu1Repository.DeleteAsync(existingRumpunIlmu1!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

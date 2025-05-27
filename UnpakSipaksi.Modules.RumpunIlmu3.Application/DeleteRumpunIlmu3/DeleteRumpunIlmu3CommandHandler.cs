using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RumpunIlmu3.Domain.RumpunIlmu3;
using UnpakSipaksi.Modules.RumpunIlmu3.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.RumpunIlmu3.Application.DeleteRumpunIlmu3
{
    internal sealed class DeleteRumpunIlmu3CommandHandler(
    IRumpunIlmu3Repository RumpunIlmu3Repository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteRumpunIlmu3Command>
    {
        public async Task<Result> Handle(DeleteRumpunIlmu3Command request, CancellationToken cancellationToken)
        {
            Domain.RumpunIlmu3.RumpunIlmu3? existingRumpunIlmu3 = await RumpunIlmu3Repository.GetAsync(Guid.Parse(request.uuid), cancellationToken);

            if (existingRumpunIlmu3 is null)
            {
                return Result.Failure(RumpunIlmu3Errors.NotFound(Guid.Parse(request.uuid)));
            }

            await RumpunIlmu3Repository.DeleteAsync(existingRumpunIlmu3!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

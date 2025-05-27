using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RumpunIlmu2.Domain.RumpunIlmu2;
using UnpakSipaksi.Modules.RumpunIlmu2.Application.Abstractions.Data;
using UnpakSipaksi.Modules.RumpunIlmu1.PublicApi;

namespace UnpakSipaksi.Modules.RumpunIlmu2.Application.CreateRumpunIlmu2
{
    internal sealed class CreateRumpunIlmu2CommandHandler(
    IRumpunIlmu2Repository RumpunIlmu2Repository,
    IRumpunIlmu1Api RumpunIlmu1Api,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateRumpunIlmu2Command, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateRumpunIlmu2Command request, CancellationToken cancellationToken)
        {
            RumpunIlmu1Response? RumpunIlmu1 = await RumpunIlmu1Api.GetAsync(Guid.Parse(request.UuidRumpunIlmu1), cancellationToken);

            if (RumpunIlmu1 is null)
            {
                return Result.Failure<Guid>(RumpunIlmu2Errors.RumpunIlmu1NotFound(Guid.Parse(request.UuidRumpunIlmu1)));
            }

            Result<Domain.RumpunIlmu2.RumpunIlmu2> result = Domain.RumpunIlmu2.RumpunIlmu2.Create(
                request.Nama,
                int.Parse(RumpunIlmu1.Id)
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            RumpunIlmu2Repository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}

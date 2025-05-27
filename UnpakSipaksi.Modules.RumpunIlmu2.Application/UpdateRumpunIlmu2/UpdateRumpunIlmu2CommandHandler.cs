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

namespace UnpakSipaksi.Modules.RumpunIlmu2.Application.UpdateRumpunIlmu2
{
    internal sealed class UpdateRumpunIlmu2CommandHandler(
    IRumpunIlmu2Repository RumpunIlmu2Repository,
    IRumpunIlmu1Api RumpunIlmu1Api,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateRumpunIlmu2Command>
    {
        public async Task<Result> Handle(UpdateRumpunIlmu2Command request, CancellationToken cancellationToken)
        {
            RumpunIlmu1Response? RumpunIlmu1 = await RumpunIlmu1Api.GetAsync(Guid.Parse(request.UuidRumpunIlmu1), cancellationToken);

            if (RumpunIlmu1 is null)
            {
                return Result.Failure<Guid>(RumpunIlmu2Errors.RumpunIlmu1NotFound(Guid.Parse(request.UuidRumpunIlmu1)));
            }

            Domain.RumpunIlmu2.RumpunIlmu2? existingRumpunIlmu2 = await RumpunIlmu2Repository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingRumpunIlmu2 is null)
            {
                Result.Failure(RumpunIlmu2Errors.NotFound(Guid.Parse(request.Uuid)));
            }

            Result<Domain.RumpunIlmu2.RumpunIlmu2> asset = Domain.RumpunIlmu2.RumpunIlmu2.Update(existingRumpunIlmu2!)
                         .ChangeNama(request.Nama)
                         .ChangeIdRumpunIlmu1(int.Parse(RumpunIlmu1.Id))
                         .Build();

            if (asset.IsFailure)
            {
                return Result.Failure<Guid>(asset.Error);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

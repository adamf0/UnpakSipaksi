using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RumpunIlmu3.Domain.RumpunIlmu3;
using UnpakSipaksi.Modules.RumpunIlmu2.PublicApi;
using UnpakSipaksi.Modules.RumpunIlmu3.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.RumpunIlmu3.Application.UpdateRumpunIlmu3
{
    internal sealed class UpdateRumpunIlmu3CommandHandler(
    IRumpunIlmu3Repository RumpunIlmu3Repository,
    IRumpunIlmu2Api RumpunIlmu2Api,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateRumpunIlmu3Command>
    {
        public async Task<Result> Handle(UpdateRumpunIlmu3Command request, CancellationToken cancellationToken)
        {
            RumpunIlmu2Response? RumpunIlmu2 = await RumpunIlmu2Api.GetAsync(request.UuidRumpunIlmu2, cancellationToken);

            if (RumpunIlmu2 is null)
            {
                return Result.Failure<Guid>(RumpunIlmu3Errors.RumpunIlmu2NotFound(request.UuidRumpunIlmu2));
            }

            Domain.RumpunIlmu3.RumpunIlmu3? existingRumpunIlmu3 = await RumpunIlmu3Repository.GetAsync(request.Uuid, cancellationToken);

            if (existingRumpunIlmu3 is null)
            {
                Result.Failure(RumpunIlmu3Errors.NotFound(request.Uuid));
            }

            Result<Domain.RumpunIlmu3.RumpunIlmu3> asset = Domain.RumpunIlmu3.RumpunIlmu3.Update(existingRumpunIlmu3!)
                         .ChangeNama(request.Nama)
                         .ChangeIdRumpunIlmu2(int.Parse(RumpunIlmu2.Id))
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

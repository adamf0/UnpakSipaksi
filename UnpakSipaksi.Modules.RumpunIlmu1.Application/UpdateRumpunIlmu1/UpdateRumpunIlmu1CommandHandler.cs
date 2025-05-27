using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RumpunIlmu1.Application.Abstractions.Data;
using UnpakSipaksi.Modules.RumpunIlmu1.Domain.RumpunIlmu1;

namespace UnpakSipaksi.Modules.RumpunIlmu1.Application.UpdateRumpunIlmu1
{
    internal sealed class UpdateRumpunIlmu1CommandHandler(
    IRumpunIlmu1Repository RumpunIlmu1Repository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateRumpunIlmu1Command>
    {
        public async Task<Result> Handle(UpdateRumpunIlmu1Command request, CancellationToken cancellationToken)
        {
            Domain.RumpunIlmu1.RumpunIlmu1? existingRumpunIlmu1 = await RumpunIlmu1Repository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingRumpunIlmu1 is null)
            {
                Result.Failure(RumpunIlmu1Errors.NotFound(Guid.Parse(request.Uuid)));
            }

            Result<Domain.RumpunIlmu1.RumpunIlmu1> asset = Domain.RumpunIlmu1.RumpunIlmu1.Update(existingRumpunIlmu1!)
                         .ChangeNama(request.Nama)
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

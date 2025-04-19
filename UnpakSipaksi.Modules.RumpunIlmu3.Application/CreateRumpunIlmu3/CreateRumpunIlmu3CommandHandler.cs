using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RumpunIlmu3.Domain.RumpunIlmu3;
using UnpakSipaksi.Modules.RumpunIlmu3.Application.Abstractions.Data;
using UnpakSipaksi.Modules.RumpunIlmu2.PublicApi;

namespace UnpakSipaksi.Modules.RumpunIlmu3.Application.CreateRumpunIlmu3
{
    internal sealed class CreateRumpunIlmu3CommandHandler(
    IRumpunIlmu3Repository RumpunIlmu3Repository,
    IRumpunIlmu2Api RumpunIlmu2Api,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateRumpunIlmu3Command, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateRumpunIlmu3Command request, CancellationToken cancellationToken)
        {
            RumpunIlmu2Response? RumpunIlmu2 = await RumpunIlmu2Api.GetAsync(request.UuidRumpunIlmu2, cancellationToken);

            if (RumpunIlmu2 is null)
            {
                return Result.Failure<Guid>(RumpunIlmu3Errors.RumpunIlmu2NotFound(request.UuidRumpunIlmu2));
            }

            Result<Domain.RumpunIlmu3.RumpunIlmu3> result = Domain.RumpunIlmu3.RumpunIlmu3.Create(
                request.Nama,
                int.Parse(RumpunIlmu2.Id)
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            RumpunIlmu3Repository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}

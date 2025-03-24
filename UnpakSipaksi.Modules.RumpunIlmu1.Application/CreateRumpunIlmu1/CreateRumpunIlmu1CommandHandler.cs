using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RumpunIlmu1.Application.Abstractions.Data;
using UnpakSipaksi.Modules.RumpunIlmu1.Domain.RumpunIlmu1;

namespace UnpakSipaksi.Modules.RumpunIlmu1.Application.CreateRumpunIlmu1
{
    internal sealed class CreateRumpunIlmu1CommandHandler(
    IRumpunIlmu1Repository RumpunIlmu1Repository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateRumpunIlmu1Command, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateRumpunIlmu1Command request, CancellationToken cancellationToken)
        {
            Result<Domain.RumpunIlmu1.RumpunIlmu1> result = Domain.RumpunIlmu1.RumpunIlmu1.Create(
                request.Nama
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            RumpunIlmu1Repository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}

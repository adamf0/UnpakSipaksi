using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Rirn.Domain.Rirn;
using UnpakSipaksi.Modules.Rirn.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.Rirn.Application.CreateRirn
{
    internal sealed class CreateRirnCommandHandler(
    IRirnRepository RirnRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateRirnCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateRirnCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.Rirn.Rirn> result = Domain.Rirn.Rirn.Create(
                request.Nama
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            RirnRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}

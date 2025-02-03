using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.FokusPenelitian.Domain.FokusPenelitian;
using UnpakSipaksi.Modules.FokusPenelitian.Application.Abstractions.Data;
using UnpakSipaksi.Modules.FokusPenelitian.Application.CreateFokusPenelitian;

namespace UnpakSipaksi.Modules.FokusPenelitian.Application.CreateFokusPenelitian
{
    internal sealed class CreateFokusPenelitianCommandHandler(
    IFokusPenelitianRepository fokusPenelitianRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateFokusPenelitianCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateFokusPenelitianCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.FokusPenelitian.FokusPenelitian> result = Domain.FokusPenelitian.FokusPenelitian.Create(
                request.Nama
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            fokusPenelitianRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}

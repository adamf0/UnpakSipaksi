using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.FokusPengabdian.Domain.FokusPengabdian;
using UnpakSipaksi.Modules.FokusPengabdian.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.FokusPengabdian.Application.CreateFokusPengabdian
{
    internal sealed class CreateFokusPengabdianCommandHandler(
    IFokusPengabdianRepository fokusPengabdianRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateFokusPengabdianCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateFokusPengabdianCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.FokusPengabdian.FokusPengabdian> result = Domain.FokusPengabdian.FokusPengabdian.Create(
                request.Nama
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            fokusPengabdianRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}

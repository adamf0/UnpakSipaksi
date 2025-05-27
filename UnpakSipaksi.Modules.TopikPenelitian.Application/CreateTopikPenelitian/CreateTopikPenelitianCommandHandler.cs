using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.TopikPenelitian.Application.Abstractions.Data;
using UnpakSipaksi.Modules.TopikPenelitian.Domain.TopikPenelitian;
using UnpakSipaksi.Modules.TemaPenelitian.PublicApi;

namespace UnpakSipaksi.Modules.TopikPenelitian.Application.CreateTopikPenelitian
{
    internal sealed class CreateTopikPenelitianCommandHandler(
    ITopikPenelitianRepository topikPenelitianRepository,
    ITemaPenelitianApi temaPenelitianApi,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateTopikPenelitianCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateTopikPenelitianCommand request, CancellationToken cancellationToken)
        {
            TemaPenelitianResponse? temaPenelitian = await temaPenelitianApi.GetAsync(Guid.Parse(request.TemaPenelitianId), cancellationToken);

            if (temaPenelitian is null)
            {
                return Result.Failure<Guid>(TopikPenelitianErrors.TemaPenelitianNotFound(Guid.Parse(request.TemaPenelitianId)));
            }

            Result<Domain.TopikPenelitian.TopikPenelitian> result = Domain.TopikPenelitian.TopikPenelitian.Create(
                request.Nama,
                int.Parse(temaPenelitian.Id)
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            topikPenelitianRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}

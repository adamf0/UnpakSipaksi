using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.TopikPenelitian.Application.Abstractions.Data;
using UnpakSipaksi.Modules.TemaPenelitian.PublicApi;
using UnpakSipaksi.Modules.TopikPenelitian.Domain.TopikPenelitian;

namespace UnpakSipaksi.Modules.TopikPenelitian.Application.UpdateTopikPenelitian
{
    internal sealed class UpdateTopikPenelitianCommandHandler(
    ITopikPenelitianRepository topikPenelitianRepository,
    ITemaPenelitianApi temaPenelitianApi,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateTopikPenelitianCommand>
    {
        public async Task<Result> Handle(UpdateTopikPenelitianCommand request, CancellationToken cancellationToken)
        {
            TemaPenelitianResponse? temaPenelitian = await temaPenelitianApi.GetAsync(Guid.Parse(request.TemaPenelitianId), cancellationToken);

            if (temaPenelitian is null)
            {
                return Result.Failure<Guid>(TopikPenelitianErrors.TemaPenelitianNotFound(Guid.Parse(request.TemaPenelitianId)));
            }

            Domain.TopikPenelitian.TopikPenelitian? existingTopikPenelitian = await topikPenelitianRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingTopikPenelitian is null)
            {
                return Result.Failure(TopikPenelitianErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Result<Domain.TopikPenelitian.TopikPenelitian> asset = Domain.TopikPenelitian.TopikPenelitian.Update(existingTopikPenelitian!)
                         .ChangeNama(request.Nama)
                         .ChangeTemaPenelitianId(int.Parse(temaPenelitian.Id))
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

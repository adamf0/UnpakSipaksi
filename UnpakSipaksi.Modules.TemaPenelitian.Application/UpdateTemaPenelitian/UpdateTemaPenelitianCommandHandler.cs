using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.TemaPenelitian.Domain.TemaPenelitian;
using UnpakSipaksi.Modules.TemaPenelitian.Application.Abstractions.Data;
using UnpakSipaksi.Modules.FokusPenelitian.PublicApi;

namespace UnpakSipaksi.Modules.TemaPenelitian.Application.UpdateTemaPenelitian
{
    internal sealed class UpdateTemaPenelitianCommandHandler(
    ITemaPenelitianRepository temaPenelitianRepository,
    IFokusPenelitianApi fokusPenelitianApi,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateTemaPenelitianCommand>
    {
        public async Task<Result> Handle(UpdateTemaPenelitianCommand request, CancellationToken cancellationToken)
        {
            FokusPenelitianResponse? fokusPenelitian = await fokusPenelitianApi.GetAsync(Guid.Parse(request.FokusPenelitianId), cancellationToken);

            if (fokusPenelitian is null)
            {
                return Result.Failure<Guid>(TemaPenelitianErrors.FokusPenelitianNotFound(Guid.Parse(request.FokusPenelitianId)));
            }

            Domain.TemaPenelitian.TemaPenelitian? existingTemaPenelitian = await temaPenelitianRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingTemaPenelitian is null)
            {
                Result.Failure(TemaPenelitianErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Result<Domain.TemaPenelitian.TemaPenelitian> asset = Domain.TemaPenelitian.TemaPenelitian.Update(existingTemaPenelitian!)
                         .ChangeNama(request.Nama)
                         .ChangeTemaPenelitianId(int.Parse(fokusPenelitian.Id))
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

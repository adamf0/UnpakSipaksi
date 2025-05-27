using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.FokusPenelitian.Domain.FokusPenelitian;
using UnpakSipaksi.Modules.FokusPenelitian.Application.Abstractions.Data;
using UnpakSipaksi.Modules.FokusPenelitian.Application.UpdateFokusPenelitian;

namespace UnpakSipaksi.Modules.FokusPenelitian.Application.UpdateFokusPenelitian
{
    internal sealed class UpdateFokusPenelitianCommandHandler(
    IFokusPenelitianRepository fokusPenelitianRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateFokusPenelitianCommand>
    {
        public async Task<Result> Handle(UpdateFokusPenelitianCommand request, CancellationToken cancellationToken)
        {
            Domain.FokusPenelitian.FokusPenelitian? existingFokusPenelitian = await fokusPenelitianRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingFokusPenelitian is null)
            {
                Result.Failure(FokusPenelitianErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Result<Domain.FokusPenelitian.FokusPenelitian> asset = Domain.FokusPenelitian.FokusPenelitian.Update(existingFokusPenelitian!)
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

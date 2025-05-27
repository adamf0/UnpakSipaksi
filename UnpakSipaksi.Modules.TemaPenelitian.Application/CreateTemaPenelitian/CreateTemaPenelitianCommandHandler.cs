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

namespace UnpakSipaksi.Modules.TemaPenelitian.Application.CreateTemaPenelitian
{
    internal sealed class CreateTemaPenelitianCommandHandler(
    ITemaPenelitianRepository temaPenelitianRepository,
    IFokusPenelitianApi fokusPenelitianApi,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateTemaPenelitianCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateTemaPenelitianCommand request, CancellationToken cancellationToken)
        {
            FokusPenelitianResponse? fokusPenelitian = await fokusPenelitianApi.GetAsync(Guid.Parse(request.FokusPenelitianId), cancellationToken);

            if (fokusPenelitian is null)
            {
                return Result.Failure<Guid>(TemaPenelitianErrors.FokusPenelitianNotFound(Guid.Parse(request.FokusPenelitianId)));
            }

            Result<Domain.TemaPenelitian.TemaPenelitian> result = Domain.TemaPenelitian.TemaPenelitian.Create(
                request.Nama,
                int.Parse(fokusPenelitian.Id)
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            temaPenelitianRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}

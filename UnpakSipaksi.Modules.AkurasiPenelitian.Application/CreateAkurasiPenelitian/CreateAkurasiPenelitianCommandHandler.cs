using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.AkurasiPenelitian.Application.Abstractions.Data;
using UnpakSipaksi.Modules.AkurasiPenelitian.Domain.AkurasiPenelitian;

namespace UnpakSipaksi.Modules.AkurasiPenelitian.Application.CreateAkurasiPenelitian
{
    internal sealed class CreateAkurasiPenelitianCommandHandler(
    IAkurasiPenelitianRepository akurasiPenelitianRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateAkurasiPenelitianCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateAkurasiPenelitianCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.AkurasiPenelitian.AkurasiPenelitian> result = Domain.AkurasiPenelitian.AkurasiPenelitian.Create(
                request.Nama,
                request.Skor
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            akurasiPenelitianRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}

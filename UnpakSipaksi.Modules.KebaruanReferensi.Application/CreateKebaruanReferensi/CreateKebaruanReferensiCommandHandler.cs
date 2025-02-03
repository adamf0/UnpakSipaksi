using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KebaruanReferensi.Domain.KebaruanReferensi;
using UnpakSipaksi.Modules.KebaruanReferensi.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.KebaruanReferensi.Application.CreateKebaruanReferensi
{
    internal sealed class CreateKebaruanReferensiCommandHandler(
    IKebaruanReferensiRepository kebaruanReferensiRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateKebaruanReferensiCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateKebaruanReferensiCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.KebaruanReferensi.KebaruanReferensi> result = Domain.KebaruanReferensi.KebaruanReferensi.Create(
                request.Nama,
                request.Skor
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            kebaruanReferensiRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}

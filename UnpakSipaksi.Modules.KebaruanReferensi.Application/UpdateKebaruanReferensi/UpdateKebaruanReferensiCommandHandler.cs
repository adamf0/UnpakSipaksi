using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KebaruanReferensi.Domain.KebaruanReferensi;
using UnpakSipaksi.Modules.KebaruanReferensi.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.KebaruanReferensi.Application.UpdateKebaruanReferensi
{
    internal sealed class UpdateKebaruanReferensiCommandHandler(
    IKebaruanReferensiRepository kebaruanReferensiRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateKebaruanReferensiCommand>
    {
        public async Task<Result> Handle(UpdateKebaruanReferensiCommand request, CancellationToken cancellationToken)
        {
            Domain.KebaruanReferensi.KebaruanReferensi? existingKebaruanReferensi = await kebaruanReferensiRepository.GetAsync(request.Uuid, cancellationToken);

            if (existingKebaruanReferensi is null)
            {
                Result.Failure(KebaruanReferensiErrors.NotFound(request.Uuid));
            }

            Result<Domain.KebaruanReferensi.KebaruanReferensi> asset = Domain.KebaruanReferensi.KebaruanReferensi.Update(existingKebaruanReferensi!)
                         .ChangeNama(request.Nama)
                         .ChangeSkor(request.Skor)
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

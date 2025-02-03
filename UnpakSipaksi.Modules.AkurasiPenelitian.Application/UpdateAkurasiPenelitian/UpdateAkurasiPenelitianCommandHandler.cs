using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.AkurasiPenelitian.Application.Abstractions.Data;
using UnpakSipaksi.Modules.AkurasiPenelitian.Domain.AkurasiPenelitian;

namespace UnpakSipaksi.Modules.AkurasiPenelitian.Application.UpdateAkurasiPenelitian
{
    internal sealed class UpdateAkurasiPenelitianCommandHandler(
    IAkurasiPenelitianRepository akurasiPenelitianRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateAkurasiPenelitianCommand>
    {
        public async Task<Result> Handle(UpdateAkurasiPenelitianCommand request, CancellationToken cancellationToken)
        {
            Domain.AkurasiPenelitian.AkurasiPenelitian? existingAkurasiPenelitian = await akurasiPenelitianRepository.GetAsync(request.Uuid, cancellationToken);

            if (existingAkurasiPenelitian is null)
            {
                Result.Failure(AkurasiPenelitianErrors.NotFound(request.Uuid));
            }

            Result<Domain.AkurasiPenelitian.AkurasiPenelitian> asset = Domain.AkurasiPenelitian.AkurasiPenelitian.Update(existingAkurasiPenelitian!)
                         .ChangeNama(request.Nama)
                         .ChangeBobotPDP(request.BobotPDP)
                         .ChangeBobotTerapan(request.BobotTerapan)
                         .ChangeBobotPenelitianDasar(request.BobotPenelitianDasar)
                         .ChangeBobotKerjasama(request.BobotKerjasama)
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

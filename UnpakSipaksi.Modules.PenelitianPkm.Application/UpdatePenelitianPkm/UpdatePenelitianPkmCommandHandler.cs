using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm;
using UnpakSipaksi.Modules.PenelitianPkm.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.UpdatePenelitianPkm
{
    internal sealed class UpdatePenelitianPkmCommandHandler(
    IPenelitianPkmRepository penelitianHibahRepository,
    IUnitOfWorkHibah unitOfWork)
    : ICommandHandler<UpdatePenelitianPkmCommand>
    {
        public async Task<Result> Handle(UpdatePenelitianPkmCommand request, CancellationToken cancellationToken)
        {
            Domain.PenelitianPkm.PenelitianPkm? existingPenelitianPkm = await penelitianHibahRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            Result<Domain.PenelitianPkm.PenelitianPkm> asset = await Domain.PenelitianPkm.PenelitianPkm.Update(
                existingPenelitianPkm!,
                penelitianHibahRepository,
                request.TahunPengajuan,
                request.Judul
            );

            if (asset.IsFailure)
            {
                return Result.Failure<Guid>(asset.Error);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

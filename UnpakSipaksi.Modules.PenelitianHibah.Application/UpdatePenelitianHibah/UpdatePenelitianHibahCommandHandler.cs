using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PenelitianHibah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.UpdatePenelitianHibah
{
    internal sealed class UpdatePenelitianHibahCommandHandler(
    IPenelitianHibahRepository penelitianHibahRepository,
    IUnitOfWorkHibah unitOfWork)
    : ICommandHandler<UpdatePenelitianHibahCommand>
    {
        public async Task<Result> Handle(UpdatePenelitianHibahCommand request, CancellationToken cancellationToken)
        {
            Domain.PenelitianHibah.PenelitianHibah? existingPenelitianHibah = await penelitianHibahRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            Result<Domain.PenelitianHibah.PenelitianHibah> asset = await Domain.PenelitianHibah.PenelitianHibah.Update(
                existingPenelitianHibah!,
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

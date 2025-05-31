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
            //[PR] check valid nidn

            //harus pindah ke domain
            Domain.PenelitianHibah.PenelitianHibah? existingPenelitianHibah = await penelitianHibahRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingPenelitianHibah is null)
            {
                return Result.Failure(PenelitianHibahErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            bool isUnique = await penelitianHibahRepository.HasUniqueDataAsync(
                Guid.Parse(request.Uuid),
                request.NIDN,
                request.Judul
            );
            if (!isUnique)
            {
                return Result.Failure<Guid>(PenelitianHibahErrors.NotUnique(request.NIDN, request.Judul));
            }
            //harus pindah ke domain

            Result<Domain.PenelitianHibah.PenelitianHibah> asset = Domain.PenelitianHibah.PenelitianHibah.Update(
                existingPenelitianHibah!,
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

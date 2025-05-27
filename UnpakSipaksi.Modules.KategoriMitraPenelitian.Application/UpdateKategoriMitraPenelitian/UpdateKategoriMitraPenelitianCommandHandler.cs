using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KategoriMitraPenelitian.Domain.KategoriMitraPenelitian;
using UnpakSipaksi.Modules.KategoriMitraPenelitian.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.KategoriMitraPenelitian.Application.UpdateKategoriMitraPenelitian
{
    internal sealed class UpdateKategoriMitraPenelitianCommandHandler(
    IKategoriMitraPenelitianRepository kategoriMitraPenelitianRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateKategoriMitraPenelitianCommand>
    {
        public async Task<Result> Handle(UpdateKategoriMitraPenelitianCommand request, CancellationToken cancellationToken)
        {
            Domain.KategoriMitraPenelitian.KategoriMitraPenelitian? existingKategoriMitraPenelitian = await kategoriMitraPenelitianRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingKategoriMitraPenelitian is null)
            {
                Result.Failure(KategoriMitraPenelitianErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Result<Domain.KategoriMitraPenelitian.KategoriMitraPenelitian> asset = Domain.KategoriMitraPenelitian.KategoriMitraPenelitian.Update(existingKategoriMitraPenelitian!)
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

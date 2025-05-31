using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Domain.KesesuaianSolusiMasalahMitra;
using UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Application.UpdateKesesuaianSolusiMasalahMitra
{
    internal sealed class UpdateKesesuaianSolusiMasalahMitraCommandHandler(
    IKesesuaianSolusiMasalahMitraRepository kesesuaianSolusiMasalahMitraRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateKesesuaianSolusiMasalahMitraCommand>
    {
        public async Task<Result> Handle(UpdateKesesuaianSolusiMasalahMitraCommand request, CancellationToken cancellationToken)
        {
            Domain.KesesuaianSolusiMasalahMitra.KesesuaianSolusiMasalahMitra? existingKesesuaianSolusiMasalahMitra = await kesesuaianSolusiMasalahMitraRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingKesesuaianSolusiMasalahMitra is null)
            {
                return Result.Failure(KesesuaianSolusiMasalahMitraErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Result<Domain.KesesuaianSolusiMasalahMitra.KesesuaianSolusiMasalahMitra> asset = Domain.KesesuaianSolusiMasalahMitra.KesesuaianSolusiMasalahMitra.Update(existingKesesuaianSolusiMasalahMitra!)
                         .ChangeNama(request.Nama)
                         .ChangeNilai(request.Nilai)
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

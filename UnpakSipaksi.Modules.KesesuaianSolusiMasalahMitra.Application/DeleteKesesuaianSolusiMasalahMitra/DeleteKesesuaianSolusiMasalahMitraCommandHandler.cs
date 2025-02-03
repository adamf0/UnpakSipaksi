using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Domain.KesesuaianSolusiMasalahMitra;
using UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Application.DeleteKesesuaianSolusiMasalahMitra
{
    internal sealed class DeleteKesesuaianSolusiMasalahMitraCommandHandler(
    IKesesuaianSolusiMasalahMitraRepository kesesuaianSolusiMasalahMitraRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteKesesuaianSolusiMasalahMitraCommand>
    {
        public async Task<Result> Handle(DeleteKesesuaianSolusiMasalahMitraCommand request, CancellationToken cancellationToken)
        {
            Domain.KesesuaianSolusiMasalahMitra.KesesuaianSolusiMasalahMitra? existingKesesuaianSolusiMasalahMitra = await kesesuaianSolusiMasalahMitraRepository.GetAsync(request.uuid, cancellationToken);

            if (existingKesesuaianSolusiMasalahMitra is null)
            {
                return Result.Failure(KesesuaianSolusiMasalahMitraErrors.NotFound(request.uuid));
            }

            await kesesuaianSolusiMasalahMitraRepository.DeleteAsync(existingKesesuaianSolusiMasalahMitra!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

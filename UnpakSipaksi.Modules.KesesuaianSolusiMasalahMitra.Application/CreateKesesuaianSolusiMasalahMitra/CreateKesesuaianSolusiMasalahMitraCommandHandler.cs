using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Domain.KesesuaianSolusiMasalahMitra;
using UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.KesesuaianSolusiMasalahMitra.Application.CreateKesesuaianSolusiMasalahMitra
{
    internal sealed class CreateKesesuaianSolusiMasalahMitraCommandHandler(
    IKesesuaianSolusiMasalahMitraRepository kesesuaianSolusiMasalahMitraRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateKesesuaianSolusiMasalahMitraCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateKesesuaianSolusiMasalahMitraCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.KesesuaianSolusiMasalahMitra.KesesuaianSolusiMasalahMitra> result = Domain.KesesuaianSolusiMasalahMitra.KesesuaianSolusiMasalahMitra.Create(
                request.Nama,
                request.Nilai
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            kesesuaianSolusiMasalahMitraRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}

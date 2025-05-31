using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KesesuaianPenugasan.Domain.KesesuaianPenugasan;
using UnpakSipaksi.Modules.KesesuaianPenugasan.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.KesesuaianPenugasan.Application.UpdateKesesuaianPenugasan
{
    internal sealed class UpdateKesesuaianPenugasanCommandHandler(
    IKesesuaianPenugasanRepository kesesuaianPenugasanRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateKesesuaianPenugasanCommand>
    {
        public async Task<Result> Handle(UpdateKesesuaianPenugasanCommand request, CancellationToken cancellationToken)
        {
            Domain.KesesuaianPenugasan.KesesuaianPenugasan? existingKesesuaianPenugasan = await kesesuaianPenugasanRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingKesesuaianPenugasan is null)
            {
                return Result.Failure(KesesuaianPenugasanErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Result<Domain.KesesuaianPenugasan.KesesuaianPenugasan> asset = Domain.KesesuaianPenugasan.KesesuaianPenugasan.Update(existingKesesuaianPenugasan!)
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

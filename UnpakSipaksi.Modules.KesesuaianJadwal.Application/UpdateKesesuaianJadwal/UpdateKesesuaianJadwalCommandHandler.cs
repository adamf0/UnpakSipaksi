using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KesesuaianJadwal.Domain.KesesuaianJadwal;
using UnpakSipaksi.Modules.KesesuaianJadwal.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.KesesuaianJadwal.Application.UpdateKesesuaianJadwal
{
    internal sealed class UpdateKesesuaianJadwalCommandHandler(
    IKesesuaianJadwalRepository kesesuaianJadwalRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateKesesuaianJadwalCommand>
    {
        public async Task<Result> Handle(UpdateKesesuaianJadwalCommand request, CancellationToken cancellationToken)
        {
            Domain.KesesuaianJadwal.KesesuaianJadwal? existingKesesuaianJadwal = await kesesuaianJadwalRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingKesesuaianJadwal is null)
            {
                Result.Failure(KesesuaianJadwalErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Result<Domain.KesesuaianJadwal.KesesuaianJadwal> asset = Domain.KesesuaianJadwal.KesesuaianJadwal.Update(existingKesesuaianJadwal!)
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

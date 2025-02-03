using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KesesuaianJadwal.Domain.KesesuaianJadwal;
using UnpakSipaksi.Modules.KesesuaianJadwal.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.KesesuaianJadwal.Application.DeleteKesesuaianJadwal
{
    internal sealed class DeleteKesesuaianJadwalCommandHandler(
    IKesesuaianJadwalRepository kesesuaianJadwalRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteKesesuaianJadwalCommand>
    {
        public async Task<Result> Handle(DeleteKesesuaianJadwalCommand request, CancellationToken cancellationToken)
        {
            Domain.KesesuaianJadwal.KesesuaianJadwal? existingKesesuaianJadwal = await kesesuaianJadwalRepository.GetAsync(request.uuid, cancellationToken);

            if (existingKesesuaianJadwal is null)
            {
                return Result.Failure(KesesuaianJadwalErrors.NotFound(request.uuid));
            }

            await kesesuaianJadwalRepository.DeleteAsync(existingKesesuaianJadwal!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

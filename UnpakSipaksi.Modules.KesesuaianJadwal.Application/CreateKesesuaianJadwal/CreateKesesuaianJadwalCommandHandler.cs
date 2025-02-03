using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KesesuaianJadwal.Domain.KesesuaianJadwal;
using UnpakSipaksi.Modules.KesesuaianJadwal.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.KesesuaianJadwal.Application.CreateKesesuaianJadwal
{
    internal sealed class CreateKesesuaianJadwalCommandHandler(
    IKesesuaianJadwalRepository kesesuaianJadwalRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateKesesuaianJadwalCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateKesesuaianJadwalCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.KesesuaianJadwal.KesesuaianJadwal> result = Domain.KesesuaianJadwal.KesesuaianJadwal.Create(
                request.Nama,
                request.Nilai
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            kesesuaianJadwalRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}

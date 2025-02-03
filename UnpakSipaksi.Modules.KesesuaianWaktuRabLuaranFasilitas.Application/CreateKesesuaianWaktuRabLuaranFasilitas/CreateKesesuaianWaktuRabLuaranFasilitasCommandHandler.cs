using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Domain.KesesuaianWaktuRabLuaranFasilitas;
using UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Application.CreateKesesuaianWaktuRabLuaranFasilitas
{
    internal sealed class CreateKesesuaianWaktuRabLuaranFasilitasCommandHandler(
    IKesesuaianWaktuRabLuaranFasilitasRepository kesesuaianWaktuRabLuaranFasilitasRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateKesesuaianWaktuRabLuaranFasilitasCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateKesesuaianWaktuRabLuaranFasilitasCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.KesesuaianWaktuRabLuaranFasilitas.KesesuaianWaktuRabLuaranFasilitas> result = Domain.KesesuaianWaktuRabLuaranFasilitas.KesesuaianWaktuRabLuaranFasilitas.Create(
                request.Nama,
                request.Skor
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            kesesuaianWaktuRabLuaranFasilitasRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Domain.KesesuaianWaktuRabLuaranFasilitas;

namespace UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Application.UpdateKesesuaianWaktuRabLuaranFasilitas
{
    internal sealed class UpdateKesesuaianWaktuRabLuaranFasilitasCommandHandler(
    IKesesuaianWaktuRabLuaranFasilitasRepository kesesuaianWaktuRabLuaranFasilitasRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateKesesuaianWaktuRabLuaranFasilitasCommand>
    {
        public async Task<Result> Handle(UpdateKesesuaianWaktuRabLuaranFasilitasCommand request, CancellationToken cancellationToken)
        {
            Domain.KesesuaianWaktuRabLuaranFasilitas.KesesuaianWaktuRabLuaranFasilitas? existingKesesuaianWaktuRabLuaranFasilitas = await kesesuaianWaktuRabLuaranFasilitasRepository.GetAsync(request.Uuid, cancellationToken);

            if (existingKesesuaianWaktuRabLuaranFasilitas is null)
            {
                Result.Failure(KesesuaianWaktuRabLuaranFasilitasErrors.NotFound(request.Uuid));
            }

            Result<Domain.KesesuaianWaktuRabLuaranFasilitas.KesesuaianWaktuRabLuaranFasilitas> asset = Domain.KesesuaianWaktuRabLuaranFasilitas.KesesuaianWaktuRabLuaranFasilitas.Update(existingKesesuaianWaktuRabLuaranFasilitas!)
                         .ChangeNama(request.Nama)
                         .ChangeSkor(request.Skor)
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Domain.KesesuaianWaktuRabLuaranFasilitas;
using UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.KesesuaianWaktuRabLuaranFasilitas.Application.DeleteKesesuaianWaktuRabLuaranFasilitas
{
    internal sealed class DeleteKesesuaianWaktuRabLuaranFasilitasCommandHandler(
    IKesesuaianWaktuRabLuaranFasilitasRepository kesesuaianTktRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteKesesuaianWaktuRabLuaranFasilitasCommand>
    {
        public async Task<Result> Handle(DeleteKesesuaianWaktuRabLuaranFasilitasCommand request, CancellationToken cancellationToken)
        {
            Domain.KesesuaianWaktuRabLuaranFasilitas.KesesuaianWaktuRabLuaranFasilitas? existingKesesuaianWaktuRabLuaranFasilitas = await kesesuaianTktRepository.GetAsync(Guid.Parse(request.uuid), cancellationToken);

            if (existingKesesuaianWaktuRabLuaranFasilitas is null)
            {
                return Result.Failure(KesesuaianWaktuRabLuaranFasilitasErrors.NotFound(Guid.Parse(request.uuid)));
            }

            await kesesuaianTktRepository.DeleteAsync(existingKesesuaianWaktuRabLuaranFasilitas!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

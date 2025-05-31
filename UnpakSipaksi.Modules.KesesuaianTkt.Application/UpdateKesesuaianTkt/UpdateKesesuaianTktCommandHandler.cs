using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KesesuaianTkt.Domain.KesesuaianTkt;
using UnpakSipaksi.Modules.KesesuaianTkt.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.KesesuaianTkt.Application.UpdateKesesuaianTkt
{
    internal sealed class UpdateKesesuaianTktCommandHandler(
    IKesesuaianTktRepository kesesuaianTktRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateKesesuaianTktCommand>
    {
        public async Task<Result> Handle(UpdateKesesuaianTktCommand request, CancellationToken cancellationToken)
        {
            Domain.KesesuaianTkt.KesesuaianTkt? existingKesesuaianTkt = await kesesuaianTktRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingKesesuaianTkt is null)
            {
                return Result.Failure(KesesuaianTktErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Result<Domain.KesesuaianTkt.KesesuaianTkt> asset = Domain.KesesuaianTkt.KesesuaianTkt.Update(existingKesesuaianTkt!)
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

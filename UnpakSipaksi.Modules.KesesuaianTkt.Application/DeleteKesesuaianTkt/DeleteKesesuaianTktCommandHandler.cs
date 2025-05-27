using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KesesuaianTkt.Domain.KesesuaianTkt;
using UnpakSipaksi.Modules.KesesuaianTkt.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.KesesuaianTkt.Application.DeleteKesesuaianTkt
{
    internal sealed class DeleteKesesuaianTktCommandHandler(
    IKesesuaianTktRepository kesesuaianTktRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteKesesuaianTktCommand>
    {
        public async Task<Result> Handle(DeleteKesesuaianTktCommand request, CancellationToken cancellationToken)
        {
            Domain.KesesuaianTkt.KesesuaianTkt? existingKesesuaianTkt = await kesesuaianTktRepository.GetAsync(Guid.Parse(request.uuid), cancellationToken);

            if (existingKesesuaianTkt is null)
            {
                return Result.Failure(KesesuaianTktErrors.NotFound(Guid.Parse(request.uuid)));
            }

            await kesesuaianTktRepository.DeleteAsync(existingKesesuaianTkt!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

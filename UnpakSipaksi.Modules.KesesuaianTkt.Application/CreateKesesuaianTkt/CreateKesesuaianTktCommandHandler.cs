using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KesesuaianTkt.Domain.KesesuaianTkt;
using UnpakSipaksi.Modules.KesesuaianTkt.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.KesesuaianTkt.Application.CreateKesesuaianTkt
{
    internal sealed class CreateKesesuaianTktCommandHandler(
    IKesesuaianTktRepository kesesuaianTktRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateKesesuaianTktCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateKesesuaianTktCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.KesesuaianTkt.KesesuaianTkt> result = Domain.KesesuaianTkt.KesesuaianTkt.Create(
                request.Nama,
                request.Skor
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            kesesuaianTktRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}

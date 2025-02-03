using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KesesuaianPenugasan.Domain.KesesuaianPenugasan;
using UnpakSipaksi.Modules.KesesuaianPenugasan.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.KesesuaianPenugasan.Application.CreateKesesuaianPenugasan
{
    internal sealed class CreateKesesuaianPenugasanCommandHandler(
    IKesesuaianPenugasanRepository kesesuaianPenugasanRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateKesesuaianPenugasanCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateKesesuaianPenugasanCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.KesesuaianPenugasan.KesesuaianPenugasan> result = Domain.KesesuaianPenugasan.KesesuaianPenugasan.Create(
                request.Nama,
                request.Nilai
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            kesesuaianPenugasanRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}

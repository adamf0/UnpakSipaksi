using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KesesuaianPenugasan.Domain.KesesuaianPenugasan;
using UnpakSipaksi.Modules.KesesuaianPenugasan.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.KesesuaianPenugasan.Application.DeleteKesesuaianPenugasan
{
    internal sealed class DeleteKesesuaianPenugasanCommandHandler(
    IKesesuaianPenugasanRepository kesesuaianPenugasanRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteKesesuaianPenugasanCommand>
    {
        public async Task<Result> Handle(DeleteKesesuaianPenugasanCommand request, CancellationToken cancellationToken)
        {
            Domain.KesesuaianPenugasan.KesesuaianPenugasan? existingKesesuaianPenugasan = await kesesuaianPenugasanRepository.GetAsync(Guid.Parse(request.uuid), cancellationToken);

            if (existingKesesuaianPenugasan is null)
            {
                return Result.Failure(KesesuaianPenugasanErrors.NotFound(Guid.Parse(request.uuid)));
            }

            await kesesuaianPenugasanRepository.DeleteAsync(existingKesesuaianPenugasan!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

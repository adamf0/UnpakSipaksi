using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.IndikatorCapaian.Application.Abstractions.Data;
using UnpakSipaksi.Modules.IndikatorCapaian.Domain;

namespace UnpakSipaksi.Modules.IndikatorCapaian.Application.DeleteIndikatorCapaian
{
    internal sealed class DeleteIndikatorCapaianCommandHandler(
    IIndikatorCapaianRepository JenisLuaranRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteIndikatorCapaianCommand>
    {
        public async Task<Result> Handle(DeleteIndikatorCapaianCommand request, CancellationToken cancellationToken)
        {
            Domain.IndikatorCapaian? existingAkurasiPenelitian = await JenisLuaranRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingAkurasiPenelitian is null)
            {
                return Result.Failure(IndikatorCapaianErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            await JenisLuaranRepository.DeleteAsync(existingAkurasiPenelitian!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.JenisLuaran.Application.Abstractions.Data;
using UnpakSipaksi.Modules.JenisLuaran.Domain;

namespace UnpakSipaksi.Modules.JenisLuaran.Application.DeleteJenisLuaran
{
    internal sealed class DeleteJenisLuaranCommandHandler(
    IJenisLuaranRepository JenisLuaranRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteJenisLuaranCommand>
    {
        public async Task<Result> Handle(DeleteJenisLuaranCommand request, CancellationToken cancellationToken)
        {
            Domain.JenisLuaran? existingAkurasiPenelitian = await JenisLuaranRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingAkurasiPenelitian is null)
            {
                return Result.Failure(JenisLuaranErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            await JenisLuaranRepository.DeleteAsync(existingAkurasiPenelitian!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

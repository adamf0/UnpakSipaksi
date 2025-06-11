using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.JenisLuaran.Application.Abstractions.Data;
using UnpakSipaksi.Modules.JenisLuaran.Domain;

namespace UnpakSipaksi.Modules.JenisLuaran.Application.UpdateJenisLuaran
{
    internal sealed class UpdateJenisLuaranCommandHandler(
    IJenisLuaranRepository akurasiPenelitianRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateJenisLuaranCommand>
    {
        public async Task<Result> Handle(UpdateJenisLuaranCommand request, CancellationToken cancellationToken)
        {
            Domain.JenisLuaran? existingAkurasiPenelitian = await akurasiPenelitianRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingAkurasiPenelitian is null)
            {
                return Result.Failure(JenisLuaranErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Result<Domain.JenisLuaran> asset = Domain.JenisLuaran.Update(
                Guid.Parse(request.Uuid), 
                request.Nama,
                existingAkurasiPenelitian!
            );

            if (asset.IsFailure)
            {
                return Result.Failure<Guid>(asset.Error);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

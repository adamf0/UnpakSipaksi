using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.IndikatorCapaian.Application.Abstractions.Data;
using UnpakSipaksi.Modules.IndikatorCapaian.Domain;
using UnpakSipaksi.Modules.JenisLuaran.PublicApi;

namespace UnpakSipaksi.Modules.IndikatorCapaian.Application.UpdateIndikatorCapaian
{
    internal sealed class UpdateIndikatorCapaianCommandHandler(
    IJenisLuaranApi JenisLuaranApi,
    IIndikatorCapaianRepository akurasiPenelitianRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateIndikatorCapaianCommand>
    {
        public async Task<Result> Handle(UpdateIndikatorCapaianCommand request, CancellationToken cancellationToken)
        {
            JenisLuaranResponse? JenisLuaran = await JenisLuaranApi.GetAsync(Guid.Parse(request.UuidJenisLuaran));
            Domain.IndikatorCapaian? existingAkurasiPenelitian = await akurasiPenelitianRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            Result<Domain.IndikatorCapaian> asset = Domain.IndikatorCapaian.Update(
                Guid.Parse(request.Uuid),
                int.Parse(JenisLuaran?.Id ?? "0"),
                request.Nama,
                request.Status,
                existingAkurasiPenelitian
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

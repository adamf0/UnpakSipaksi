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

namespace UnpakSipaksi.Modules.IndikatorCapaian.Application.CreateIndikatorCapaian
{
    internal sealed class CreateIndikatorCapaianCommandHandler(
    IJenisLuaranApi JenisLuaranApi,
    IIndikatorCapaianRepository JenisLuaranRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateIndikatorCapaianCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateIndikatorCapaianCommand request, CancellationToken cancellationToken)
        {
            JenisLuaranResponse? JenisLuaran = await JenisLuaranApi.GetAsync(Guid.Parse(request.UuidJenisLuaran));

            Result<Domain.IndikatorCapaian> result = Domain.IndikatorCapaian.Create(
                int.Parse(JenisLuaran?.Id??"0"),
                request.Nama,
                request.Status
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            JenisLuaranRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}

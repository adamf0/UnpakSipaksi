using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.IndikatorCapaian.PublicApi;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.Luaran;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm;
using UnpakSipaksi.Modules.PenelitianPkm.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianPkm.Application.GetLuaran;
using UnpakSipaksi.Modules.JenisLuaran.PublicApi;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.UpdateLuaran
{
    internal sealed class UpdateLuaranCommandHandler(
        ILuaranRepository luaranRepository,
        IPenelitianPkmRepository penelitianHibahRepository,
        IJenisLuaranApi jenisLuaranApi,
        IIndikatorCapaianApi indikatorCapaianApi,
        IUnitOfWorkLuaran unitOfWork)
        : ICommandHandler<UpdateLuaranCommand>
    {
        public async Task<Result> Handle(UpdateLuaranCommand request, CancellationToken cancellationToken)
        {
            Domain.Luaran.Luaran? existingLuaran = await luaranRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);
            if (existingLuaran is null)
            {
                return Result.Failure(LuaranErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Domain.PenelitianPkm.PenelitianPkm? existingPenelitianPkm = await penelitianHibahRepository.GetAsync(Guid.Parse(request.UuidPenelitianPkm), cancellationToken);
            JenisLuaran.PublicApi.JenisLuaranResponse? existingJenisLuaran = await jenisLuaranApi.GetAsync(string.IsNullOrEmpty(request.UuidJenisLuaran) ? Guid.Empty : Guid.Parse(request.UuidJenisLuaran), cancellationToken);
            IndikatorCapaian.PublicApi.IndikatorCapaianResponse? existingIndikatorCapaian = await indikatorCapaianApi.GetAsync(string.IsNullOrEmpty(request.UuidIndikatorCapaian) ? Guid.Empty : Guid.Parse(request.UuidIndikatorCapaian), cancellationToken);

            var createResult = Domain.Luaran.Luaran.Update(
                Guid.Parse(request.Uuid),
                Guid.Parse(request.UuidPenelitianPkm),
                existingPenelitianPkm,
                existingLuaran,
                string.IsNullOrEmpty(existingJenisLuaran?.Id) ? null : int.Parse(existingJenisLuaran.Id),
                string.IsNullOrEmpty(existingIndikatorCapaian?.Id) ? null : int.Parse(existingIndikatorCapaian.Id),
                request.Keterangan,
                request.Link,
                request.Jenis
            );

            if (createResult.IsFailure)
                return Result.Failure<Guid>(createResult.Error);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

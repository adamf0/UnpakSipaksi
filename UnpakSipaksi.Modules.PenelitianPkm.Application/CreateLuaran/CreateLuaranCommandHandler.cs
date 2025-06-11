using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.IndikatorCapaian.PublicApi;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.Luaran;
using UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm;
using UnpakSipaksi.Modules.PenelitianPkm.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianPkm.Application.GetLuaran;
using UnpakSipaksi.Modules.JenisLuaran.PublicApi;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.CreateLuaran
{
    internal sealed class CreateLuaranCommandHandler(
        ILuaranRepository luaranRepository,
        IPenelitianPkmRepository penelitianHibahRepository,
        IJenisLuaranApi jenisLuaranApi,
        IIndikatorCapaianApi indikatorCapaianApi,
        IUnitOfWorkLuaran unitOfWork)
        : ICommandHandler<CreateLuaranCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateLuaranCommand request, CancellationToken cancellationToken)
        {
            Domain.PenelitianPkm.PenelitianPkm? existingPenelitianPkm = await penelitianHibahRepository.GetAsync(Guid.Parse(request.UuidPenelitianPkm), cancellationToken);
            JenisLuaran.PublicApi.JenisLuaranResponse? existingJenisLuaran = await jenisLuaranApi.GetAsync(string.IsNullOrEmpty(request.UuidJenisLuaran) ? Guid.Empty : Guid.Parse(request.UuidJenisLuaran), cancellationToken);
            IndikatorCapaian.PublicApi.IndikatorCapaianResponse? existingIndikatorCapaian = await indikatorCapaianApi.GetAsync(string.IsNullOrEmpty(request.UuidIndikatorCapaian) ? Guid.Empty : Guid.Parse(request.UuidIndikatorCapaian), cancellationToken);

            var result = Domain.Luaran.Luaran.Create(
                Guid.Parse(request.UuidPenelitianPkm),
                existingPenelitianPkm,
                string.IsNullOrEmpty(existingJenisLuaran?.Id) ? null : int.Parse(existingJenisLuaran?.Id ?? "0"),
                string.IsNullOrEmpty(existingIndikatorCapaian?.Id) ? null : int.Parse(existingIndikatorCapaian?.Id ?? "0"),
                request.Keterangan,
                request.Link,
                request.Jenis
            );

            if (result.IsFailure)
                return Result.Failure<Guid>(result.Error);

            luaranRepository.Insert(result.Value);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(result.Value.Uuid);
        }
    }
}

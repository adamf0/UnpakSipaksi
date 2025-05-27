using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Kategori.PublicApi;
using UnpakSipaksi.Modules.KategoriLuaran.PublicApi;
using UnpakSipaksi.Modules.PenelitianHibah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.Luaran;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.UpdateLuaran
{
    internal sealed class UpdateLuaranCommandHandler(
        ILuaranRepository luaranRepository,
        IPenelitianHibahRepository penelitianHibahRepository,
        IKategoriApi kategoriApi,
        IKategoriLuaranApi kategoriLuaranApi,
        IUnitOfWorkLuaran unitOfWork)
        : ICommandHandler<UpdateLuaranCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(UpdateLuaranCommand request, CancellationToken cancellationToken)
        {
            Domain.Luaran.Luaran? existingLuaran = await luaranRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);
            if (existingLuaran is null)
            {
                Result.Failure(LuaranErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Domain.PenelitianHibah.PenelitianHibah? existingPenelitianHibah = await penelitianHibahRepository.GetAsync(Guid.Parse(request.UuidPenelitianHibah), cancellationToken);
            KategoriResponse? existingKategori = await kategoriApi.GetAsync(string.IsNullOrEmpty(request.UuidKategori) ? Guid.Empty : Guid.Parse(request.UuidKategori), cancellationToken);
            KategoriLuaranResponse? existingKategoriLuaran = await kategoriLuaranApi.GetAsync(string.IsNullOrEmpty(request.UuidKategoriLuaran) ? Guid.Empty : Guid.Parse(request.UuidKategoriLuaran), cancellationToken);

            var createResult = Domain.Luaran.Luaran.Update(
                Guid.Parse(request.Uuid),
                Guid.Parse(request.UuidPenelitianHibah),
                existingPenelitianHibah,
                existingLuaran,
                string.IsNullOrEmpty(existingKategori?.Id) ? null : int.Parse(existingKategori.Id),
                string.IsNullOrEmpty(existingKategoriLuaran?.Id) ? null : int.Parse(existingKategoriLuaran.Id),
                request.Keterangan,
                request.Link,
                request.Jenis
            );

            if (createResult.IsFailure)
                return Result.Failure<Guid>(createResult.Error);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(createResult.Value.Uuid);
        }
    }
}

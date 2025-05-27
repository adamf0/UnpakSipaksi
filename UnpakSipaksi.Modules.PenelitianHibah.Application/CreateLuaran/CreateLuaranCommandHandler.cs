using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Kategori.PublicApi;
using UnpakSipaksi.Modules.KategoriLuaran.PublicApi;
using UnpakSipaksi.Modules.PenelitianHibah.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.Luaran;
using UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.CreateLuaran
{
    internal sealed class CreateLuaranCommandHandler(
        ILuaranRepository luaranRepository,
        IPenelitianHibahRepository penelitianHibahRepository,
        IKategoriApi kategoriApi,
        IKategoriLuaranApi kategoriLuaranApi,
        IUnitOfWorkLuaran unitOfWork)
        : ICommandHandler<CreateLuaranCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateLuaranCommand request, CancellationToken cancellationToken)
        {
            Domain.PenelitianHibah.PenelitianHibah? existingPenelitianHibah = await penelitianHibahRepository.GetAsync(Guid.Parse(request.UuidPenelitianHibah), cancellationToken);
            KategoriResponse? existingKategori = await kategoriApi.GetAsync(string.IsNullOrEmpty(request.UuidKategori)? Guid.Empty : Guid.Parse(request.UuidKategori), cancellationToken);
            KategoriLuaranResponse? existingKategoriLuaran = await kategoriLuaranApi.GetAsync(string.IsNullOrEmpty(request.UuidKategoriLuaran) ? Guid.Empty : Guid.Parse(request.UuidKategoriLuaran), cancellationToken);

            var result = Domain.Luaran.Luaran.Create(
                Guid.Parse(request.UuidPenelitianHibah),
                existingPenelitianHibah,
                string.IsNullOrEmpty(existingKategori?.Id)? null:int.Parse(existingKategori.Id),
                string.IsNullOrEmpty(existingKategoriLuaran?.Id) ? null : int.Parse(existingKategoriLuaran.Id),
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Kategori.PublicApi;
using UnpakSipaksi.Modules.KategoriLuaran.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KategoriLuaran.Domain.Kategori;

namespace UnpakSipaksi.Modules.KategoriLuaran.Application.UpdateKategoriLuaran
{
    internal sealed class UpdateKategoriLuaranCommandHandler(
    IKategoriApi kategoriApi,
    IKategoriLuaranRepository kategoriLuaranRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateKategoriLuaranCommand>
    {
        public async Task<Result> Handle(UpdateKategoriLuaranCommand request, CancellationToken cancellationToken)
        {
            Domain.KategoriLuaran.KategoriLuaran? existingKategoriLuaran = await kategoriLuaranRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingKategoriLuaran is null)
            {
                return Result.Failure(KategoriLuaranErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            KategoriResponse? kategori = await kategoriApi.GetAsync(Guid.Parse(request.UuidKategori));
            if (kategori == null)
            {
                return Result.Failure<Guid>(KategoriLuaranErrors.KategoriNotFound());
            }

            //[PR] belum penambahan validasi di bisnis model
            Result<Domain.KategoriLuaran.KategoriLuaran> asset = Domain.KategoriLuaran.KategoriLuaran.Update(existingKategoriLuaran!)
                         .ChangeKategoriId(int.Parse(kategori.Id ?? "0"))
                         .ChangeNama(request.Nama)
                         .ChangeStatus(request.Status)
                         .Build();

            if (asset.IsFailure)
            {
                return Result.Failure<Guid>(asset.Error);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

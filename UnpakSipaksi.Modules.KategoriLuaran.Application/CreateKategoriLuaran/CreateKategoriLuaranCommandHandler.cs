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

namespace UnpakSipaksi.Modules.KategoriLuaran.Application.CreateKategoriLuaran
{
    internal sealed class CreateKategoriLuaranCommandHandler(
    IKategoriApi kategoriApi,
    IKategoriLuaranRepository kategoriLuaranRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateKategoriLuaranCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateKategoriLuaranCommand request, CancellationToken cancellationToken)
        {
            KategoriResponse? kategori = await kategoriApi.GetAsync(Guid.Parse(request.UuidKategori));
            if (kategori == null) {
                return Result.Failure<Guid>(KategoriLuaranErrors.KategoriNotFound());
            }

            Result<Domain.KategoriLuaran.KategoriLuaran> result = Domain.KategoriLuaran.KategoriLuaran.Create(
                int.Parse(kategori?.Id ?? "0"),
                request.Nama,
                request.Status
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            kategoriLuaranRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}

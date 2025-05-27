using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KategoriLuaran.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KategoriLuaran.Domain.Kategori;

namespace UnpakSipaksi.Modules.KategoriLuaran.Application.DeleteKategoriLuaran
{
    internal sealed class DeleteKategoriLuaranCommandHandler(
    IKategoriLuaranRepository kategoriLuaranRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteKategoriLuaranCommand>
    {
        public async Task<Result> Handle(DeleteKategoriLuaranCommand request, CancellationToken cancellationToken)
        {
            Domain.KategoriLuaran.KategoriLuaran? existingKategoriLuaran = await kategoriLuaranRepository.GetAsync(Guid.Parse(request.uuid), cancellationToken);

            if (existingKategoriLuaran is null)
            {
                return Result.Failure(KategoriLuaranErrors.NotFound(Guid.Parse(request.uuid)));
            }

            await kategoriLuaranRepository.DeleteAsync(existingKategoriLuaran!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

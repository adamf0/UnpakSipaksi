using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Kategori.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Kategori.Domain.Kategori;

namespace UnpakSipaksi.Modules.Kategori.Application.DeleteKategori
{
    internal sealed class DeleteKategoriCommandHandler(
    IKategoriRepository kategoriRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteKategoriCommand>
    {
        public async Task<Result> Handle(DeleteKategoriCommand request, CancellationToken cancellationToken)
        {
            Domain.Kategori.Kategori? existingKategori = await kategoriRepository.GetAsync(Guid.Parse(request.uuid), cancellationToken);

            if (existingKategori is null)
            {
                return Result.Failure(KategoriErrors.NotFound(Guid.Parse(request.uuid)));
            }

            await kategoriRepository.DeleteAsync(existingKategori!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Kategori.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Kategori.Domain.Kategori;

namespace UnpakSipaksi.Modules.Kategori.Application.UpdateKategori
{
    internal sealed class UpdateKategoriCommandHandler(
    IKategoriRepository kategoriRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateKategoriCommand>
    {
        public async Task<Result> Handle(UpdateKategoriCommand request, CancellationToken cancellationToken)
        {
            Domain.Kategori.Kategori? existingKategori = await kategoriRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingKategori is null)
            {
                Result.Failure(KategoriErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Result<Domain.Kategori.Kategori> asset = Domain.Kategori.Kategori.Update(existingKategori!)
                         .ChangeNama(request.Nama)
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KategoriSumberDana.Domain.KategoriSumberDana;
using UnpakSipaksi.Modules.KategoriSumberDana.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.KategoriSumberDana.Application.UpdateKategoriSumberDana
{
    internal sealed class UpdateKategoriSumberDanaCommandHandler(
    IKategoriSumberDanaRepository kategoriSumberDanaRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateKategoriSumberDanaCommand>
    {
        public async Task<Result> Handle(UpdateKategoriSumberDanaCommand request, CancellationToken cancellationToken)
        {
            Domain.KategoriSumberDana.KategoriSumberDana? existingKategoriSumberDana = await kategoriSumberDanaRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingKategoriSumberDana is null)
            {
                Result.Failure(KategoriSumberDanaErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Result<Domain.KategoriSumberDana.KategoriSumberDana> asset = Domain.KategoriSumberDana.KategoriSumberDana.Update(existingKategoriSumberDana!)
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KategoriSumberDana.Domain.KategoriSumberDana;
using UnpakSipaksi.Modules.KategoriSumberDana.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.KategoriSumberDana.Application.DeleteKategoriSumberDana
{
    internal sealed class DeleteKategoriSumberDanaCommandHandler(
    IKategoriSumberDanaRepository kategoriSumberDanaRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteKategoriSumberDanaCommand>
    {
        public async Task<Result> Handle(DeleteKategoriSumberDanaCommand request, CancellationToken cancellationToken)
        {
            Domain.KategoriSumberDana.KategoriSumberDana? existingKategoriSumberDana = await kategoriSumberDanaRepository.GetAsync(request.uuid, cancellationToken);

            if (existingKategoriSumberDana is null)
            {
                return Result.Failure(KategoriSumberDanaErrors.NotFound(request.uuid));
            }

            await kategoriSumberDanaRepository.DeleteAsync(existingKategoriSumberDana!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

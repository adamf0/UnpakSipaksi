using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KategoriMitraPenelitian.Domain.KategoriMitraPenelitian;
using UnpakSipaksi.Modules.KategoriMitraPenelitian.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.KategoriMitraPenelitian.Application.DeleteKategoriMitraPenelitian
{
    internal sealed class DeleteKategoriMitraPenelitianCommandHandler(
    IKategoriMitraPenelitianRepository kategoriMitraPenelitianRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteKategoriMitraPenelitianCommand>
    {
        public async Task<Result> Handle(DeleteKategoriMitraPenelitianCommand request, CancellationToken cancellationToken)
        {
            Domain.KategoriMitraPenelitian.KategoriMitraPenelitian? existingKategoriMitraPenelitian = await kategoriMitraPenelitianRepository.GetAsync(request.uuid, cancellationToken);

            if (existingKategoriMitraPenelitian is null)
            {
                return Result.Failure(KategoriMitraPenelitianErrors.NotFound(request.uuid));
            }

            await kategoriMitraPenelitianRepository.DeleteAsync(existingKategoriMitraPenelitian!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

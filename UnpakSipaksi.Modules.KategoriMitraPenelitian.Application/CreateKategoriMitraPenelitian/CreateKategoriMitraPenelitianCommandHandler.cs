using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KategoriMitraPenelitian.Domain.KategoriMitraPenelitian;
using UnpakSipaksi.Modules.KategoriMitraPenelitian.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.KategoriMitraPenelitian.Application.CreateKategoriMitraPenelitian
{
    internal sealed class CreateKategoriMitraPenelitianCommandHandler(
    IKategoriMitraPenelitianRepository kategoriMitraPenelitianRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateKategoriMitraPenelitianCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateKategoriMitraPenelitianCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.KategoriMitraPenelitian.KategoriMitraPenelitian> result = Domain.KategoriMitraPenelitian.KategoriMitraPenelitian.Create(
                request.Nama
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            kategoriMitraPenelitianRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}

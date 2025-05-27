using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Kategori.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Kategori.Domain.Kategori;

namespace UnpakSipaksi.Modules.Kategori.Application.CreateKategori
{
    internal sealed class CreateKategoriCommandHandler(
    IKategoriRepository kategoriRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateKategoriCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateKategoriCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.Kategori.Kategori> result = Domain.Kategori.Kategori.Create(
                request.Nama
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            kategoriRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}

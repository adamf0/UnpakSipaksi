using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KategoriSumberDana.Domain.KategoriSumberDana;
using UnpakSipaksi.Modules.KategoriSumberDana.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.KategoriSumberDana.Application.CreateKategoriSumberDana
{
    internal sealed class CreateKategoriSumberDanaCommandHandler(
    IKategoriSumberDanaRepository kategoriSumberDanaRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateKategoriSumberDanaCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateKategoriSumberDanaCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.KategoriSumberDana.KategoriSumberDana> result = Domain.KategoriSumberDana.KategoriSumberDana.Create(
                request.Nama
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            kategoriSumberDanaRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}

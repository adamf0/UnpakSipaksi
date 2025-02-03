using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KategoriTkt.Domain.KategoriTkt;
using UnpakSipaksi.Modules.KategoriTkt.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.KategoriTkt.Application.CreateKategoriTkt
{
    internal sealed class CreateKategoriTktCommandHandler(
    IKategoriTktRepository kategoriTktRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateKategoriTktCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateKategoriTktCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.KategoriTkt.KategoriTkt> result = Domain.KategoriTkt.KategoriTkt.Create(
                request.Nama
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            kategoriTktRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Satuan.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Satuan.Domain.Satuan;

namespace UnpakSipaksi.Modules.Satuan.Application.CreateSatuan
{
    internal sealed class CreateSatuanCommandHandler(
    ISatuanRepository SatuanRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateSatuanCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateSatuanCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.Satuan.Satuan> result = Domain.Satuan.Satuan.Create(
                request.Nama
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            SatuanRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}

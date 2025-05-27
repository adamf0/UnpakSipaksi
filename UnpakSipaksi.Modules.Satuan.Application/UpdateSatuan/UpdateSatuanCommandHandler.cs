using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Satuan.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Satuan.Domain.Satuan;

namespace UnpakSipaksi.Modules.Satuan.Application.UpdateSatuan
{
    internal sealed class UpdateSatuanCommandHandler(
    ISatuanRepository SatuanRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateSatuanCommand>
    {
        public async Task<Result> Handle(UpdateSatuanCommand request, CancellationToken cancellationToken)
        {
            Domain.Satuan.Satuan? existingSatuan = await SatuanRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingSatuan is null)
            {
                Result.Failure(SatuanErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Result<Domain.Satuan.Satuan> asset = Domain.Satuan.Satuan.Update(existingSatuan!)
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Satuan.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Satuan.Domain.Satuan;

namespace UnpakSipaksi.Modules.Satuan.Application.DeleteSatuan
{
    internal sealed class DeleteSatuanCommandHandler(
    ISatuanRepository SatuanRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteSatuanCommand>
    {
        public async Task<Result> Handle(DeleteSatuanCommand request, CancellationToken cancellationToken)
        {
            Domain.Satuan.Satuan? existingSatuan = await SatuanRepository.GetAsync(Guid.Parse(request.uuid), cancellationToken);

            if (existingSatuan is null)
            {
                return Result.Failure(SatuanErrors.NotFound(Guid.Parse(request.uuid)));
            }

            await SatuanRepository.DeleteAsync(existingSatuan!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

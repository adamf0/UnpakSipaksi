using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Referensi.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Referensi.Domain.Referensi;

namespace UnpakSipaksi.Modules.Referensi.Application.DeleteReferensi
{
    internal sealed class DeleteReferensiCommandHandler(
    IReferensiRepository ReferensiRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteReferensiCommand>
    {
        public async Task<Result> Handle(DeleteReferensiCommand request, CancellationToken cancellationToken)
        {
            Domain.Referensi.Referensi? existingReferensi = await ReferensiRepository.GetAsync(Guid.Parse(request.uuid), cancellationToken);

            if (existingReferensi is null)
            {
                return Result.Failure(ReferensiErrors.NotFound(Guid.Parse(request.uuid)));
            }

            await ReferensiRepository.DeleteAsync(existingReferensi!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

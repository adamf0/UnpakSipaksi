using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KebaruanReferensi.Domain.KebaruanReferensi;
using UnpakSipaksi.Modules.KebaruanReferensi.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.KebaruanReferensi.Application.DeleteKebaruanReferensi
{
    internal sealed class DeleteKebaruanReferensiCommandHandler(
    IKebaruanReferensiRepository kebaruanReferensiRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteKebaruanReferensiCommand>
    {
        public async Task<Result> Handle(DeleteKebaruanReferensiCommand request, CancellationToken cancellationToken)
        {
            Domain.KebaruanReferensi.KebaruanReferensi? existingKebaruanReferensi = await kebaruanReferensiRepository.GetAsync(request.uuid, cancellationToken);

            if (existingKebaruanReferensi is null)
            {
                return Result.Failure(KebaruanReferensiErrors.NotFound(request.uuid));
            }

            await kebaruanReferensiRepository.DeleteAsync(existingKebaruanReferensi!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

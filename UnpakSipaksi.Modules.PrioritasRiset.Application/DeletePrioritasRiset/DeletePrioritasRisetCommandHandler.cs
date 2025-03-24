using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PrioritasRiset.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PrioritasRiset.Domain.PrioritasRiset;

namespace UnpakSipaksi.Modules.PrioritasRiset.Application.DeletePrioritasRiset
{
    internal sealed class DeletePrioritasRisetCommandHandler(
    IPrioritasRisetRepository PrioritasRisetRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeletePrioritasRisetCommand>
    {
        public async Task<Result> Handle(DeletePrioritasRisetCommand request, CancellationToken cancellationToken)
        {
            Domain.PrioritasRiset.PrioritasRiset? existingPrioritasRiset = await PrioritasRisetRepository.GetAsync(request.uuid, cancellationToken);

            if (existingPrioritasRiset is null)
            {
                return Result.Failure(PrioritasRisetErrors.NotFound(request.uuid));
            }

            await PrioritasRisetRepository.DeleteAsync(existingPrioritasRiset!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

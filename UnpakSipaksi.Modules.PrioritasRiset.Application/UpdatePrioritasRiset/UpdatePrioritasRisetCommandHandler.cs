using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PrioritasRiset.Domain.PrioritasRiset;
using UnpakSipaksi.Modules.PrioritasRiset.Application.Abstractions.Data;

namespace UnpakSipaksi.Modules.PrioritasRiset.Application.UpdatePrioritasRiset
{
    internal sealed class UpdatePrioritasRisetCommandHandler(
    IPrioritasRisetRepository PrioritasRisetRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdatePrioritasRisetCommand>
    {
        public async Task<Result> Handle(UpdatePrioritasRisetCommand request, CancellationToken cancellationToken)
        {
            Domain.PrioritasRiset.PrioritasRiset? existingPrioritasRiset = await PrioritasRisetRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingPrioritasRiset is null)
            {
                Result.Failure(PrioritasRisetErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Result<Domain.PrioritasRiset.PrioritasRiset> asset = Domain.PrioritasRiset.PrioritasRiset.Update(existingPrioritasRiset!)
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

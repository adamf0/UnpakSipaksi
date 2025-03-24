using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PrioritasRiset.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PrioritasRiset.Domain.PrioritasRiset;

namespace UnpakSipaksi.Modules.PrioritasRiset.Application.CreatePrioritasRiset
{
    internal sealed class CreatePrioritasRisetCommandHandler(
    IPrioritasRisetRepository PrioritasRisetRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreatePrioritasRisetCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreatePrioritasRisetCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.PrioritasRiset.PrioritasRiset> result = Domain.PrioritasRiset.PrioritasRiset.Create(
                request.Nama
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            PrioritasRisetRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}

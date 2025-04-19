using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Roadmap.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Roadmap.Domain.Roadmap;

namespace UnpakSipaksi.Modules.Roadmap.Application.CreateRoadmap
{
    internal sealed class CreateRoadmapCommandHandler(
    IRoadmapRepository RoadmapRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateRoadmapCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateRoadmapCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.Roadmap.Roadmap> result = Domain.Roadmap.Roadmap.Create(
                request.Nidn,
                request.Link
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            RoadmapRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}

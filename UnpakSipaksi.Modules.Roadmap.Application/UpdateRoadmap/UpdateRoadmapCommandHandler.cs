using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Roadmap.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Roadmap.Domain.Roadmap;

namespace UnpakSipaksi.Modules.Roadmap.Application.UpdateRoadmap
{
    internal sealed class UpdateRoadmapCommandHandler(
    IRoadmapRepository RoadmapRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateRoadmapCommand>
    {
        public async Task<Result> Handle(UpdateRoadmapCommand request, CancellationToken cancellationToken)
        {
            Domain.Roadmap.Roadmap? existingRoadmap = await RoadmapRepository.GetAsync(request.Uuid, cancellationToken);

            if (existingRoadmap is null)
            {
                Result.Failure(RoadmapErrors.NotFound(request.Uuid));
            }

            Result<Domain.Roadmap.Roadmap> asset = Domain.Roadmap.Roadmap.Update(existingRoadmap!)
                         .ChangeNidn(request.Nidn)
                         .ChangeLink(request.Link)
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

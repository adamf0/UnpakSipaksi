using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Roadmap.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Roadmap.Domain.Roadmap;

namespace UnpakSipaksi.Modules.Roadmap.Application.DeleteRoadmap
{
    internal sealed class DeleteRoadmapCommandHandler(
    IRoadmapRepository RoadmapRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteRoadmapCommand>
    {
        public async Task<Result> Handle(DeleteRoadmapCommand request, CancellationToken cancellationToken)
        {
            Domain.Roadmap.Roadmap? existingRoadmap = await RoadmapRepository.GetAsync(Guid.Parse(request.uuid), cancellationToken);

            if (existingRoadmap is null)
            {
                return Result.Failure(RoadmapErrors.NotFound(Guid.Parse(request.uuid)));
            }

            await RoadmapRepository.DeleteAsync(existingRoadmap!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

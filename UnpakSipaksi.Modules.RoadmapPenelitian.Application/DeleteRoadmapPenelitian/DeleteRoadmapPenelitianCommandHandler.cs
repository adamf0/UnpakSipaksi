using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RoadmapPenelitian.Application.Abstractions.Data;
using UnpakSipaksi.Modules.RoadmapPenelitian.Domain.RoadmapPenelitian;

namespace UnpakSipaksi.Modules.RoadmapPenelitian.Application.DeleteRoadmapPenelitian
{
    internal sealed class DeleteRoadmapPenelitianCommandHandler(
    IRoadmapPenelitianRepository RoadmapPenelitianRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteRoadmapPenelitianCommand>
    {
        public async Task<Result> Handle(DeleteRoadmapPenelitianCommand request, CancellationToken cancellationToken)
        {
            Domain.RoadmapPenelitian.RoadmapPenelitian? existingRoadmapPenelitian = await RoadmapPenelitianRepository.GetAsync(Guid.Parse(request.uuid), cancellationToken);

            if (existingRoadmapPenelitian is null)
            {
                return Result.Failure(RoadmapPenelitianErrors.NotFound(Guid.Parse(request.uuid)));
            }

            await RoadmapPenelitianRepository.DeleteAsync(existingRoadmapPenelitian!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

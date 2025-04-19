using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RoadmapPenelitian.Application.Abstractions.Data;
using UnpakSipaksi.Modules.RoadmapPenelitian.Domain.RoadmapPenelitian;

namespace UnpakSipaksi.Modules.RoadmapPenelitian.Application.UpdateRoadmapPenelitian
{
    internal sealed class UpdateRoadmapPenelitianCommandHandler(
    IRoadmapPenelitianRepository RoadmapPenelitianRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateRoadmapPenelitianCommand>
    {
        public async Task<Result> Handle(UpdateRoadmapPenelitianCommand request, CancellationToken cancellationToken)
        {
            Domain.RoadmapPenelitian.RoadmapPenelitian? existingRoadmapPenelitian = await RoadmapPenelitianRepository.GetAsync(request.Uuid, cancellationToken);

            if (existingRoadmapPenelitian is null)
            {
                Result.Failure(RoadmapPenelitianErrors.NotFound(request.Uuid));
            }

            Result<Domain.RoadmapPenelitian.RoadmapPenelitian> asset = Domain.RoadmapPenelitian.RoadmapPenelitian.Update(existingRoadmapPenelitian!)
                         .ChangeNama(request.Nama)
                         .ChangeSkor(request.Skor)
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

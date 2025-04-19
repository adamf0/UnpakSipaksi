using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RoadmapPenelitian.Application.Abstractions.Data;
using UnpakSipaksi.Modules.RoadmapPenelitian.Domain.RoadmapPenelitian;

namespace UnpakSipaksi.Modules.RoadmapPenelitian.Application.CreateRoadmapPenelitian
{
    internal sealed class CreateRoadmapPenelitianCommandHandler(
    IRoadmapPenelitianRepository RoadmapPenelitianRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateRoadmapPenelitianCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateRoadmapPenelitianCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.RoadmapPenelitian.RoadmapPenelitian> result = Domain.RoadmapPenelitian.RoadmapPenelitian.Create(
                request.Nama,
                request.Skor
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            RoadmapPenelitianRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}

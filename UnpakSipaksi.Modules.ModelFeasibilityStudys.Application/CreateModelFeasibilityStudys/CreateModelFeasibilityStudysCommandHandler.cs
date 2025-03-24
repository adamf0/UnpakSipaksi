using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.ModelFeasibilityStudys.Application.Abstractions.Data;
using UnpakSipaksi.Modules.ModelFeasibilityStudys.Domain.ModelFeasibilityStudys;

namespace UnpakSipaksi.Modules.ModelFeasibilityStudys.Application.CreateModelFeasibilityStudys
{
    internal sealed class CreateModelFeasibilityStudysCommandHandler(
    IModelFeasibilityStudysRepository ModelFeasibilityStudysRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateModelFeasibilityStudysCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateModelFeasibilityStudysCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.ModelFeasibilityStudys.ModelFeasibilityStudys> result = Domain.ModelFeasibilityStudys.ModelFeasibilityStudys.Create(
                request.Nama,
                request.BobotPDP,
                request.BobotTerapan,
                request.BobotKerjasama,
                request.BobotPenelitianDasar,
                request.Skor
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            ModelFeasibilityStudysRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}

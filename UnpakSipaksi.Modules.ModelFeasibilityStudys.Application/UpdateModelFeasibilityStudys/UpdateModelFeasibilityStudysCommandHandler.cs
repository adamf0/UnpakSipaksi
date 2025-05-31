using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.ModelFeasibilityStudys.Application.Abstractions.Data;
using UnpakSipaksi.Modules.ModelFeasibilityStudys.Domain.ModelFeasibilityStudys;

namespace UnpakSipaksi.Modules.ModelFeasibilityStudys.Application.UpdateModelFeasibilityStudys
{
    internal sealed class UpdateModelFeasibilityStudysCommandHandler(
    IModelFeasibilityStudysRepository ModelFeasibilityStudysRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateModelFeasibilityStudysCommand>
    {
        public async Task<Result> Handle(UpdateModelFeasibilityStudysCommand request, CancellationToken cancellationToken)
        {
            Domain.ModelFeasibilityStudys.ModelFeasibilityStudys? existingModelFeasibilityStudys = await ModelFeasibilityStudysRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingModelFeasibilityStudys is null)
            {
                return Result.Failure(ModelFeasibilityStudysErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Result<Domain.ModelFeasibilityStudys.ModelFeasibilityStudys> asset = Domain.ModelFeasibilityStudys.ModelFeasibilityStudys.Update(existingModelFeasibilityStudys!)
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

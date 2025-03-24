using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.ModelFeasibilityStudys.Application.Abstractions.Data;
using UnpakSipaksi.Modules.ModelFeasibilityStudys.Domain.ModelFeasibilityStudys;

namespace UnpakSipaksi.Modules.ModelFeasibilityStudys.Application.DeleteModelFeasibilityStudys
{
    internal sealed class DeleteModelFeasibilityStudysCommandHandler(
    IModelFeasibilityStudysRepository ModelFeasibilityStudysRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteModelFeasibilityStudysCommand>
    {
        public async Task<Result> Handle(DeleteModelFeasibilityStudysCommand request, CancellationToken cancellationToken)
        {
            Domain.ModelFeasibilityStudys.ModelFeasibilityStudys? existingModelFeasibilityStudys = await ModelFeasibilityStudysRepository.GetAsync(request.uuid, cancellationToken);

            if (existingModelFeasibilityStudys is null)
            {
                return Result.Failure(ModelFeasibilityStudysErrors.NotFound(request.uuid));
            }

            await ModelFeasibilityStudysRepository.DeleteAsync(existingModelFeasibilityStudys!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

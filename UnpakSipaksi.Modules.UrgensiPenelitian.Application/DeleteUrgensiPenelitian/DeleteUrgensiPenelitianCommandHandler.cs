using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.UrgensiPenelitian.Application.Abstractions.Data;
using UnpakSipaksi.Modules.UrgensiPenelitian.Domain.UrgensiPenelitian;

namespace UnpakSipaksi.Modules.UrgensiPenelitian.Application.DeleteUrgensiPenelitian
{
    internal sealed class DeleteUrgensiPenelitianCommandHandler(
    IUrgensiPenelitianRepository UrgensiPenelitianRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteUrgensiPenelitianCommand>
    {
        public async Task<Result> Handle(DeleteUrgensiPenelitianCommand request, CancellationToken cancellationToken)
        {
            Domain.UrgensiPenelitian.UrgensiPenelitian? existingUrgensiPenelitian = await UrgensiPenelitianRepository.GetAsync(request.uuid, cancellationToken);

            if (existingUrgensiPenelitian is null)
            {
                return Result.Failure(UrgensiPenelitianErrors.NotFound(request.uuid));
            }

            await UrgensiPenelitianRepository.DeleteAsync(existingUrgensiPenelitian!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

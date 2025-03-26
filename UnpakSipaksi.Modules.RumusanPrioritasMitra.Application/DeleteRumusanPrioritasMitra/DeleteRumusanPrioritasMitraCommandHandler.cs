using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RumusanPrioritasMitra.Application.Abstractions.Data;
using UnpakSipaksi.Modules.RumusanPrioritasMitra.Domain.RumusanPrioritasMitra;

namespace UnpakSipaksi.Modules.RumusanPrioritasMitra.Application.DeleteRumusanPrioritasMitra
{
    internal sealed class DeleteRumusanPrioritasMitraCommandHandler(
    IRumusanPrioritasMitraRepository RumusanPrioritasMitraRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteRumusanPrioritasMitraCommand>
    {
        public async Task<Result> Handle(DeleteRumusanPrioritasMitraCommand request, CancellationToken cancellationToken)
        {
            Domain.RumusanPrioritasMitra.RumusanPrioritasMitra? existingRumusanPrioritasMitra = await RumusanPrioritasMitraRepository.GetAsync(request.uuid, cancellationToken);

            if (existingRumusanPrioritasMitra is null)
            {
                return Result.Failure(RumusanPrioritasMitraErrors.NotFound(request.uuid));
            }

            await RumusanPrioritasMitraRepository.DeleteAsync(existingRumusanPrioritasMitra!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

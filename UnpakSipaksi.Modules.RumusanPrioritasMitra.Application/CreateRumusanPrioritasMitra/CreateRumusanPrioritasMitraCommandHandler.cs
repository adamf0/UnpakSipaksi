using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RumusanPrioritasMitra.Application.Abstractions.Data;
using UnpakSipaksi.Modules.RumusanPrioritasMitra.Domain.RumusanPrioritasMitra;

namespace UnpakSipaksi.Modules.RumusanPrioritasMitra.Application.CreateRumusanPrioritasMitra
{
    internal sealed class CreateRumusanPrioritasMitraCommandHandler(
    IRumusanPrioritasMitraRepository RumusanPrioritasMitraRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateRumusanPrioritasMitraCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateRumusanPrioritasMitraCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.RumusanPrioritasMitra.RumusanPrioritasMitra> result = Domain.RumusanPrioritasMitra.RumusanPrioritasMitra.Create(
                request.Nama,
                request.Nilai
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            RumusanPrioritasMitraRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}

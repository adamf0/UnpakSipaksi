using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.RumusanPrioritasMitra.Application.Abstractions.Data;
using UnpakSipaksi.Modules.RumusanPrioritasMitra.Domain.RumusanPrioritasMitra;

namespace UnpakSipaksi.Modules.RumusanPrioritasMitra.Application.UpdateRumusanPrioritasMitra
{
    internal sealed class UpdateRumusanPrioritasMitraCommandHandler(
    IRumusanPrioritasMitraRepository RumusanPrioritasMitraRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateRumusanPrioritasMitraCommand>
    {
        public async Task<Result> Handle(UpdateRumusanPrioritasMitraCommand request, CancellationToken cancellationToken)
        {
            Domain.RumusanPrioritasMitra.RumusanPrioritasMitra? existingRumusanPrioritasMitra = await RumusanPrioritasMitraRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingRumusanPrioritasMitra is null)
            {
                Result.Failure(RumusanPrioritasMitraErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Result<Domain.RumusanPrioritasMitra.RumusanPrioritasMitra> asset = Domain.RumusanPrioritasMitra.RumusanPrioritasMitra.Update(existingRumusanPrioritasMitra!)
                         .ChangeNama(request.Nama)
                         .ChangeNilai(request.Nilai)
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

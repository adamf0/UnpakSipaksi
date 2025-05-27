using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KredibilitasMitraDukungan.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KredibilitasMitraDukungan.Domain.KredibilitasMitraDukungan;

namespace UnpakSipaksi.Modules.KredibilitasMitraDukungan.Application.DeleteKredibilitasMitraDukungan
{
    internal sealed class DeleteKredibilitasMitraDukunganCommandHandler(
    IKredibilitasMitraDukunganRepository KredibilitasMitraDukunganRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteKredibilitasMitraDukunganCommand>
    {
        public async Task<Result> Handle(DeleteKredibilitasMitraDukunganCommand request, CancellationToken cancellationToken)
        {
            Domain.KredibilitasMitraDukungan.KredibilitasMitraDukungan? existingKredibilitasMitraDukungan = await KredibilitasMitraDukunganRepository.GetAsync(Guid.Parse(request.uuid), cancellationToken);

            if (existingKredibilitasMitraDukungan is null)
            {
                return Result.Failure(KredibilitasMitraDukunganErrors.NotFound(Guid.Parse(request.uuid)));
            }

            await KredibilitasMitraDukunganRepository.DeleteAsync(existingKredibilitasMitraDukungan!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

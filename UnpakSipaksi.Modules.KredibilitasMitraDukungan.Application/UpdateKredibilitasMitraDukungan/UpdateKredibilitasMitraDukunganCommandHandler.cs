using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KredibilitasMitraDukungan.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KredibilitasMitraDukungan.Domain.KredibilitasMitraDukungan;

namespace UnpakSipaksi.Modules.KredibilitasMitraDukungan.Application.UpdateKredibilitasMitraDukungan
{
    internal sealed class UpdateKredibilitasMitraDukunganCommandHandler(
    IKredibilitasMitraDukunganRepository KredibilitasMitraDukunganRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateKredibilitasMitraDukunganCommand>
    {
        public async Task<Result> Handle(UpdateKredibilitasMitraDukunganCommand request, CancellationToken cancellationToken)
        {
            Domain.KredibilitasMitraDukungan.KredibilitasMitraDukungan? existingKredibilitasMitraDukungan = await KredibilitasMitraDukunganRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingKredibilitasMitraDukungan is null)
            {
                return Result.Failure(KredibilitasMitraDukunganErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Result<Domain.KredibilitasMitraDukungan.KredibilitasMitraDukungan> asset = Domain.KredibilitasMitraDukungan.KredibilitasMitraDukungan.Update(existingKredibilitasMitraDukungan!)
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

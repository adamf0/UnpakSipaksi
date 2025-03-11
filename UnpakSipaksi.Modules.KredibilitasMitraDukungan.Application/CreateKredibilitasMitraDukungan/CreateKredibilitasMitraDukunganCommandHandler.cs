using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.KredibilitasMitraDukungan.Application.Abstractions.Data;
using UnpakSipaksi.Modules.KredibilitasMitraDukungan.Domain.KredibilitasMitraDukungan;

namespace UnpakSipaksi.Modules.KredibilitasMitraDukungan.Application.CreateKredibilitasMitraDukungan
{
    internal sealed class CreateKredibilitasMitraDukunganCommandHandler(
    IKredibilitasMitraDukunganRepository KredibilitasMitraDukunganRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateKredibilitasMitraDukunganCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateKredibilitasMitraDukunganCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.KredibilitasMitraDukungan.KredibilitasMitraDukungan> result = Domain.KredibilitasMitraDukungan.KredibilitasMitraDukungan.Create(
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

            KredibilitasMitraDukunganRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}

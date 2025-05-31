using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Domain.PeningkatanKeberdayaanMitra;

namespace UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Application.UpdatePeningkatanKeberdayaanMitra
{
    internal sealed class UpdatePeningkatanKeberdayaanMitraCommandHandler(
    IPeningkatanKeberdayaanMitraRepository PeningkatanKeberdayaanMitraRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdatePeningkatanKeberdayaanMitraCommand>
    {
        public async Task<Result> Handle(UpdatePeningkatanKeberdayaanMitraCommand request, CancellationToken cancellationToken)
        {
            Domain.PeningkatanKeberdayaanMitra.PeningkatanKeberdayaanMitra? existingPeningkatanKeberdayaanMitra = await PeningkatanKeberdayaanMitraRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingPeningkatanKeberdayaanMitra is null)
            {
                return Result.Failure(PeningkatanKeberdayaanMitraErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Result<Domain.PeningkatanKeberdayaanMitra.PeningkatanKeberdayaanMitra> asset = Domain.PeningkatanKeberdayaanMitra.PeningkatanKeberdayaanMitra.Update(existingPeningkatanKeberdayaanMitra!)
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

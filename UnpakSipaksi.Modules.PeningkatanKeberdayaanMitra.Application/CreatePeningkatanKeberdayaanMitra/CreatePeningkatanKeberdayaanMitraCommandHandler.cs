using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Domain.PeningkatanKeberdayaanMitra;

namespace UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Application.CreatePeningkatanKeberdayaanMitra
{
    internal sealed class CreatePeningkatanKeberdayaanMitraCommandHandler(
    IPeningkatanKeberdayaanMitraRepository PeningkatanKeberdayaanMitraRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreatePeningkatanKeberdayaanMitraCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreatePeningkatanKeberdayaanMitraCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.PeningkatanKeberdayaanMitra.PeningkatanKeberdayaanMitra> result = Domain.PeningkatanKeberdayaanMitra.PeningkatanKeberdayaanMitra.Create(
                request.Nama,
                request.Nilai
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            PeningkatanKeberdayaanMitraRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}

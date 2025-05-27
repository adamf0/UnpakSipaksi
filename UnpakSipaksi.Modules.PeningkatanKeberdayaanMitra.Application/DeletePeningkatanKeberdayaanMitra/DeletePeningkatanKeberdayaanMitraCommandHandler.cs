using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Application.Abstractions.Data;
using UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Domain.PeningkatanKeberdayaanMitra;

namespace UnpakSipaksi.Modules.PeningkatanKeberdayaanMitra.Application.DeletePeningkatanKeberdayaanMitra
{
    internal sealed class DeletePeningkatanKeberdayaanMitraCommandHandler(
    IPeningkatanKeberdayaanMitraRepository PeningkatanKeberdayaanMitraRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeletePeningkatanKeberdayaanMitraCommand>
    {
        public async Task<Result> Handle(DeletePeningkatanKeberdayaanMitraCommand request, CancellationToken cancellationToken)
        {
            Domain.PeningkatanKeberdayaanMitra.PeningkatanKeberdayaanMitra? existingPeningkatanKeberdayaanMitra = await PeningkatanKeberdayaanMitraRepository.GetAsync(Guid.Parse(request.uuid), cancellationToken);

            if (existingPeningkatanKeberdayaanMitra is null)
            {
                return Result.Failure(PeningkatanKeberdayaanMitraErrors.NotFound(Guid.Parse(request.uuid)));
            }

            await PeningkatanKeberdayaanMitraRepository.DeleteAsync(existingPeningkatanKeberdayaanMitra!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

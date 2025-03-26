using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.VideoKegiatan.Application.Abstractions.Data;
using UnpakSipaksi.Modules.VideoKegiatan.Domain.VideoKegiatan;

namespace UnpakSipaksi.Modules.VideoKegiatan.Application.CreateVideoKegiatan
{
    internal sealed class CreateVideoKegiatanCommandHandler(
    IVideoKegiatanRepository VideoKegiatanRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateVideoKegiatanCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateVideoKegiatanCommand request, CancellationToken cancellationToken)
        {
            Result<Domain.VideoKegiatan.VideoKegiatan> result = Domain.VideoKegiatan.VideoKegiatan.Create(
                request.Nama,
                request.Nilai
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            VideoKegiatanRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}

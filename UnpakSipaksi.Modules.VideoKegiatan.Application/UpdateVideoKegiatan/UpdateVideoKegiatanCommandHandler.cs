using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.VideoKegiatan.Application.Abstractions.Data;
using UnpakSipaksi.Modules.VideoKegiatan.Domain.VideoKegiatan;

namespace UnpakSipaksi.Modules.VideoKegiatan.Application.UpdateVideoKegiatan
{
    internal sealed class UpdateVideoKegiatanCommandHandler(
    IVideoKegiatanRepository VideoKegiatanRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateVideoKegiatanCommand>
    {
        public async Task<Result> Handle(UpdateVideoKegiatanCommand request, CancellationToken cancellationToken)
        {
            Domain.VideoKegiatan.VideoKegiatan? existingVideoKegiatan = await VideoKegiatanRepository.GetAsync(Guid.Parse(request.Uuid), cancellationToken);

            if (existingVideoKegiatan is null)
            {
                return Result.Failure(VideoKegiatanErrors.NotFound(Guid.Parse(request.Uuid)));
            }

            Result<Domain.VideoKegiatan.VideoKegiatan> asset = Domain.VideoKegiatan.VideoKegiatan.Update(existingVideoKegiatan!)
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

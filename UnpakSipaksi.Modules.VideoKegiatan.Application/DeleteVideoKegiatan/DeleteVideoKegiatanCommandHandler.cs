using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.VideoKegiatan.Application.Abstractions.Data;
using UnpakSipaksi.Modules.VideoKegiatan.Domain.VideoKegiatan;

namespace UnpakSipaksi.Modules.VideoKegiatan.Application.DeleteVideoKegiatan
{
    internal sealed class DeleteVideoKegiatanCommandHandler(
    IVideoKegiatanRepository VideoKegiatanRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteVideoKegiatanCommand>
    {
        public async Task<Result> Handle(DeleteVideoKegiatanCommand request, CancellationToken cancellationToken)
        {
            Domain.VideoKegiatan.VideoKegiatan? existingVideoKegiatan = await VideoKegiatanRepository.GetAsync(Guid.Parse(request.uuid), cancellationToken);

            if (existingVideoKegiatan is null)
            {
                return Result.Failure(VideoKegiatanErrors.NotFound(Guid.Parse(request.uuid)));
            }

            await VideoKegiatanRepository.DeleteAsync(existingVideoKegiatan!);
            //event update change table position asset, order desc + select first

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Pengumuman.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Pengumuman.Application.CreatePengumuman;
using UnpakSipaksi.Modules.Pengumuman.Domain.Pengumuman;

namespace UnpakSipaksi.Modules.Pengumuman.Application.UpdatePengumuman
{
    internal sealed class UpdatePengumumanCommandHandler(
    IPengumumanRepository PengumumanRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UpdatePengumumanCommand>
    {
        public AnnouncementInfo AnnouncementInfoFactory(UpdatePengumumanCommand request)
        {
            return string.IsNullOrEmpty(request.Nidn) ?
                AnnouncementInfo.CreateForFakultas(
                    request.Type.ToEnumFromString<AnnouncementType>(),
                    request.Target!.ToEnumFromString<TargetType>(),
                    request.KodeFaKultas
                ) :
                AnnouncementInfo.CreateForDosen(
                    request.Type.ToEnumFromString<AnnouncementType>(),
                    request.Target!.ToEnumFromString<TargetType>(),
                    request.Nidn
                );
        }
        public Attachment AttachmentFactory(UpdatePengumumanCommand request)
        {
            return string.IsNullOrEmpty(request.File) ?
                Attachment.FromPath(request.File!) :
                Attachment.FromUrl(request.Url!);
        }

        public async Task<Result> Handle(UpdatePengumumanCommand request, CancellationToken cancellationToken)
        {
            Domain.Pengumuman.Pengumuman? existingPengumuman = await PengumumanRepository.GetAsync(request.Uuid, cancellationToken);

            if (existingPengumuman is null)
            {
                return Result.Failure(PengumumanErrors.NotFound(request.Uuid));
            }

            var expiredTypeParsed = request.TypeExpired.ToEnumFromString<ExpiredType>();
            DateOnly? tanggalAwal = DateOnly.TryParse(request.TanggalAwal, out var tglAwal) ? tglAwal : null;
            DateOnly? tanggalAkhir = DateOnly.TryParse(request.TanggalAkhir, out var tglAkhir) ? tglAkhir : null;

            Result<Domain.Pengumuman.Pengumuman> asset = Domain.Pengumuman.Pengumuman.Update(existingPengumuman!)
                         .ChangeMessage(request.Pesan)
                         .ChangeAnnouncementInfo(AnnouncementInfoFactory(request))
                         .ChangeAttachment(AttachmentFactory(request))
                         .ChangeExpiredInfo(
                            new ExpiredInfo(
                                expiredTypeParsed,
                                tanggalAwal,
                                tanggalAkhir
                            )
                         )
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

using MediatR;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Common.Domain;
using UnpakSipaksi.Modules.Pengumuman.Application.Abstractions.Data;
using UnpakSipaksi.Modules.Pengumuman.Domain.Pengumuman;

namespace UnpakSipaksi.Modules.Pengumuman.Application.CreatePengumuman
{
    internal sealed class CreatePengumumanCommandHandler(
    IPengumumanRepository PengumumanRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreatePengumumanCommand, Guid>
    {
        public AnnouncementInfo AnnouncementInfoFactory(CreatePengumumanCommand request) {
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
        public Attachment AttachmentFactory(CreatePengumumanCommand request)
        {
            return string.IsNullOrEmpty(request.File) ?
                Attachment.FromPath(request.File!) :
                Attachment.FromUrl(request.Url!);
        }

        public async Task<Result<Guid>> Handle(CreatePengumumanCommand request, CancellationToken cancellationToken)
        {
            var expiredTypeParsed = request.TypeExpired.ToEnumFromString<ExpiredType>();
            DateOnly? tanggalAwal = DateOnly.TryParse(request.TanggalAwal, out var tglAwal) ? tglAwal : null;
            DateOnly? tanggalAkhir = DateOnly.TryParse(request.TanggalAkhir, out var tglAkhir) ? tglAkhir : null;

            Result<Domain.Pengumuman.Pengumuman> result = Domain.Pengumuman.Pengumuman.Create(
                request.Pesan,
                AnnouncementInfoFactory(request),
                AttachmentFactory(request),
                new ExpiredInfo(
                    expiredTypeParsed,
                    tanggalAwal,
                    tanggalAkhir
                )
            );

            if (result.IsFailure)
            {
                return Result.Failure<Guid>(result.Error);
            }

            PengumumanRepository.Insert(result.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return result.Value.Uuid;
        }
    }
}

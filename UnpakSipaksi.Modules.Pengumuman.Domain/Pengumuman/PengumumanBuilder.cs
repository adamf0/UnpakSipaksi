using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.Pengumuman.Domain.Pengumuman
{
    public sealed partial class Pengumuman
    {
        public sealed class PengumumanBuilder
        {
            private readonly Pengumuman _akurasiPenelitian;
            private Result? _result;

            public PengumumanBuilder(Pengumuman akurasiPenelitian)
            {
                _akurasiPenelitian = akurasiPenelitian;
            }

            private bool HasError => _result is not null && _result.IsFailure;

            public Result<Pengumuman> Build()
            {
                return HasError ? Result.Failure<Pengumuman>(_result!.Error) : Result.Success(_akurasiPenelitian);
            }

            public PengumumanBuilder ChangeMessage(string Pesan)
            {
                if (HasError) return this;

                _akurasiPenelitian.Pesan = Pesan;
                return this;
            }
            public PengumumanBuilder ChangeAnnouncementInfo(AnnouncementInfo AnnouncementInfo)
            {
                if (HasError) return this;

                if (!string.IsNullOrEmpty(AnnouncementInfo?.Nidn) && DomainValidator.IsValidNidn(AnnouncementInfo?.Nidn))
                {
                    _result =  Result.Failure<Pengumuman>(PengumumanErrors.InvalidNidn());
                    return this;
                }

                _akurasiPenelitian.Type = AnnouncementInfo.Type.ToString();
                _akurasiPenelitian.Target = AnnouncementInfo.Target.ToString();
                _akurasiPenelitian.Nidn = AnnouncementInfo?.Nidn;
                _akurasiPenelitian.KodeFaKultas = AnnouncementInfo?.KodeFakultas;
                return this;
            }
            public PengumumanBuilder ChangeAttachment(Attachment Attachment)
            {
                if (HasError) return this;

                _akurasiPenelitian.File = Attachment?.Path;
                _akurasiPenelitian.Url = Attachment?.Link;
                return this;
            }
            public PengumumanBuilder ChangeExpiredInfo(ExpiredInfo expiredInfo)
            {
                if (HasError) return this;

                if (expiredInfo.validationResult.IsFailure)
                {
                    _result = expiredInfo.validationResult;
                }

                _akurasiPenelitian.TypeExpired = expiredInfo.Type.ToString();
                _akurasiPenelitian.TanggalAwal = expiredInfo.TanggalAwal?.ToString();
                _akurasiPenelitian.TanggalAkhir = expiredInfo.TanggalAkhir?.ToString();
                return this;
            }
        }
    }
}

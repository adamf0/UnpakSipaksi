using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.Pengumuman.Domain.Pengumuman
{
    public sealed partial class ExpiredInfo
    {
        public ExpiredType Type { get; }
        public DateOnly? TanggalAwal { get; }
        public DateOnly? TanggalAkhir { get; }
        
        public readonly Result<Pengumuman> validationResult;

        public ExpiredInfo(ExpiredType type, DateOnly? tanggalAwal = null, DateOnly? tanggalAkhir = null)
        {
            Type = type;

            if (type == ExpiredType.Range)
            {
                if (!tanggalAwal.HasValue || !tanggalAkhir.HasValue)
                    validationResult = Result.Failure<Pengumuman>(PengumumanErrors.RequireDateRangeExpired());

                else if (tanggalAkhir < tanggalAwal)
                    validationResult = Result.Failure<Pengumuman>(PengumumanErrors.InvalidDateRangeExpired());

                TanggalAwal = tanggalAwal;
                TanggalAkhir = tanggalAkhir;
            }
        }

        public bool IsNoExpire=> Type == ExpiredType.NoExpire;
    }

}

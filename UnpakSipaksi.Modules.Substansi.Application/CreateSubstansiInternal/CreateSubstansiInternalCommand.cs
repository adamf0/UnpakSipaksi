using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.Substansi.Application.CreateSubstansiInternal
{
    public sealed record CreateSubstansiInternalCommand(
        string UuidPenelitianHibah,
        string? UuidPublikasiDisitasiProposal,
        string? UuidRelevansiKepakaranTemaProposal,
        string? UuidJumlahKolaboratorPublikasiBereputasi,
        string? UuidRelevansiProdukKepentinganNasional,
        string? UuidKetajamanPerumusanMasalah,
        string? UuidInovasiPemecahanMasalah,
        string? UuidSotaKebaharuan,
        string? UuidRoadmapPenelitian,
        string? UuidAkurasiPenelitian,
        string? UuidKejelasanPembagianTugasTim,
        string? UuidKesesuaianWaktuRabLuaranFasilitas,
        string? UuidPotensiKetercapaianLuaranDijanjikan,
        string? UuidModelFeasibilityStudy,
        string? UuidKesesuaianTkt,
        string? UuidKredibilitasMitraDukungan,
        string? UuidKebaruanReferensi,
        string? UuidRelevansiKualitasReferensi,
        string? Komentar,
        string? UuidReviewerInternal,
        string? UuidReviewerExternal,
        string TanggalMulai,
        string TanggalBerakhir
    ) : ICommand<Guid>;
}

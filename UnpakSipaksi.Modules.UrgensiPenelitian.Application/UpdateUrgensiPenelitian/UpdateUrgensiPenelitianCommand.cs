using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.UrgensiPenelitian.Application.UpdateUrgensiPenelitian
{
    public sealed record UpdateUrgensiPenelitianCommand(
        Guid Uuid,
        string UuidRelevansiProdukKepentinganNasional,
        string UuidKetajamanPerumusanMasalah,
        string UuidInovasiPemecahanMasalah,
        string UuidSotaKebaharuan,
        string UuidRoadmapPenelitian,
        int Nilai
    ) : ICommand;
}

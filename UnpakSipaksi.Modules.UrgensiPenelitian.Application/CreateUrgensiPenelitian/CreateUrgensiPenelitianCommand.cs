using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.UrgensiPenelitian.Application.CreateUrgensiPenelitian
{
    public sealed record CreateUrgensiPenelitianCommand(
        string UuidRelevansiProdukKepentinganNasional,
        string UuidKetajamanPerumusanMasalah,
        string UuidInovasiPemecahanMasalah,
        string UuidSotaKebaharuan,
        string UuidRoadmapPenelitian,
        int Nilai
    ) : ICommand<Guid>;
}

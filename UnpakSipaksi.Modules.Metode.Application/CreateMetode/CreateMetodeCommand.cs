using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.Metode.Application.CreateMetode
{
    public sealed record CreateMetodeCommand(
        string UuidAkurasiPenelitian,
        string UuidKejelasanPembagianTugasTim,
        string UuidKesesuaianWaktuRabLuaranFasilitas,
        string UuidPotensiKetercapaianLuaranDijanjikan,
        string UuidModelFeasibilityStudy,
        string UuidKesesuaianTkt,
        string UuidKredibilitasMitraDukungan
    ) : ICommand<Guid>;
}

using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.Metode.Application.UpdateMetode
{
    public sealed record UpdateMetodeCommand(
        Guid Uuid,
        string UuidAkurasiPenelitian,
        string UuidKejelasanPembagianTugasTim,
        string UuidKesesuaianWaktuRabLuaranFasilitas,
        string UuidPotensiKetercapaianLuaranDijanjikan,
        string UuidModelFeasibilityStudy,
        string UuidKesesuaianTkt,
        string UuidKredibilitasMitraDukungan
    ) : ICommand;
}

using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.Komponen.Application.UpdateKomponen
{
    public sealed record UpdateKomponenCommand(
        string Uuid,
        string Nama,
        int? MaxBiaya
    ) : ICommand;
}

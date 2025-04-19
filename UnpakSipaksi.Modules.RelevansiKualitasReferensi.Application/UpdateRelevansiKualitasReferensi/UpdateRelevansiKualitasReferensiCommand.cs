using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.RelevansiKualitasReferensi.Application.UpdateRelevansiKualitasReferensi
{
    public sealed record UpdateRelevansiKualitasReferensiCommand(
        Guid Uuid,
        string Nama,
        int Skor
    ) : ICommand;
}

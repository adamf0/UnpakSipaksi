using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.RelevansiKualitasReferensi.Application.UpdateRelevansiKualitasReferensi
{
    public sealed record UpdateRelevansiKualitasReferensiCommand(
        string Uuid,
        string Nama,
        int Skor
    ) : ICommand;
}

using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.RelevansiKualitasReferensi.Application.CreateRelevansiKualitasReferensi
{
    public sealed record CreateRelevansiKualitasReferensiCommand(
        string Nama,
        int Skor
    ) : ICommand<Guid>;
}

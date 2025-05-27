using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.RelevansiKualitasReferensi.Application.DeleteRelevansiKualitasReferensi
{
    public sealed record DeleteRelevansiKualitasReferensiCommand(
        string uuid
    ) : ICommand;
}

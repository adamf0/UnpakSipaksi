using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.SotaKebaharuan.Application.UpdateSotaKebaharuan
{
    public sealed record UpdateSotaKebaharuanCommand(
        string Uuid,
        string Nama,
        int Skor
    ) : ICommand;
}

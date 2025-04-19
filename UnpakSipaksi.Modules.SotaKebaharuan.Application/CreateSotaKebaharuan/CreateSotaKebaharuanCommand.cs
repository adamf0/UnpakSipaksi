using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.SotaKebaharuan.Application.CreateSotaKebaharuan
{
    public sealed record CreateSotaKebaharuanCommand(
        string Nama,
        int Skor
    ) : ICommand<Guid>;
}

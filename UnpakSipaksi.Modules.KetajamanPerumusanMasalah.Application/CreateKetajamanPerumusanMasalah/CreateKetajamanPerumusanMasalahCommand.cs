using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.KetajamanPerumusanMasalah.Application.CreateKetajamanPerumusanMasalah
{
    public sealed record CreateKetajamanPerumusanMasalahCommand(
        string Nama,
        int Skor
    ) : ICommand<Guid>;
}

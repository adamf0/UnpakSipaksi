using UnpakSipaksi.Common.Application.Messaging;

//[PR] ini bisa null
namespace UnpakSipaksi.Modules.Komponen.Application.CreateKomponen
{
    public sealed record CreateKomponenCommand(
        string Nama,
        int? MaxBiaya
    ) : ICommand<Guid>;
}

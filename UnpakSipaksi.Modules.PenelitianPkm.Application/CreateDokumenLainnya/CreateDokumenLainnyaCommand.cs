using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.CreateDokumenLainnya
{
    public sealed record CreateDokumenLainnyaCommand(
          string UuidPenelitianPkm,
          string File,
          string? Kategori
    ) : ICommand<Guid>;
}

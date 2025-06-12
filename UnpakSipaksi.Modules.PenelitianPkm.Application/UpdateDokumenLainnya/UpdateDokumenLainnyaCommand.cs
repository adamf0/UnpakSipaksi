using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.UpdateDokumenLainnya
{
    public sealed record UpdateDokumenLainnyaCommand(
          string Uuid,
          string UuidPenelitianPkm,
          string File,
          string? Kategori
    ) : ICommand;
}

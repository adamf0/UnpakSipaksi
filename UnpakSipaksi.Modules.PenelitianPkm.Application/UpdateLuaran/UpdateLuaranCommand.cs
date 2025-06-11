using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.UpdateLuaran
{
    public sealed record UpdateLuaranCommand(
          string Uuid,
          string UuidPenelitianPkm,
          string? UuidJenisLuaran,
          string? UuidIndikatorCapaian,
          string? Keterangan,
          string? Link,
          string Jenis
    ) : ICommand;
}

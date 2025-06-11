using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.CreateLuaran
{
    public sealed record CreateLuaranCommand(
          string UuidPenelitianPkm,
          string? UuidJenisLuaran,
          string? UuidIndikatorCapaian,
          string? Keterangan,
          string? Link,
          string Jenis
    ) : ICommand<Guid>;
}

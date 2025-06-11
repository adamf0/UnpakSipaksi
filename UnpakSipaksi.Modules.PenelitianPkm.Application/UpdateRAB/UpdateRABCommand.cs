using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.UpdateRAB
{
    public sealed record UpdateRABCommand(
          string Uuid,
          string UuidPenelitianPkm,
          string? UuidKelompokRab,
          string? UuidKomponen,
          int? Item,
          string? UuidSatuan,
          int? HargaSatuan,
          int? Total
    ) : ICommand;
}

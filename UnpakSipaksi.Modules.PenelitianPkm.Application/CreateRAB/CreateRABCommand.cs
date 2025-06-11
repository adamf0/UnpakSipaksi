using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.CreateRAB
{
    public sealed record CreateRABCommand(
          string UuidPenelitianPkm,
          string? UuidKelompokRab,
          string? UuidKomponen,
          int? Item,
          string? UuidSatuan,
          int? HargaSatuan,
          int? Total
    ) : ICommand<Guid>;
}

using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.CreateRAB
{
    public sealed record CreateRABCommand(
          string UuidPenelitianHibah,
          string? UuidKelompokRab,
          string? UuidKomponen,
          int? Item,
          string? UuidSatuan,
          int? HargaSatuan,
          int? Total
    ) : ICommand<Guid>;
}

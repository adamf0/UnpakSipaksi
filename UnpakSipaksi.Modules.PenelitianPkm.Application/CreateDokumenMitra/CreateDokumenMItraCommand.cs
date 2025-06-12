using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.CreateDokumenMitra
{
    public sealed record CreateDokumenMitraCommand(
          string UuidPenelitianPkm,
          string Mitra,
          string Provinsi,
          string Kota,
          string UuidKelompokMitra,
          string PemimpinMitra,
          string? KontakMitra,
          string File
    ) : ICommand<Guid>;
}

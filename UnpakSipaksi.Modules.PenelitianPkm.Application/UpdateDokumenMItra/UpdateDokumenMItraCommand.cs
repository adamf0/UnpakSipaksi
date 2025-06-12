using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.UpdateDokumenMitra
{
    public sealed record UpdateDokumenMitraCommand(
          string Uuid,
          string UuidPenelitianPkm,
          string Mitra,
          string Provinsi,
          string Kota,
          string UuidKelompokMitra,
          string PemimpinMitra,
          string? KontakMitra,
          string File
    ) : ICommand;
}

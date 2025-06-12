using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.UpdateKategoriPengabdian
{
    public sealed record UpdateKategoriPengabdianCommand(
          string Uuid,
          string UuidPenelitianPkm,
          string UuidKategoriPengabdian
    ) : ICommand;
}

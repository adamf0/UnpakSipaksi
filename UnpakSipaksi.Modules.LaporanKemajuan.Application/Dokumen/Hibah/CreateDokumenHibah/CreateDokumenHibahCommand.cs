using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.LaporanKemajuan.Application.Dokumen.Hibah.CreateDokumenHibah
{
    public sealed record CreateDokumenHibahCommand(
        string UuidPenenitianHibah,
        string File,
        string Type
    ) : ICommand<Guid>;
}

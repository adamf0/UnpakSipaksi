using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.LaporanKemajuan.Application.Dokumen.Hibah.UpdateDokumenHibah
{
    public sealed record UpdateDokumenHibahCommand(
        string Uuid,
        string UuidPenenitianHibah,
        string File,
        string Type
    ) : ICommand;
}

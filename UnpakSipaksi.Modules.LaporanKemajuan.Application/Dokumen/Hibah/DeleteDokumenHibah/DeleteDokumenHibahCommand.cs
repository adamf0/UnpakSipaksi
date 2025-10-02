using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.LaporanKemajuan.Application.Dokumen.Hibah.DeleteDokumenHibah
{
    public sealed record DeleteDokumenHibahCommand(
        string uuid
    ) : ICommand;
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;

namespace UnpakSipaksi.Modules.Logbook.Application.CreateLogbook
{
    public sealed record CreateLogbookCommand(
        string? UuidPenelitianHibah,
        string? UuidPenelitianPkm,
        string TanggalKegiatan,
        string Lampiran,
        string Deskripsi,
        double Persentase
    ) : ICommand<Guid>;
}

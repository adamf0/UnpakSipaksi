using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.Logbook.Application.GetLogbook
{
    public sealed record LogbookResponse
    {
        public string Uuid { get; set; }
        public string UuidPenelitianHibah { get; private set; }
        public string UuidPenelitianPkm { get; private set; }
        public string? NIDN { get; set; }
        public DateOnly TanggalKegiatan { get; private set; }
        public string Lampiran { get; private set; }
        public string Deskripsi { get; private set; }
        public double Persentase { get; private set; }
    }
}

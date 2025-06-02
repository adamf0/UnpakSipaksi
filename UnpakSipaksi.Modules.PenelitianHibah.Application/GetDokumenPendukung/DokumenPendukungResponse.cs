using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.GetDokumenPendukung
{
    public sealed record DokumenPendukungResponse
    {
        public string Uuid { get; set; }
        public string UuidPenelitianHibah { get; set; }
        public string? File { get; set; }
        public string? Link { get; set; }
        public string Kategori { get; set; }
    }
}

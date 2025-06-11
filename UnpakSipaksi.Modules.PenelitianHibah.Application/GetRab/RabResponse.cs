using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.GetRab
{
    public sealed record RabResponse
    {
        public string? Uuid { get; set; }
        public string? UuidPenelitianHibah { get; set; }
        public long? HargaSatuan { get; set; }
        public long? Item { get; set; }
        public long? Total { get; set; }
        public KelompokResponse? Kelompok { get; set; }
        public KomponenResponse? Komponen { get; set; }
        public SatuanResponse? Satuan { get; set; }
    }
}

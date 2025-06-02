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
        public int? HargaSatuan { get; set; }
        public int? Item { get; set; }
        public int? Total { get; set; }
        public KelompokResponse? Kelompok { get; set; }
        public KomponenResponse? Komponen { get; set; }
        public SatuanResponse? Satuan { get; set; }
    }
}

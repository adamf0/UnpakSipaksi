using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.Referensi.Application.GetReferensi
{
    public sealed record ReferensiDefaultResponse
    {
        public string Id { get; set; }
        public string Uuid { get; set; }
        public string UuidKebaruanReferensi { get; set; } = default!;
        public string KebaruanReferensiId { get; set; } = default!;
        public string UuidRelevansiKualitasReferensi { get; set; } = default!;
        public string RelevansiKualitasReferensiId { get; set; } = default!;
        public int Nilai { get; set; }
    }
}

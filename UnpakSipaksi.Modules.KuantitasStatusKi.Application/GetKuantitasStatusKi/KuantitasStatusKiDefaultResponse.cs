using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KuantitasStatusKi.Application.GetKuantitasStatusKi
{
    public sealed record KuantitasStatusKiDefaultResponse
    {
        public string Id { get; set; }
        public string Uuid { get; set; }
        public string Nama { get; set; } = default!;
        public int Nilai { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.TemaPenelitian.Application.GetTemaPenelitian
{
    public sealed record TemaPenelitianDefaultResponse
    {
        public string Id { get; set; }
        public string Uuid { get; set; }
        public string FokusPenelitianUuid { get; set; }
        public string Nama { get; set; } = default!;
    }
}

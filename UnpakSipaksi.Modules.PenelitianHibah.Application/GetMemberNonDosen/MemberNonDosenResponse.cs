using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.GetMemberNonDosen
{
    public sealed record MemberNonDosenResponse
    {
        public string Uuid { get; set; }
        public string UuidPenelitianHibah { get; set; }
        public string? NomorIdentitas { get; set; }
        public string? Nama { get; set; }
        public string? Afiliasi { get; set; }
    }
}

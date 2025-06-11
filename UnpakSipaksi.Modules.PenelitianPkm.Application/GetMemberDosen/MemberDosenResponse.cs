using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.GetMemberDosen
{
    public sealed record MemberDosenResponse
    {
        public string Uuid { get; set; }
        public string UuidPenelitianHibah { get; set; }
        public string NIDN { get; set; }
        public string Status { get; set; }
    }
}

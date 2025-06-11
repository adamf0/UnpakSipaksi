using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.GetMemberMahasiswa
{
    public sealed record MemberMahasiswaResponse
    {
        public string Uuid { get; set; }
        public string UuidPenelitianHibah { get; set; }
        public string NPM { get; set; }
        //[PR] belum masukkan nama mahasiswa
        public string BuktiMbkm { get; set; }
    }
}

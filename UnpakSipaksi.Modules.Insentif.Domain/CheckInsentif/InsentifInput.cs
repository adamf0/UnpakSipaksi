using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Modules.Insentif.Domain.Insentif;
using UnpakSipaksi.Modules.Insentif.Domain.VerifikasiFakultas;

namespace UnpakSipaksi.Modules.Insentif.Domain.CheckInsentif
{
    public class InsentifInput
    {
        public JenisJurnal JenisJurnal { get; set; }
        public bool Mahasiswa { get; set; }
        public Peran PeranPenulis { get; set; }
        public decimal SBU { get; set; }
        public int JumlahCoAuthor { get; set; }
    }

}

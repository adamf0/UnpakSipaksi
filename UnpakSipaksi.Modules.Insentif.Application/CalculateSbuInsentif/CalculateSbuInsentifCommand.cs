using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Application.Messaging;
using UnpakSipaksi.Modules.Insentif.Application.GetInsentif;
using UnpakSipaksi.Modules.Insentif.Domain.CheckInsentif;
using UnpakSipaksi.Modules.Insentif.Domain.Insentif;

namespace UnpakSipaksi.Modules.Insentif.Application.CalculateSbuInsentif
{
    public sealed record CalculateSbuInsentifCommand(
            string StatusJurnal,
            string PeranPenulis,
            int JumlahPenulis,
            string JenisJurnal,
            string LibatkanMahasiswa,
            string StatusPengajuan
    ) : ICommand<SbuInsentifResponse>;
}

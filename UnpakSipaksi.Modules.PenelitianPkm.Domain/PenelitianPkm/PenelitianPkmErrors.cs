using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm
{
    public static class PenelitianPkmErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("PenelitianPkm.EmptyData", "data is not found");
        public static Error NotUnique(string NIDN, string Judul) =>
            Error.NotFound("PenelitianPkm.NotUnique", $"Nidn {NIDN} with Judul {Judul} is not unique");
        public static Error InvalidInputRumpunIlmu() =>
            Error.NotFound("PenelitianPkm.InvalidInputRumpunIlmu", "Rumpun ilmu must be filled in");
        public static Error InvalidStatusMember() =>
            Error.NotFound("PenelitianPkm.InvalidStatusMember", "Don't recognize status command sent");

        public static Error InvalidTahunPengajuan() =>
            Error.NotFound("PenelitianPkm.InvalidTahunPengajuan", "Tahun pengajuan is invalid format date");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("PenelitianPkm.NotFound", $"Penelitian hibah with identifier {Id} not found");
        public static Error InvalidData() =>
           Error.NotFound("PenelitianPkm.InvalidData", $"Hibah penelitian is not match existing data");
    }
}

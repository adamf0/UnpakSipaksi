using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah
{
    public static class PenelitianHibahErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("PenelitianHibah.EmptyData", "data is not found");
        public static Error NotUnique(string NIDN) =>
            Error.NotFound("PenelitianHibah.NotUnique", $"Nidn {NIDN} is not unique");
        public static Error NotUnique(string NIDN, string Judul) =>
            Error.NotFound("PenelitianHibah.NotUnique", $"Nidn {NIDN} with Judul {Judul} is not unique");
        public static Error NotSameValue() =>
            Error.NotFound("PenelitianHibah.NotSameValue", "not the same value in data 'nilai'");
        public static Error NotFoundKategoriSkema(string id) =>
            Error.NotFound("PenelitianHibah.NotFoundKategoriSkema", $"Kategori skema {id} is not found");
        public static Error NotFoundKategoriTkt(string id) =>
            Error.NotFound("PenelitianHibah.NotFoundKategoriTkt", $"Kategori tkt {id} is not found");
        public static Error InvalidInputPrioritasFokus() =>
            Error.NotFound("PenelitianHibah.InvalidInputPrioritasFokus", "Prioritas riset / Bidang fokus penelitian must be filled in");
        public static Error InvalidInputRumpunIlmu() =>
            Error.NotFound("PenelitianHibah.InvalidInputRumpunIlmu", "Rumpun ilmu must be filled in");
        public static Error InvalidStatusMember() =>
            Error.NotFound("PenelitianHibah.InvalidStatusMember", "Don't recognize status command sent");

        public static Error InvalidTahunPengajuan() =>
            Error.NotFound("PenelitianHibah.InvalidTahunPengajuan", "Tahun pengajuan is invalid format date");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("PenelitianHibah.NotFound", $"Penelitian hibah with identifier {Id} not found");
        public  static Error InvalidData() =>
           Error.NotFound("PenelitianHibah.InvalidData", $"Hibah penelitian is not match existing data");
    }
}

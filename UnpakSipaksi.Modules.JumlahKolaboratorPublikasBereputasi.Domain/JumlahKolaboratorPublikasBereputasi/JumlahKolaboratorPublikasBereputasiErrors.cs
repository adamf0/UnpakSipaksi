using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.JumlahKolaboratorPublikasBereputasi.Domain.JumlahKolaboratorPublikasBereputasi
{
    public static class JumlahKolaboratorPublikasBereputasiErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("JumlahKolaboratorPublikasBereputasi.EmptyData", "data is not found");
        public static Error NotSameValue() =>
           Error.NotFound("JumlahKolaboratorPublikasBereputasi.NotSameValue", "not the same value in data 'nilai'");
        public static Error UnknownKategoriSkema() =>
            Error.NotFound("JumlahKolaboratorPublikasBereputasi.UnknownKategoriSkema", "Unknown schema category");
        public static Error InvalidSkor() =>
            Error.NotFound("JumlahKolaboratorPublikasBereputasi.InvalidSkor", "Skor is invalid format");
        public static Error InvalidBobotKerjasama() =>
            Error.NotFound("JumlahKolaboratorPublikasBereputasi.InvalidBobotKerjasama", "Bobot kerjasama is invalid format");
        public static Error InvalidBobotPDP() =>
            Error.NotFound("JumlahKolaboratorPublikasBereputasi.InvalidBobotPDP", "Bobot pdp is invalid format");
        public static Error InvalidBobotTerapan() =>
            Error.NotFound("JumlahKolaboratorPublikasBereputasi.InvalidBobotTerapan", "Bobot terapan is invalid format");
        public static Error InvalidBobotPenelitianDasar() =>
            Error.NotFound("JumlahKolaboratorPublikasBereputasi.InvalidBobotPenelitianDasar", "Bobot penelitian dasar is invalid format");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("JumlahKolaboratorPublikasBereputasi.NotFound", $"Jumlah kolaborator publikas bereputasi with identifier {Id} not found");

    }
}

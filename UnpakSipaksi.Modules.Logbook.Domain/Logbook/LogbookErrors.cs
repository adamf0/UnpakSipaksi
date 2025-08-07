using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.Logbook.Domain.Logbook
{
    public static class LogbookErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("Logbook.EmptyData", "data is not found");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("Logbook.NotFound", $"Kelompok mitra with the identifier {Id} was not found");
        public static Error OverCapacity() =>
            Error.NotFound("Logbook.OverCapacity", "percentage over capacity");
        public static Error NoTargetHibah() =>
            Error.NotFound("Logbook.NoTargetHibah", "not target penelitian hibah or pengabdian");
        public static Error InvalidFormatPercentage() =>
            Error.NotFound("Logbook.InvalidFormatPercentage", "percentage is invalid format");
        public static Error InvalidFormatDate() =>
            Error.NotFound("Logbook.InvalidFormatDate", "Tanggal kegiatan is invalid format");
        public static Error InvalidData() =>
            Error.NotFound("Logbook.InvalidData", "Hibah penelitian is not match existing data");
    }
}

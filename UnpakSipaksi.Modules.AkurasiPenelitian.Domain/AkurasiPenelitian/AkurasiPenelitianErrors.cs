using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.AkurasiPenelitian.Domain.AkurasiPenelitian
{
    public static class AkurasiPenelitianErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("AkurasiPenelitian.EmptyData", "data is not found");
        public static Error NotSameValue() =>
            Error.NotFound("AkurasiPenelitian.NotSameValue", "not the same value in data 'nilai'");
        public static Error UnknownKategoriSkema() =>
            Error.NotFound("AkurasiPenelitian.UnknownKategoriSkema", "Unknown schema category");
        public static Error InvalidSkor() =>
            Error.NotFound("AkurasiPenelitian.InvalidSkor", "Skor is invalid format");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("AkurasiPenelitian.NotFound", $"Akurasi penelitian with identifier {Id} not found");
        
    }
}

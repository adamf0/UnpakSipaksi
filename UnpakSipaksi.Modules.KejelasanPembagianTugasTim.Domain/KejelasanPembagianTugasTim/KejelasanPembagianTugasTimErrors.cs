using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Domain.KejelasanPembagianTugasTim
{
    public static class KejelasanPembagianTugasTimErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("KejelasanPembagianTugasTim.EmptyData", "data is not found");
        public static Error NotSameValue() =>
            Error.NotFound("KejelasanPembagianTugasTim.NotSameValue", "not the same value in data 'nilai'");
        public static Error UnknownKategoriSkema() =>
            Error.NotFound("KejelasanPembagianTugasTim.UnknownKategoriSkema", "Unknown schema category");
        public static Error InvalidValueSkor() =>
            Error.NotFound("KejelasanPembagianTugasTim.InvalidValueSkor", "Invalid value 'skor'");
        public static Error NotFound(Guid Id) =>
            Error.NotFound("KejelasanPembagianTugasTim.NotFound", $"Kejelasan pembagian tugas tim with identifier {Id} not found");

    }
}

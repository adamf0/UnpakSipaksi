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
            Error.NotFound("KejelasanPembagianTugasTim.EmptyData", $"data is not found");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("KejelasanPembagianTugasTim.NotFound", $"The event with the identifier {Id} was not found");

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnpakSipaksi.Common.Domain;

namespace UnpakSipaksi.Modules.JenisPublikasi.Domain.JenisPublikasi
{
    public static class JenisPublikasiErrors
    {
        public static Error EmptyData() =>
            Error.NotFound("JenisPublikasi.EmptyData", "data is not found");

        public static Error NotFound(Guid Id) =>
            Error.NotFound("JenisPublikasi.NotFound", $"Jenis publikasi with identifier {Id} not found");

    }
}

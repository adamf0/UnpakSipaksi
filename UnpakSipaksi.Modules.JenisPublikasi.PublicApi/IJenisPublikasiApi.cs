using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.JenisPublikasi.PublicApi
{
    public interface IJenisPublikasiApi
    {
        Task<JenisPublikasiResponse?> GetAsync(Guid JenisPublikasiUuid, CancellationToken cancellationToken = default);
    }

}

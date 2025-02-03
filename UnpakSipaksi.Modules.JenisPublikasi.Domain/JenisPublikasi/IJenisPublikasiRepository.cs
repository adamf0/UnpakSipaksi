using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.JenisPublikasi.Domain.JenisPublikasi
{
    public interface IJenisPublikasiRepository
    {
        void Insert(JenisPublikasi jenisPublikasi);
        Task<JenisPublikasi> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(JenisPublikasi jenisPublikasi);
    }
}

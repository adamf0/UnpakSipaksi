using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KategoriProgramPengabdian.PublicApi
{
    public interface IKategoriProgramPengabdianApi
    {
        Task<KategoriProgramPengabdianResponse?> GetAsync(Guid KategoriProgramPengabdianUuid, CancellationToken cancellationToken = default);
    }

}

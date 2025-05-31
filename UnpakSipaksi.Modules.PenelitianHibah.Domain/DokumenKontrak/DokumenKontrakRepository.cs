using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianHibah.Domain.DokumenKontrak
{
    public interface IDokumenKontrakRepository
    {
        void Insert(DokumenKontrak DokumenKontrak);
        Task<DokumenKontrak> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(DokumenKontrak DokumenKontrak);
    }
}

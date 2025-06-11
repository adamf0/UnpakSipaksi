using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianPkm.Domain.PenelitianPkm
{
    public interface IPenelitianPkmRepository
    {
        void Insert(PenelitianPkm PenelitianPkm);
        Task<PenelitianPkm> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task<PenelitianPkm> GetAsync(Guid Uuid, string Nidn, CancellationToken cancellationToken = default);
        Task<bool> HasUniqueDataAsync(Guid? Uuid, string NIDN, string Judul, CancellationToken cancellationToken = default);
        Task DeleteAsync(PenelitianPkm PenelitianPkm);
    }
}

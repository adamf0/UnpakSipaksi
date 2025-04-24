using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianHibah.Domain.PenelitianHibah
{
    public interface IPenelitianHibahRepository
    {
        void Insert(PenelitianHibah PenelitianHibah);
        Task<PenelitianHibah> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task<PenelitianHibah> GetAsync(Guid Uuid, string Nidn, CancellationToken cancellationToken = default);
        Task<bool> HasUniqueDataAsync(Guid? Uuid, string NIDN, string Judul, CancellationToken cancellationToken = default);
        Task DeleteAsync(PenelitianHibah PenelitianHibah);
    }
}

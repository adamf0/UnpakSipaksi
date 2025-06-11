using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianPkm.Domain.MemberDosen
{
    public interface IMemberDosenRepository
    {
        void Insert(MemberDosen MemberDosen);
        Task<MemberDosen> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task<MemberDosen> GetAsync(Guid Uuid, Guid UuidPenelitianHibah, CancellationToken cancellationToken = default);
        Task<MemberDosen> GetAsync(Guid Uuid, string Nidn, CancellationToken cancellationToken = default);
        Task<int> CheckUniqueDataAsync(int PenelitianHibahId, string NIDN, CancellationToken cancellationToken = default);
        Task DeleteAsync(MemberDosen MemberDosen);
    }
}

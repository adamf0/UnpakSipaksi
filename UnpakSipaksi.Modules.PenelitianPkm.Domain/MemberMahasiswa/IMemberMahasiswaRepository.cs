using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianPkm.Domain.MemberMahasiswa
{
    public interface IMemberMahasiswaRepository
    {
        void Insert(MemberMahasiswa MemberMahasiswa);
        Task<MemberMahasiswa> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task<MemberMahasiswa> GetAsync(Guid Uuid, string Npm, CancellationToken cancellationToken = default);
        Task<MemberMahasiswa> GetAsync(Guid Uuid, Guid UuidPenelitianHibah, string Npm, CancellationToken cancellationToken = default);
        Task<int> CheckUniqueDataAsync(int PenelitianHibahId, string NPM, CancellationToken cancellationToken = default);
        Task DeleteAsync(MemberMahasiswa MemberMahasiswa);
    }
}

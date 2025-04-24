using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianHibah.Domain.MemberNonDosen
{
    public interface IMemberNonDosenRepository
    {
        void Insert(MemberNonDosen MemberNonDosen);
        Task<MemberNonDosen> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(MemberNonDosen MemberNonDosen);
    }
}

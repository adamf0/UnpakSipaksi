using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.FokusPengabdian.Domain.FokusPengabdian
{
    public interface IFokusPengabdianRepository
    {
        void Insert(FokusPengabdian akurasiPenelitian);
        Task<FokusPengabdian> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(FokusPengabdian akurasiPenelitian);
    }
}

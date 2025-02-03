using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.AkurasiPenelitian.Domain.AkurasiPenelitian
{
    public interface IAkurasiPenelitianRepository
    {
        void Insert(AkurasiPenelitian akurasiPenelitian);
        Task<AkurasiPenelitian> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(AkurasiPenelitian akurasiPenelitian);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.TemaPenelitian.Domain.TemaPenelitian
{
    public interface ITemaPenelitianRepository
    {
        void Insert(TemaPenelitian TemaPenelitian);
        Task<TemaPenelitian> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(TemaPenelitian TemaPenelitian);
    }
}

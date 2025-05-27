using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianHibah.Domain.Substansi
{
    public interface ISubstansiRepository
    {
        void Insert(Substansi Substansi);
        Task<Substansi> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(Substansi Substansi);
    }
}

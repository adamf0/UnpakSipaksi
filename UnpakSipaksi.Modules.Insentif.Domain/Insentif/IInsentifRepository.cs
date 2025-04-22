using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.Insentif.Domain.Insentif
{
    public interface IInsentifRepository
    {
        void Insert(Insentif Insentif);
        Task<Insentif> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(Insentif Insentif);
    }
}

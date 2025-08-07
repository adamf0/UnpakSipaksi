using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.Logbook.Domain.Logbook
{
    public interface ILogbookRepository
    {
        void Insert(Logbook Logbook);
        Task<Logbook> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task<Logbook> GetAsync(Guid Uuid, int idHibah, int idPkm, CancellationToken cancellationToken = default);
        Task DeleteAsync(Logbook Logbook);
    }
}

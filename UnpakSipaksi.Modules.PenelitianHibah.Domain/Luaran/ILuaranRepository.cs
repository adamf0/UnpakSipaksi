using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianHibah.Domain.Luaran
{
    public interface ILuaranRepository
    {
        void Insert(Luaran Luaran);
        Task<Luaran> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(Luaran Luaran);
    }
}

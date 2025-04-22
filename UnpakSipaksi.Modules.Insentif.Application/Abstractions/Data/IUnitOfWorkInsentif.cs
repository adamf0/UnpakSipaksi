using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.Insentif.Application.Abstractions.Data
{
    public interface IUnitOfWorkInsentif
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

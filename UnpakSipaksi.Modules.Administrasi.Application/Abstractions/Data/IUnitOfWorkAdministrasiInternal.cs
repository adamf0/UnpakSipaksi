using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.Administrasi.Application.Abstractions.Data
{
    public interface IUnitOfWorkAdministrasiInternal
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianPkm.Application.Abstractions.Data
{
    public interface IUnitOfWorkDokumenLainnya
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

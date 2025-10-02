using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.LaporanKemajuan.Application.Abstractions.Data
{
    public interface IUnitOfWorkDokumenHibah
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

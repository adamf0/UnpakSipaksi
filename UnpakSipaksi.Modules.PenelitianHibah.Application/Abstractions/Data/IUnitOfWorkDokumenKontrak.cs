using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.PenelitianHibah.Application.Abstractions.Data
{
    public interface IUnitOfWorkDokumenKontrak
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

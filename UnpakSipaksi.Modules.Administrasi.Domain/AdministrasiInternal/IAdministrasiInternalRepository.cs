using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.Administrasi.Domain.AdministrasiInternal
{
    public interface IAdministrasiInternalRepository
    {
        void Insert(AdministrasiInternal AdministrasiInternal);
        Task<AdministrasiInternal> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(AdministrasiInternal AdministrasiInternal);
    }
}

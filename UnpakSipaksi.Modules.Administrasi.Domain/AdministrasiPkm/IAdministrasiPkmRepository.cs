using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.Administrasi.Domain.AdministrasiPkm
{
    public interface IAdministrasiPkmRepository
    {
        void Insert(AdministrasiPkm AdministrasiPkm);
        Task<AdministrasiPkm> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(AdministrasiPkm AdministrasiPkm);
    }
}

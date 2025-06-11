using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.IndikatorCapaian.Domain
{
    public interface IIndikatorCapaianRepository
    {
        void Insert(IndikatorCapaian IndikatorCapaian);
        Task<IndikatorCapaian> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(IndikatorCapaian IndikatorCapaian);
    }
}

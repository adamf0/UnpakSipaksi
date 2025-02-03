using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.KejelasanPembagianTugasTim.Domain.KejelasanPembagianTugasTim
{
    public interface IKejelasanPembagianTugasTimRepository
    {
        void Insert(KejelasanPembagianTugasTim KejelasanPembagianTugasTim);
        Task<KejelasanPembagianTugasTim> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(KejelasanPembagianTugasTim KejelasanPembagianTugasTim);
    }
}

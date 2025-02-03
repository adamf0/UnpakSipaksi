using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.InovasiPemecahanMasalah.Domain.InovasiPemecahanMasalah
{
    public interface IInovasiPemecahanMasalahRepository
    {
        void Insert(InovasiPemecahanMasalah inovasiPemecahanMasalah);
        Task<InovasiPemecahanMasalah> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(InovasiPemecahanMasalah inovasiPemecahanMasalah);
    }
}

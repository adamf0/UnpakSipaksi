using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnpakSipaksi.Modules.ArtikelMediaMassa.Domain.ArtikelMediaMassa
{
    public interface IArtikelMediaMassaRepository
    {
        void Insert(ArtikelMediaMassa artikelMediaMassa);
        Task<ArtikelMediaMassa> GetAsync(Guid Uuid, CancellationToken cancellationToken = default);
        Task DeleteAsync(ArtikelMediaMassa artikelMediaMassa);
    }
}
